﻿using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCM.ViewModel
{
   public class AppViewModel: ReactiveObject
    {
        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
        }
        private readonly ObservableAsPropertyHelper<IEnumerable<NugetDetailsViewModel>> _searchResults;
        public IEnumerable<NugetDetailsViewModel> SearchResults => _searchResults.Value;
        private readonly ObservableAsPropertyHelper<bool> _isAvailable;
        public bool IsAvailable => _isAvailable.Value;
        public AppViewModel()
        {
            // Creating our UI declaratively
            // 
            // The Properties in this ViewModel are related to each other in different 
            // ways - with other frameworks, it is difficult to describe each relation
            // succinctly; the code to implement "The UI spinner spins while the search 
            // is live" usually ends up spread out over several event handlers.
            //
            // However, with ReactiveUI, we can describe how properties are related in a 
            // very organized clear way. Let's describe the workflow of what the user does 
            // in this application, in the order they do it.

            // We're going to take a Property and turn it into an Observable here - this
            // Observable will yield a value every time the Search term changes, which in
            // the XAML, is connected to the TextBox. 
            //
            // We're going to use the Throttle operator to ignore changes that happen too 
            // quickly, since we don't want to issue a search for each key pressed! We 
            // then pull the Value of the change, then filter out changes that are identical, 
            // as well as strings that are empty.
            //
            // We then do a SelectMany() which starts the task by converting Task<IEnumerable<T>> 
            // into IObservable<IEnumerable<T>>. If subsequent requests are made, the 
            // CancellationToken is called. We then ObserveOn the main thread, 
            // everything up until this point has been running on a separate thread due 
            // to the Throttle().
            //
            // We then use an ObservableAsPropertyHelper, OAPH, and the ToProperty() method to allow
            // us to have the latest results that we can expose through the property to the View.
            _searchResults = this
                .WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(800))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .Where(term => !string.IsNullOrWhiteSpace(term))
                .SelectMany(SearchNuGetPackages)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, x => x.SearchResults);

            // We subscribe to the "ThrownExceptions" property of our OAPH, where ReactiveUI 
            // marshals any exceptions that are thrown in SearchNuGetPackages method. 
            // See the "Error Handling" section for more information about this.
            _searchResults.ThrownExceptions.Subscribe(error => { /* Handle errors here */ });

            // A helper method we can use for Visibility or Spinners to show if results are available.
            // We get the latest value of the SearchResults and make sure it's not null.
            _isAvailable = this
                .WhenAnyValue(x => x.SearchResults)
                .Select(searchResults => searchResults != null)
                .ToProperty(this, x => x.IsAvailable);
        }

        private async Task<IEnumerable<NugetDetailsViewModel>> SearchNuGetPackages(
        string term, CancellationToken token)
        {
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3()); // Add v3 API support
            var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            var source = new SourceRepository(packageSource, providers);
            ILogger logger = NullLogger.Instance;

            var filter = new SearchFilter(false);
            var resource = await source.GetResourceAsync<PackageSearchResource>().ConfigureAwait(false);
            var metadata = await resource.SearchAsync(term, filter, 0, 10, logger, token).ConfigureAwait(false);
            return metadata.Select(x => new NugetDetailsViewModel(x));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDialogService
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IDialogService dialogService = new DialogService();

            dialogService.Register<DialogViewModel, DialogWindow>();
            dialogService.Register<SecondDialogVM, SecondDialog>();

            var viewModel = new MainWindowViewModel(dialogService);
            var view = new MainWindow() { DataContext = viewModel };
            view.ShowDialog();

            base.OnStartup(e);

        }
    }
}

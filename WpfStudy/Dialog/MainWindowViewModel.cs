using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFDialogService
{
    /// <summary>
    /// demo:
    /// voi moi 1 new model message, he thong se tu dong detect ra view tuong ung
    /// dependence of viewmodel that inject view object control by service
    /// </summary>
   public class MainWindowViewModel
    {
        private readonly IDialogService dialogService;
        public ICommand DisplayMessageCommand { get; }
        public ICommand SecondDisplayMessageCommand { get; }
       
        
        public MainWindowViewModel(IDialogService _dialogService)
        {
            this.dialogService = _dialogService;

            DisplayMessageCommand = new ActionCommand(p => DisplayMessage());
            SecondDisplayMessageCommand = new ActionCommand(p => SecondDisplayMessage());

        }
        
        private void DisplayMessage()
        {
            string dialogResult;
            var viewModel = new DialogViewModel("Demo Dialog");
            
            //remove old/traditional code...
            
            //rem1: var view = new DialogWindow() { DataContext = viewModel };
            //rem2: bool? result=view.ShowDialog();
            
            //using new dependency approach / using service to display /show dialog
            //service will know which view to use to display basing on viewmodel
            //bcause view model is register with view when app start up
            //

            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue)
            {
                if (result.Value)
                {
                    dialogResult = "you click OK";
                }
                else
                {
                    dialogResult = "you click Cancel";
                }
            }
        }

        private void SecondDisplayMessage()
        {
           
            var viewModel = new SecondDialogVM("I am just simple the second");           

            bool? result = dialogService.ShowDialog(viewModel);

           
        }
    }
}

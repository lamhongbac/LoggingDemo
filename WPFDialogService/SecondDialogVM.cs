using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFDialogService
{
    public class SecondDialogVM: IDialogRequestClose
    {
        private string title; 
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        

        public event EventHandler<DialogRequestedCloseEventArg> CloseRequested;
        public ICommand OKCommand { get; }
        public ICommand CancelCommand { get; }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public SecondDialogVM(string message)
        {
            Message = message;
            OKCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogRequestedCloseEventArg(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogRequestedCloseEventArg(false)));

        }
    }
}

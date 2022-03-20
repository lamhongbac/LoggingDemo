using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFDialogService
{
    partial class ActionCommand : ICommand
    {
        private Action<object> p;

        

        public ActionCommand(Action<object> _p)
        {
            this.p = _p;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            p(parameter);
        }
    }
}

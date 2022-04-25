using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservation.Command
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);
        /// <summary>
        /// ham nay chay khi event CanExecutedChanged fired
        /// ham nay se  re-run CanExecute --> tac dong nguoc lai den view 
        /// --> enable or disable button attached to this command
        /// </summary>
        public void OnExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}

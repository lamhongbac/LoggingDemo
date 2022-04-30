using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Command
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool isExecuting;

        public bool IsExecuting
        {
            get => isExecuting;
            set { 
                isExecuting = value;
                OnExecutedChanged(); 
            }
        }
        public override bool CanExecute(object parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }
        public override async void Execute(object parameter)
        {
            IsExecuting = true;
            try
            {
                
                await ExecuteAsync(parameter);
            }
            finally
            {
                IsExecuting = false;
            }
        }
        public abstract Task ExecuteAsync(object parameter);


    }
}

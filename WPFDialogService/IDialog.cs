using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDialogService
{
    /// <summary>
    /// dialog view (ex:DialogWindow)
    /// data: DialogViewModel
    /// </summary>
 public   interface IDialog
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        Window Owner { get; set; }
        void Close();
        bool? ShowDialog();
    }
    /// <summary>
    /// service provider to Window using dialog for display...
    /// </summary>
    public interface IDialogService
    {
        void Register<TViewModel, TView>() where TViewModel:IDialogRequestClose
                                            where TView:IDialog;
        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;
                                           
    }
    /// <summary>
    /// event close dialog
    /// </summary>
    public interface IDialogRequestClose
    {
        event EventHandler<DialogRequestedCloseEventArg> CloseRequested;
    }
    public class DialogRequestedCloseEventArg:EventArgs
    {
        public DialogRequestedCloseEventArg(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
        public bool? DialogResult { get; }
    }
    public class DialogService : IDialogService
    {
        // Window owner;=>Window _owner
        public IDictionary<Type,Type> Mappings { get; }
        public DialogService()
        {

            if (Mappings == null)
            {
                Mappings = new Dictionary<Type, Type>();
            }
            //owner = _owner;
        }
        /// <summary>
        /// cho phep nh kieu dialog view va data khac nhau dang ky vao container nay
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TView"></typeparam>
        public void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog
        {
            
            if (Mappings.ContainsKey(typeof(TViewModel)))
            {
                throw new ArgumentException($"Type of {typeof(TViewModel)} already map");

            }
            Mappings.Add(typeof(TViewModel), typeof(TView));
        }
        /// <summary>
        /// show dialog can cu cu vao para viewModel
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public bool? ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : IDialogRequestClose
           
        {
            Type viewType = Mappings[typeof(TViewModel)];
            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);
            dialog.DataContext = viewModel;
           
            EventHandler<DialogRequestedCloseEventArg> handler = null;
            handler = (sender, e) =>
              {
                  viewModel.CloseRequested -= handler;
                  if (e.DialogResult.HasValue)
                  {
                      dialog.DialogResult = e.DialogResult;
                  }
                  else
                  {
                      dialog.Close();
                  }

              };
            viewModel.CloseRequested += handler;
            return dialog.ShowDialog();
        }
    }
}

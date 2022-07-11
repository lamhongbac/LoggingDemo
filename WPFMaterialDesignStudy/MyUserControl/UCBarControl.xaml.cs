using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFMaterialDesignStudy.ViewModel;

namespace WPFMaterialDesignStudy.MyUserControl
{
    /// <summary>
    /// Interaction logic for UCBarControl.xaml
    /// </summary>
    public partial class UCBarControl : UserControl
    {
        UCBarControlViewModel viewModel { get; set; }
        public UCBarControl()
        {
            InitializeComponent();
            viewModel = new UCBarControlViewModel();
            this.DataContext = viewModel;
        }
    }
}

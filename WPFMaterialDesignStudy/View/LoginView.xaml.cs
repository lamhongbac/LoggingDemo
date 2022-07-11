﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFMaterialDesignStudy.ViewModel;

namespace WPFMaterialDesignStudy.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        LoginViewModel viewModel;
        public LoginView()
        {
            InitializeComponent(); viewModel = new LoginViewModel();
            this.DataContext = viewModel;
        }
    }
}

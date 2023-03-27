using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfStudy.BetalgoOpenAI;
using WpfStudy.View;

namespace WpfStudy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            UniCodeSearchView view = new UniCodeSearchView();
            view.Show();
        }
        //ButtonChatGpt_Click

        private void ButtonChatGpt_Click(object sender, RoutedEventArgs e)
        {
            ChatGpt view = new ChatGpt();
            view.Show();
        }
    }
}

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
using System.Windows.Shapes;
using WpfStudy.Logic;

namespace WpfStudy.View
{
    /// <summary>
    /// Interaction logic for UniCodeSearchView.xaml
    /// </summary>
    public partial class UniCodeSearchView : Window
    {
        public UniCodeSearchView()
        {
            InitializeComponent();
            txtKeyword.Focus();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string content = tbMultiLine.Text.ToLower();
            string keyword = txtKeyword.Text.ToLower();
            if (content.IndexOf(keyword)>-1)
            {
                MessageBox.Show("Found keyword");
            }
        }

        private void btnSearchRelative_Click(object sender, RoutedEventArgs e)
        {
            tbnoUnicode.Text = StringUtil.RemoveDiacritics(tbMultiLine.Text);
        }
    }
}

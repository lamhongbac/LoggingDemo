using Microsoft.VisualBasic;
using System.Globalization;
using System;

namespace DateTimeDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string dateString = textBox2.Text.Trim();
            string dateFormat = textBox1.Text.Trim();

            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dateTime10;

            bool isSuccess = DateTime.TryParseExact(dateString, dateFormat, provider,
                DateTimeStyles.None, out dateTime10);

            if (isSuccess)
            {
                MessageBox.Show("Convert success");
                dateTimePicker1.Value = dateTime10;
            }
            else
            {
                MessageBox.Show("convert failed");
            }
        }
    }
}
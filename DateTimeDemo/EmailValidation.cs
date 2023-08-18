using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DateTimeDemo
{
    public partial class EmailValidation : Form
    {
        public EmailValidation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string error = IsValidEmailV1(email);
            label2.Text = error;
        }
        public string IsValidEmailV1(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return string.Empty;
            }
            catch (FormatException)
            {
                return "InvalidEmail";
            }


        }
    }
}

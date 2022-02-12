using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoggingDemo
{
    public partial class Form1 : Form
    {
        private readonly ILogger _logger;
        public Form1(ILogger<Form1> logger)
        {
            _logger = logger;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation("Form1 {BusinessLayerEvent} at {dateTime}", "Started", DateTime.UtcNow);
                // Perform Business Logic here 

                MessageBox.Show("Hello .NET Core 3.1.This is First Forms app in .NET Core");
                _logger.LogInformation("Form1 {BusinessLayerEvent} at {dateTime}", "Ended", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                //Log technical exception 
                _logger.LogError(ex.Message);
            }
        }
    }
}

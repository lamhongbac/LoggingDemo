using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QRCoder;
using ZXing;
using ZXing.Windows.Compatibility;
using ZXing.QrCode;
using System.Drawing.Imaging;

namespace DateTimeDemo
{
  
    public partial class UniqueCodeForm : Form
    {
        public UniqueCodeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = GetUniqueCode();
        }
        private string GetUniqueCode()
        {
            long ticks = DateTime.Now.Ticks;
            byte[] bytes = BitConverter.GetBytes(ticks);
            string id = Convert.ToBase64String(bytes)
                                    .Replace('+', '_')
                                    .Replace('/', '-')
                                    .TrimEnd('=');
            return id;
        }
        private string GetUniqeString()
        {
            Guid g = Guid.NewGuid();

            string uniqueString = Convert.ToBase64String(g.ToByteArray());
            uniqueString = uniqueString.Replace("=", "");
            uniqueString = uniqueString.Replace("+", "");
            uniqueString = uniqueString.Replace("/", "");
            return uniqueString;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = GetUniqeString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Text length is " + textBox1.Text.Length);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var brcode = new ZXing.BarcodeWriter<Image>()
            //{
            //    Format = BarcodeFormat.CODE_128
            //};
            //pictureBox1.Image = brcode.Write(textBox1.Text);




            // Set barcode type to Code 39
            //barcode.Type = BarcodeLib.Barcode.BarcodeType.CODE39;

            //// Set your encoded barcode value
            //barcode.Data = "123456789";
            //Image barcode = code128.Encode(BarcodeLib..CODE128, txtDataBarcode.Text);
            //pictureBox_Barcode.Image = barcode;
            //QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCode qrCode = new QRCode(qrGenerator.CreateQrCode(txtDataQRCode.Text, QRCodeGenerator.ECCLevel.Q));
            ////QRCodeGenerator.ECCLevel.Q là mức chịu lỗi 25%; .L là 7%; .M là 15% và .H là trên 25%
            //pictureBox_QRCode.Image = qrCode.GetGraphic(2, Color.Black, Color.White, false);
            //qrGenerator.Dispose();
            //qrCode.Dispose();
            var byteArrayBarCode = CreateQrCode(textBox1.Text,false);
            MemoryStream ms1 = new MemoryStream(byteArrayBarCode, 0, byteArrayBarCode.Length);
            ms1.Position = 0; // this is important
            Image BCodeImage = Image.FromStream(ms1, true);


            pictureBox1.Image = BCodeImage;




            var byteArrayQRCode = CreateQrCode(textBox1.Text);
            MemoryStream ms = new MemoryStream(byteArrayQRCode, 0, byteArrayQRCode.Length);
            ms.Position = 0; // this is important
            Image QRCodeImage = Image.FromStream(ms, true);
            
            
            pictureBox2.Image = QRCodeImage;
        }
        public static byte[] CreateQrCode(string content,bool isQRCode=true)
        {
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = 100,
                    Height = 100,
                }
            };
            if (!isQRCode )
            {
                writer.Format= BarcodeFormat.CODE_128;
                
            }
            var qrCodeImage = writer.Write(content); // BOOM!!

            using (var stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}

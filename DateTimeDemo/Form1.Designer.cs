namespace DateTimeDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(57, 39);
            label1.Name = "label1";
            label1.Size = new Size(111, 25);
            label1.TabIndex = 0;
            label1.Text = "Date Format";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(60, 92);
            label2.Name = "label2";
            label2.Size = new Size(84, 25);
            label2.TabIndex = 1;
            label2.Text = "Date Text";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 150);
            label3.Name = "label3";
            label3.Size = new Size(59, 25);
            label3.TabIndex = 1;
            label3.Text = "Result";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(183, 33);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(228, 31);
            textBox1.TabIndex = 2;
            textBox1.Text = "MM/dd/yyyy hh:mm tt";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(183, 92);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(228, 31);
            textBox2.TabIndex = 2;
            textBox2.Text = "2023/08/15 12:12";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            dateTimePicker1.Location = new Point(183, 145);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(470, 31);
            dateTimePicker1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(503, 48);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 4;
            button1.Text = "Convert";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private DateTimePicker dateTimePicker1;
        private Button button1;
    }
}
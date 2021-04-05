using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApp2.Forms
{
    public partial class EmailForm : Form
    {
        public EmailForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage("didakish@gmail.com", "forcedgaming8@gmail.com", "Not enough links", richTextBox1.Text);
            NetworkCredential cre = new NetworkCredential("didakish@gmail.com", passwordBox.Text);
            SmtpClient sc = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
            sc.Credentials = cre;
            sc.EnableSsl = true;
            sc.Send(mail);
            MessageBox.Show("Thank you for your feedback!");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
using ConsultationPlus.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            logoLabel.Left = (this.ClientSize.Width - logoLabel.Width) / 2;
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            loginButton.Left = (this.ClientSize.Width - loginButton.Width) / 2;
            disclaimerButton.Left = (this.ClientSize.Width - (disclaimerButton.Width + aboutButton.Width)) / 2 - 5;
            aboutButton.Left = (this.ClientSize.Width + disclaimerButton.Width - aboutButton.Width) / 2 + 5;
            logoLabel.Left = ((this.ClientSize.Width - (logoLabel.Width + pictureBox5.Width)) / 2);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            GraphsAPIHandler.Login();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void disclaimerButton_Click(object sender, EventArgs e)
        {
            DisclaimerForm form = new DisclaimerForm();
            form.Show();
        }
    }

}

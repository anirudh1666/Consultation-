using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class RegisterForm : Form
    {
        private string user;

        public RegisterForm(string email)
        {
            InitializeComponent();
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
            comboBox1.Left = (this.ClientSize.Width - comboBox1.Width) / 2;
            txt.Left = (this.ClientSize.Width - txt.Width) / 2;
            user = email;
        }

        /** We add the user to django database. We jsut send the corresponding
         *  code for the location they select to the database by calling Program.AddUser.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                return;
            }
            var choice = comboBox1.SelectedItem.ToString();
            switch (choice)
            {
                case "North East England":
                    DatabaseCommunicator.AddUser(user, "1");
                    break;
                case "North West England":
                    DatabaseCommunicator.AddUser(user, "2");
                    break;
                case "Yorkshire and The Humber":
                    DatabaseCommunicator.AddUser(user, "3");
                    break;
                case "East Midlands":
                    DatabaseCommunicator.AddUser(user, "4");
                    break;
                case "West Midlands":
                    DatabaseCommunicator.AddUser(user, "5");
                    break;
                case "East of England":
                    DatabaseCommunicator.AddUser(user, "6");
                    break;
                case "London":
                    DatabaseCommunicator.AddUser(user, "7");
                    break;
                case "South East England":
                    DatabaseCommunicator.AddUser(user, "8");
                    break;
                case "South West England":
                    DatabaseCommunicator.AddUser(user, "9");
                    break;
                case "Wales":
                    DatabaseCommunicator.AddUser(user, "10");
                    break;
                case "Scotland":
                    DatabaseCommunicator.AddUser(user, "11");
                    break;
                case "Northern Ireland":
                    DatabaseCommunicator.AddUser(user, "12");
                    break;
                default:
                    DatabaseCommunicator.AddUser(user, "0");
                    break;
            }
            this.Close();
        }

        /** When closed we open up the search engine app.
         */
        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            PrivacyForm privacy = new PrivacyForm
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            privacy.Show();
            GraphsAPIHandler.LoadCategories();
            Program.LoadWhitelist();
        }
    }
}

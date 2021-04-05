using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace ConsultationPlus.Forms
{
    public partial class DeveloperPanel : Form
    {
        public DeveloperPanel()
        {
            InitializeComponent();
            textBox2.Text = GoogleAPIHandler.search_engine_id;
            textBox1.Text = GoogleAPIHandler.api_key;
            if (GoogleAPIHandler.developer_mode)
            {
                button1.Text = "Deactivate";
            }
            else
            {
                button1.Text = "Activate";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.ToString() == "Activate")
            {
                if ((textBox2.Text == "") || (textBox1.Text == ""))
                {
                    MessageBox.Show("Please enter a valid api key and engine ID.");
                    return;
                }
                GoogleAPIHandler.search_engine_id = textBox2.Text;
                GoogleAPIHandler.api_key = textBox1.Text;
                GoogleAPIHandler.developer_mode = true;
            }
            else
            {
                GoogleAPIHandler.developer_mode = false;
            }

            this.Close();
            WhitelistForm whitelist = new WhitelistForm();
            whitelist.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GoogleAPIHandler.developer_mode = false;
            this.Close();
            WhitelistForm whitelist = new WhitelistForm();
            whitelist.Show();
        }
    }
}

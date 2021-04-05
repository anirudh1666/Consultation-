using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class PrivacyForm : Form
    {
        public PrivacyForm()
        {
            InitializeComponent();
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
            if (Program.save_recommendation && Program.save_search)
            {
                checkBox3.Checked = true;
            }
            else if (Program.save_recommendation && !Program.save_search)
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox1.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if ((checkBox1.Checked))
            {
                sum += 1;
            }
            if (checkBox2.Checked)
            {
                sum += 1;
            }
            if (checkBox3.Checked)
            {
                sum += 1;
            }
            if ((sum > 1) || (sum == 0))
            {
                MessageBox.Show("Please select one option.");
                return;
            }

            if (checkBox1.Checked)
            {
                Program.save_recommendation = false;
                Program.save_search = false;
            }
            if (checkBox2.Checked)
            {
                Program.save_recommendation = true;
                Program.save_search = false;
            }
            if (checkBox3.Checked)
            {
                Program.save_recommendation = true;
                Program.save_search = true;
            }
            this.Close();
        }
    }
}

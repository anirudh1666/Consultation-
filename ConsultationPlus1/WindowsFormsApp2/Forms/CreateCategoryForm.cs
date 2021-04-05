using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp2
{
    public partial class CreateCategoryForm : Form
    {
        public CreateCategoryForm()
        {
            InitializeComponent();
            checkedListBox1.Left = (this.ClientSize.Width - checkedListBox1.Width) / 2;
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
        }

        /** Just creates a new Category object and adds it to Program.categories.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> sites_to_include = new List<string>();
            foreach (string site in checkedListBox1.CheckedItems)
            {
                // add all the sites that were ticked from the whitelist.
                sites_to_include.Add(site);
            }
            Program.CreateCategory(textBox1.Text, sites_to_include);
            WhitelistForm frm5 = new WhitelistForm();
            this.Close();
            frm5.Show();
        }

        private void CreateCategoryForm_Load(object sender, EventArgs e)
        {
            checkedListBox1.DataSource = Program.whitelist;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class DeleteCategory : Form
    {
        public DeleteCategory()
        {
            InitializeComponent();
            checkedListBox1.Left = (this.ClientSize.Width - checkedListBox1.Width) / 2;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
        }

        private void DeleteCategory_Load(object sender, EventArgs e)
        {
            // we want to load program.category names into checkedListBox1.
            checkedListBox1.DataSource = Program.categories;
            checkedListBox1.DisplayMember = "Name";
        }

        /** Deletes all the categories that the user has selected from 
         *  Program.categories.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Category cat in checkedListBox1.CheckedItems)
            {
                Program.categories.Remove(cat);
            }
            this.Close();
            WhitelistForm frm = new WhitelistForm();
            frm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

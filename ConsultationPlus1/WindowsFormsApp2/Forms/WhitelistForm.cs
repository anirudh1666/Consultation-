using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ConsultationPlus.Forms;

namespace WindowsFormsApp2
{
    public partial class WhitelistForm : Form
    {
        public WhitelistForm()
        {
            InitializeComponent();
            checkBox1.Left = (this.ClientSize.Width - checkBox1.Width) / 2;
            checkedListBox1.Left = (this.ClientSize.Width - checkedListBox1.Width) / 2;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
        }

        /** Depending on which item is clicked we call relevant code for that item.
         */
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.Equals("toolStripMenuItem1"))
            {
                // user wants to create a new category.
                CreateCategoryForm frm6 = new CreateCategoryForm();
                frm6.StartPosition = FormStartPosition.CenterScreen;
                frm6.Show();
                this.Close();

            }
            if (e.ClickedItem.Name.Equals("toolStripMenuItem2"))
            {
                // user wants to delete an existing category.
                DeleteCategory delete = new DeleteCategory();
                delete.StartPosition = FormStartPosition.CenterScreen;
                delete.Show();
                this.Close();
            }
            if (e.ClickedItem.Name.Equals("toolStripMenuItem3"))
            {
                // user wants to input their own search engine ID and api key.
                DeveloperPanel panel = new DeveloperPanel();
                panel.StartPosition = FormStartPosition.CenterScreen;
                panel.Show();
                this.Close();
            }
        }

        // Categories that have Ticked set to true are shown as checked.
        private void Form5_Load(object sender, EventArgs e)
        {
            checkedListBox1.DataSource = Program.categories;
            checkedListBox1.DisplayMember = "Name";

            foreach (int i in Enumerable.Range(0, Program.categories.Count))
            {
                if (Program.categories[i].Ticked)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        // We just save the selected categories as true.
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // user selected "Select all"
                foreach (Category category in Program.categories)
                {
                    // set all categories.Ticked to false so that if they were true before and werent ticked this time they are
                    // unticked after clicking button.
                    category.Ticked = true;
                }
            }
            else
            {
                foreach (Category category in Program.categories)
                {
                    // set all categories.Ticked to false so that if they were true before and werent ticked this time they are
                    // unticked after clicking button.
                    category.Ticked = false;
                }
                foreach (Category category in checkedListBox1.CheckedItems)
                {
                    category.Ticked = true;
                }
            }
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConsultationPlus.Forms
{
    public partial class DisclaimerForm : Form
    {
        public DisclaimerForm()
        {
            InitializeComponent();
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            okayButton.Left = (this.ClientSize.Width - okayButton.Width) / 2;
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

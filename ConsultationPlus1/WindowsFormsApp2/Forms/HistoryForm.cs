using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace WindowsFormsApp2
{
    public partial class HistoryForm : Form
    {
        int pageNumber = 1;
        int maxPageNumber = 0;
        List<historyResult> historyResults = new List<historyResult>();
        public List<historyResult> GetHistoryResults()
        {
            return historyResults;
        }
        public void loadResults(List<historyResult> arr)
        {
            historyResults = arr;
        }
        public HistoryForm()
        {
            InitializeComponent();
        }
        private void setVisibilityResults(Boolean flag, int i)
        {
            switch (i)
            {
                case 1:
                    panel1.Visible = flag;
                    goto case 2;
                case 2:
                    panel2.Visible = flag;
                    goto case 3;
                case 3:
                    panel3.Visible = flag;
                    goto case 4;
                case 4:
                    panel4.Visible = flag;
                    goto case 5;
                case 5:
                    panel5.Visible = flag;
                    goto case 6;
                case 6:
                    panel6.Visible = flag;
                    break;
            }
        }
        // Reduces the size of the snippets with adding ... at the end
        private String cutLinkToFit(String linkStr) 
        {
            if (linkStr.Length > 45)
            {
                String str = linkStr.Remove(45, linkStr.Length - 45);
                str = str.Insert(45, "...");
                return str;
            }
            else
                return linkStr;
        }
        private List<historyResult> LoadCurrentDayResults(DateTime date)
        {
            List<historyResult> todayHistoryResults = new List<historyResult>();
            foreach (historyResult hrs in historyResults)
            {
                if (date.Date == hrs.dateTime.Date)
                    todayHistoryResults.Add(hrs);
            }
            return todayHistoryResults;
        }
        private void LoadPanelResults(LinkLabel link, Label date, int lblNumber, List<historyResult> todayHistoryResults)
        {
            int resultNumberByPage = lblNumber - 1 + (pageNumber - 1) * 6;
            link.Text = cutLinkToFit(todayHistoryResults[resultNumberByPage].Link.ToString());
            date.Text = todayHistoryResults[resultNumberByPage].dateTime.TimeOfDay.ToString()
                                                                                            .Remove(8, todayHistoryResults[resultNumberByPage].dateTime.TimeOfDay
                                                                                            .ToString().Length - 8);
        }
        private void DisplayResults(DateTime date)
        {
            setVisibilityResults(false, 1);
            List<historyResult> todayHistoryResults = LoadCurrentDayResults(date);
            if (todayHistoryResults.Count == 0) return;
            setVisibilityResults(true, 1);
            maxPageNumber = todayHistoryResults.Count / 6;
            if (todayHistoryResults.Count % 6 != 0)
                maxPageNumber += 1;
            Boolean ifOnLastPage = (maxPageNumber == pageNumber);
            int resultsLastPage = todayHistoryResults.Count % 6;
            LoadPanelResults(linkLabel1, label1, 1, todayHistoryResults);
            if (resultsLastPage == 1 && ifOnLastPage)
            {
                setVisibilityResults(false, 2);
                return;
            }
            LoadPanelResults(linkLabel2, label2, 2, todayHistoryResults);
            if (resultsLastPage == 2 && pageNumber == maxPageNumber)
            {
                setVisibilityResults(false, 3);
                return;
            }
            LoadPanelResults(linkLabel3, label3, 3, todayHistoryResults);
            if (resultsLastPage == 3 && pageNumber == maxPageNumber)
            {
                setVisibilityResults(false, 4);
                return;
            }
            LoadPanelResults(linkLabel4, label4, 4, todayHistoryResults);
            if (resultsLastPage == 4 && pageNumber == maxPageNumber)
            {
                setVisibilityResults(false, 5);
                return;
            }
            LoadPanelResults(linkLabel5, label5, 5, todayHistoryResults);
            if (resultsLastPage == 5 && pageNumber == maxPageNumber)
            {
                setVisibilityResults(false, 6);
                return;
            }
            LoadPanelResults(linkLabel6, label6, 6, todayHistoryResults);
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (LoadCurrentDayResults(dateTimePicker1.Value).Count == 0)
            {
                MessageBox.Show("No search history for this day!");
                return;
            }
            DisplayResults(dateTimePicker1.Value);
        }
        private void loadPageResults(bool forwards)
        {
            if (forwards) pageNumber += 1;
            else pageNumber -= 1;
            pageNumberLabel.Text = pageNumber.ToString();
            DisplayResults(dateTimePicker1.Value);
        }
        private void backwardsPage_Click(object sender, EventArgs e)
        {
            if (pageNumber > 1)
            {
                loadPageResults(false);
            }
        }

        private void forwardPage_Click(object sender, EventArgs e)
        {
            if (pageNumber < maxPageNumber)
            {
                loadPageResults(true);
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            DisplayResults(DateTime.Now);
        }
        private void linkClicked(int i)
        {
            Program.OpenUrl(LoadCurrentDayResults(dateTimePicker1.Value)[i + (pageNumber - 1) * 6].Link);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(0);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(1);

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(2);

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(3);

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(4);

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(5);

        }
    }
}
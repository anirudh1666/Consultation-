using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WindowsFormsApp2.Forms;
using ConsultationPlus.Properties;

namespace WindowsFormsApp2
{
    public partial class SearchForm : Form
    {
        int maxPageNumber = 0;
        int pageNumber = 1;    //This is the current page number the user is on
        List<Result> results;
        List<historyResult> historyResults = new List<historyResult>();
        public SearchForm()
        {
            InitializeComponent();
        }
        //On load we want to center every visible object
        private void Form1_Load(object sender, EventArgs e)
        {
            pageNumberLabel.Left = (this.ClientSize.Width - pageNumberLabel.Width) / 2;
            searchButton1.Left = (this.ClientSize.Width - searchButton1.Width) / 2;
            logoLabel.Left = ((this.ClientSize.Width - (logoLabel.Width + pictureBox1.Width)) / 2);
            searchBox1.Left = (this.ClientSize.Width - searchBox1.Width) / 2; 
        }
        //This function sets the visibility to the result panels starting from the panel with index i
        //@param flag is the visibility state
        //@param i is starting index
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
                    break;
            }
        }
        // Reduces the size of the snippets with adding ... at the end
        private String cutSnippetToFit(String snippetStr) 
        {
            if (snippetStr.Length > 155)
            {
                String str = snippetStr.Remove(155, snippetStr.Length - 155);
                int lastSpaceOccurence = str.LastIndexOf(" ");
                str = str.Remove(lastSpaceOccurence, str.Length - lastSpaceOccurence).Insert(lastSpaceOccurence, "...");
                return str;
            }
            else
                return snippetStr;
        }
        // This function grayscales an image by averaging its red/blue/green values.
        private Image greyscaleButton(Image img)
        {
            Bitmap bmp = (Bitmap)img;
            Color c;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int k = 0; k < bmp.Height; k++)
                {
                    c = bmp.GetPixel(i, k);
                    int avrg = (c.R + c.G + c.B) / 3;
                    bmp.SetPixel(i, k, Color.FromArgb(c.A, avrg, avrg, avrg));
                }
            }
            return bmp;
        }
        //This function is used for loading a result on a panel
        //@param link,snippet,recommendationsCount - all represent the panel features that are being loaded
        //@param i is the index of the panel
        private void LoadPanelResult(int i, LinkLabel link, Label snippet, Label recommendationsCount)
        {
            int resultNumberByPage = i + (pageNumber - 1) * 4;  //This is the index of the result in the results array calculated using the page
            link.Text = results[resultNumberByPage].Title;
            snippet.Text = cutSnippetToFit(results[resultNumberByPage].Snippet);
            recommendationsCount.Text = results[resultNumberByPage].RecommendationsNumber.ToString();
        }
        //This function is to set the recommendations button visibility
        //If we are on the last page we will have to show the "Not enough links" link
        //@param panel,link,recommend,recommendDown,recommendationsCount - all represent the panel features that are being loaded
        //@param i is the index of the panel
        //@param flag is a boolean that represent if we are not on the "Not enough links" panel
        private void setRecommendationsVisibility(int i,Panel panel, LinkLabel link, Label recommendationsCount, Button recommend, Button recommendDown, bool flag)
        {
            if (!flag)
            {
                link.Font = new Font(link.Font.FontFamily, 16);
                link.Left = (panel.Width - link.Width) / 2;
            }
            else
            {
                link.Font = new Font(link.Font.FontFamily, 9);
                link.Left = panel.Left;
            }
            int resultNumberByPage = i + (pageNumber - 1) * 4;
            if (resultNumberByPage >= results.Count)
                return;
            recommendationsCount.Visible = flag;
            recommend.Visible = flag;
            if (recommend.Visible)
            {
                if (!results[resultNumberByPage].Upvoted)
                    recommend.Image = greyscaleButton(Resources.upvote_icon_11);
                else
                    recommend.Image = Resources.upvote_icon_11;

            }
            recommendDown.Visible = flag;
            if (recommendDown.Visible)
            {
                if (!results[resultNumberByPage].Downvoted)
                    recommendDown.Image = greyscaleButton(Resources.download);
                else
                    recommendDown.Image = Resources.download;
            }
        }
        //Loads all of the results; handles the visibility if we are on last page or have less than 4 results
        private void loadResult()
        {
            setRecommendationsVisibility(0, panel1, link1, recommendationsCount1, recommend1, recommendDown1, true);
            setRecommendationsVisibility(1, panel2, link2, recommendationsCount2, recommend2, recommendDown2, true);
            setRecommendationsVisibility(2, panel3, link3, recommendationsCount3, recommend3, recommendDown3, true);
            setRecommendationsVisibility(3, panel4, link4, recommendationsCount4, recommend4, recommendDown4, true);
            Boolean ifOnLastPage = (maxPageNumber == pageNumber);
            searchBox.Visible = true;
            Search_Button.Visible = true;
            int resultsLastPage = results.Count % 4;
            setVisibilityResults(true, 1);
            LoadPanelResult(0, link1, snippet1, recommendationsCount1);
            if (resultsLastPage == 1 && ifOnLastPage) 
            {
                setRecommendationsVisibility(0, panel1, link1, recommendationsCount1, recommend1, recommendDown1, false);
                setVisibilityResults(false, 2);
                return;
            }
            LoadPanelResult(1, link2, snippet2, recommendationsCount2);
            if (resultsLastPage == 2 && ifOnLastPage)
            {
                setRecommendationsVisibility(1, panel2, link2, recommendationsCount2, recommend2, recommendDown2, false);
                setVisibilityResults(false, 3);
                return;
            }
            LoadPanelResult(2, link3, snippet3, recommendationsCount3);
            if (resultsLastPage == 3 && ifOnLastPage)
            {
                setRecommendationsVisibility(2, panel3, link3, recommendationsCount3, recommend3, recommendDown3, false);
                setVisibilityResults(false, 4);
                return;
            }
            if (resultsLastPage == 0 && ifOnLastPage)
            {
                setRecommendationsVisibility(3, panel4, link4, recommendationsCount4, recommend4, recommendDown4, false);
            }
            LoadPanelResult(3, link4, snippet4, recommendationsCount4);
        }

        //Main menu search button
        private void searchButton1_Click(object sender, EventArgs e)
        {
            if (searchBox1.Text.Length == 0)
            {
                return;
            }
            this.Height = 595;
            backwardsPage.Visible = true;
            forwardPage.Visible = true;
            pageNumberLabel.Visible = true;
            searchButton1.Visible = false;
            searchPanel.Visible = false;
            searchBox1.Visible = false;
            searchBox.Text = searchBox1.Text;
            OnClickSearchButton(sender, e);
        }

        // When the user minimizes the application we hide this and show the system tray icon
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }
        // Function to sort a list
        private void SortList()
        {
            Boolean swapped;
            for (int i = 0; i < results.Count; i++)
            {
                swapped = false;
                for (int j = 0; j < results.Count - i - 1; j++)
                {
                    if (results[j].RecommendationsNumber < results[j + 1].RecommendationsNumber)
                    {
                        Result temp = results[j + 1];
                        results[j + 1] = results[j];
                        results[j] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                    return;
            }
        }
        //This function gets the results from the google search and loops through 
        //the database to set all the results that have been recommended
        private void SetRecommendationsFromDB()
        {
            if (!Program.connected_to_database) { return; }
            foreach (Result result in results)
            {
                result.RecommendationsNumber = DatabaseCommunicator.SetRecommendations(result.Link);
            }
        }
        //This function Loads the results from the google API
        //Sets the recommendations number of any results that have been recommended
        //and loads the results on the main results page
        private void OnClickSearchButton(object sender, EventArgs e)
        {
            if (searchBox.Text.Length == 0)
            {
                return;
            }
            pageNumber = 1;
            pageNumberLabel.Text = "1";
            results = GoogleAPIHandler.Search(searchBox.Text);
            if (Program.connected_to_database)
            {
                SetRecommendationsFromDB();
            }
            SortList();
            if (results[0].Title != "Not enough links?")
            {
                results.Add(new Result
                {
                    Title = "Not enough links?",
                    Snippet = ""
                });
            }
            maxPageNumber = results.Count / 4;
            if (results.Count % 4 != 0)
                maxPageNumber += 1;
            setVisibilityResults(true, 1);
            loadResult();
        }
        private void loadPageResults(bool flag)
        {
            if (flag) pageNumber += 1;
            else pageNumber -= 1;
            pageNumberLabel.Text = pageNumber.ToString();
            loadResult();
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
        //There are 2 cases for this function
        //If the label text is not enough links we open the email form
        //If the label is a title of a result we open the link and saving it to the history results array
        //@param lbl - label clicked
        //@param i - label index
        private void linkClicked(LinkLabel lbl, int i)
        {
            if (lbl.Text.Length > 0)
            {
                if (lbl.Text == "Not enough links?")
                {
                    EmailForm form = new EmailForm();
                    form.Show();
                    return;
                }
                Program.OpenUrl(results[i + (pageNumber - 1) * 4].Link);
                historyResult hsr = new historyResult(link1.Text, results[i + (pageNumber - 1) * 4].Link, DateTime.Now);
                historyResults.Add(hsr);
                if ((Program.connected_to_database) && (Program.save_search))
                {
                    DatabaseCommunicator.SaveURL(lbl.Text);
                }
            }
        }
        private void link1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(link1, 0);
        }

        private void link2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(link2, 1);
        }

        private void link3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(link3, 2);
        }

        private void link4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkClicked(link4, 3);
        }
        public List<historyResult> returnHistoryResults(List<historyResult> arr)
        {
            return historyResults;
        }
        //This function updates the recommendation score of a result whenever a button is clicked
        //It also makes the other button unavailble until the same button is clicked twice
        //@param button - Upvote/Downvote button 
        //@param lbl - recommendations Count label of the result
        //@param i - index of panel
        //@param flag - if true - Upvote if false - downvote
        private void recommend_func(int i, bool flag, Label lbl,Button btn)
        {
            if ((!Program.save_recommendation) || (!Program.connected_to_database)) { return; }
            int resultNumberByPage = i + (pageNumber - 1) * 4;
            if (flag && !results[resultNumberByPage].Downvoted)
            {
                if (results[resultNumberByPage].Upvoted)
                {
                    btn.Image = greyscaleButton(Resources.upvote_icon_11);
                    results[resultNumberByPage].RecommendationsNumber -= 1;
                    results[resultNumberByPage].Upvoted = false;
                }
                else
                {
                    btn.Image = Resources.upvote_icon_11;
                    results[resultNumberByPage].RecommendationsNumber += 1;
                    results[resultNumberByPage].Upvoted = true;
                }
            }
            else if(!flag && !results[resultNumberByPage].Upvoted)
            {
                if (results[resultNumberByPage].Downvoted)
                {
                    btn.Image = greyscaleButton(Resources.download);
                    results[resultNumberByPage].RecommendationsNumber += 1;
                    results[resultNumberByPage].Downvoted = false;
                }
                else
                {
                    btn.Image = Resources.download;
                    results[resultNumberByPage].RecommendationsNumber -= 1;
                    results[resultNumberByPage].Downvoted = true;
                }
            }
            lbl.Text = results[resultNumberByPage].RecommendationsNumber.ToString();
        }
        private void recommend1_Click(object sender, EventArgs e)
        {
            recommend_func(0, true, recommendationsCount1,recommend1);
        }

        private void recommend2_Click(object sender, EventArgs e)
        {
            recommend_func(1, true, recommendationsCount2, recommend2);
        }

        private void recommend3_Click(object sender, EventArgs e)
        {
            recommend_func(2, true, recommendationsCount3, recommend3);
        }

        private void recommend4_Click(object sender, EventArgs e)
        {
            recommend_func(3, true, recommendationsCount4, recommend4);
        }
        private void recommendDown1_Click(object sender, EventArgs e)
        {
            recommend_func(0, false, recommendationsCount1, recommendDown1);
        }
        private void recommendDown2_Click(object sender, EventArgs e)
        {
            recommend_func(1, false, recommendationsCount2, recommendDown2);
        }
        private void recommendDown3_Click(object sender, EventArgs e)
        {
            recommend_func(2, false, recommendationsCount3, recommendDown3);
        }
        private void recommendDown4_Click(object sender, EventArgs e)
        {
            recommend_func(3, false, recommendationsCount4, recommendDown4);
        }
        //Whichever item on the menu toolbar is clicked it does the appropriate action
        private void menu_strip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "history":
                    {
                        if (historyResults.Count == 0)
                        {
                            MessageBox.Show("No history");
                            return;
                        }
                        HistoryForm historyForm = new HistoryForm();
                        historyForm.loadResults(historyResults);
                        historyForm.Show();
                        break;
                    }
                case "whitelist":
                    {
                        WhitelistForm whitelist = new WhitelistForm();
                        whitelist.Show();
                        break;
                    }
                case "privacy":
                    {
                        PrivacyForm privacy = new PrivacyForm();
                        privacy.Show();
                        break;
                    }
                case "dashboard":
                    {
                        Program.OpenUrl("https://consultationplus.herokuapp.com");
                        break;
                    }
                case "about":
                    {
                        Program.OpenUrl("https://reflect.ucl.ac.uk/uclcomp00162020team33/");
                        break;
                    }
                case "help":
                    {
                        break;
                    }
            }
        }
        //Saving the settings to the user's one drive account
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            GraphsAPIHandler.SaveHistory(historyResults);
            GraphsAPIHandler.SaveCategories();
            GraphsAPIHandler.SavePrivacySettings();
            GraphsAPIHandler.SaveCSESettings();
            Application.Exit();
        }
        //These function call the searchbutton function if they are typing and Enter is pressed 
        private void searchBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchButton1_Click(this, new EventArgs());
            }
        }

        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnClickSearchButton(this, new EventArgs());
            }
        }
        //When the user clicks the system tray icon this form appears and the icon becomes invisible
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}

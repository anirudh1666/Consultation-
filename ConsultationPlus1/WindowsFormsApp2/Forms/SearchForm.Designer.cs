
namespace WindowsFormsApp2
{
    partial class SearchForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.searchBox = new System.Windows.Forms.TextBox();
            this.Search_Button = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_strip = new System.Windows.Forms.MenuStrip();
            this.whitelist = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.history = new System.Windows.Forms.ToolStripMenuItem();
            this.privacy = new System.Windows.Forms.ToolStripMenuItem();
            this.help = new System.Windows.Forms.ToolStripMenuItem();
            this.about = new System.Windows.Forms.ToolStripMenuItem();
            this.link1 = new System.Windows.Forms.LinkLabel();
            this.snippet1 = new System.Windows.Forms.Label();
            this.backwardsPage = new System.Windows.Forms.Label();
            this.forwardPage = new System.Windows.Forms.Label();
            this.pageNumberLabel = new System.Windows.Forms.Label();
            this.recommend1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.recommendationsCount1 = new System.Windows.Forms.Label();
            this.recommendDown1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.recommendationsCount2 = new System.Windows.Forms.Label();
            this.link2 = new System.Windows.Forms.LinkLabel();
            this.snippet2 = new System.Windows.Forms.Label();
            this.recommendDown2 = new System.Windows.Forms.Button();
            this.recommend2 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.recommendationsCount3 = new System.Windows.Forms.Label();
            this.link3 = new System.Windows.Forms.LinkLabel();
            this.snippet3 = new System.Windows.Forms.Label();
            this.recommendDown3 = new System.Windows.Forms.Button();
            this.recommend3 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.recommendationsCount4 = new System.Windows.Forms.Label();
            this.link4 = new System.Windows.Forms.LinkLabel();
            this.snippet4 = new System.Windows.Forms.Label();
            this.recommendDown4 = new System.Windows.Forms.Button();
            this.recommend4 = new System.Windows.Forms.Button();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.logoLabel = new System.Windows.Forms.Label();
            this.searchButton1 = new System.Windows.Forms.Button();
            this.searchBox1 = new System.Windows.Forms.TextBox();
            this.menu_strip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.searchBox.Location = new System.Drawing.Point(6, 46);
            this.searchBox.Margin = new System.Windows.Forms.Padding(2);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(467, 34);
            this.searchBox.TabIndex = 1;
            this.searchBox.Visible = false;
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            // 
            // Search_Button
            // 
            this.Search_Button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Search_Button.Location = new System.Drawing.Point(477, 46);
            this.Search_Button.Margin = new System.Windows.Forms.Padding(2);
            this.Search_Button.Name = "Search_Button";
            this.Search_Button.Size = new System.Drawing.Size(87, 34);
            this.Search_Button.TabIndex = 2;
            this.Search_Button.Text = "Search";
            this.Search_Button.UseVisualStyleBackColor = true;
            this.Search_Button.Visible = false;
            this.Search_Button.Click += new System.EventHandler(this.OnClickSearchButton);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            // 
            // menu_strip
            // 
            this.menu_strip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menu_strip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menu_strip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whitelist,
            this.dashboard,
            this.history,
            this.privacy,
            this.help,
            this.about});
            this.menu_strip.Location = new System.Drawing.Point(0, 0);
            this.menu_strip.Name = "menu_strip";
            this.menu_strip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menu_strip.Size = new System.Drawing.Size(573, 36);
            this.menu_strip.TabIndex = 9;
            this.menu_strip.Text = "menuStrip1";
            this.menu_strip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_strip_ItemClicked);
            // 
            // whitelist
            // 
            this.whitelist.Name = "whitelist";
            this.whitelist.Size = new System.Drawing.Size(103, 32);
            this.whitelist.Text = "Whitelist";
            // 
            // dashboard
            // 
            this.dashboard.DoubleClickEnabled = true;
            this.dashboard.Name = "dashboard";
            this.dashboard.Size = new System.Drawing.Size(122, 32);
            this.dashboard.Text = "Dashboard";
            // 
            // history
            // 
            this.history.CheckOnClick = true;
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(89, 32);
            this.history.Text = "History";
            // 
            // privacy
            // 
            this.privacy.Name = "privacy";
            this.privacy.Size = new System.Drawing.Size(88, 32);
            this.privacy.Text = "Privacy";
            // 
            // help
            // 
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(67, 32);
            this.help.Text = "Help";
            // 
            // about
            // 
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(81, 32);
            this.about.Text = "About";
            // 
            // link1
            // 
            this.link1.AutoSize = true;
            this.link1.Location = new System.Drawing.Point(4, 4);
            this.link1.Name = "link1";
            this.link1.Size = new System.Drawing.Size(40, 20);
            this.link1.TabIndex = 11;
            this.link1.TabStop = true;
            this.link1.Text = "link1";
            this.link1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link1_LinkClicked);
            // 
            // snippet1
            // 
            this.snippet1.Location = new System.Drawing.Point(6, 35);
            this.snippet1.Name = "snippet1";
            this.snippet1.Size = new System.Drawing.Size(512, 45);
            this.snippet1.TabIndex = 12;
            this.snippet1.Text = "snippet1";
            // 
            // backwardsPage
            // 
            this.backwardsPage.AutoSize = true;
            this.backwardsPage.Location = new System.Drawing.Point(228, 520);
            this.backwardsPage.Name = "backwardsPage";
            this.backwardsPage.Size = new System.Drawing.Size(29, 20);
            this.backwardsPage.TabIndex = 28;
            this.backwardsPage.Text = "<<";
            this.backwardsPage.Visible = false;
            this.backwardsPage.Click += new System.EventHandler(this.backwardsPage_Click);
            // 
            // forwardPage
            // 
            this.forwardPage.AutoSize = true;
            this.forwardPage.Location = new System.Drawing.Point(313, 520);
            this.forwardPage.Name = "forwardPage";
            this.forwardPage.Size = new System.Drawing.Size(29, 20);
            this.forwardPage.TabIndex = 30;
            this.forwardPage.Text = ">>";
            this.forwardPage.Visible = false;
            this.forwardPage.Click += new System.EventHandler(this.forwardPage_Click);
            // 
            // pageNumberLabel
            // 
            this.pageNumberLabel.AutoSize = true;
            this.pageNumberLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.pageNumberLabel.Location = new System.Drawing.Point(277, 520);
            this.pageNumberLabel.Name = "pageNumberLabel";
            this.pageNumberLabel.Size = new System.Drawing.Size(18, 20);
            this.pageNumberLabel.TabIndex = 31;
            this.pageNumberLabel.Text = "1";
            this.pageNumberLabel.Visible = false;
            // 
            // recommend1
            // 
            this.recommend1.Image = ((System.Drawing.Image)(resources.GetObject("recommend1.Image")));
            this.recommend1.Location = new System.Drawing.Point(522, 4);
            this.recommend1.Name = "recommend1";
            this.recommend1.Size = new System.Drawing.Size(30, 28);
            this.recommend1.TabIndex = 32;
            this.recommend1.UseVisualStyleBackColor = true;
            this.recommend1.Click += new System.EventHandler(this.recommend1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.recommendationsCount1);
            this.panel1.Controls.Add(this.link1);
            this.panel1.Controls.Add(this.snippet1);
            this.panel1.Controls.Add(this.recommendDown1);
            this.panel1.Controls.Add(this.recommend1);
            this.panel1.Location = new System.Drawing.Point(6, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 95);
            this.panel1.TabIndex = 36;
            this.panel1.Visible = false;
            // 
            // recommendationsCount1
            // 
            this.recommendationsCount1.AutoSize = true;
            this.recommendationsCount1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.recommendationsCount1.Location = new System.Drawing.Point(524, 35);
            this.recommendationsCount1.Name = "recommendationsCount1";
            this.recommendationsCount1.Size = new System.Drawing.Size(0, 20);
            this.recommendationsCount1.TabIndex = 33;
            // 
            // recommendDown1
            // 
            this.recommendDown1.Image = ((System.Drawing.Image)(resources.GetObject("recommendDown1.Image")));
            this.recommendDown1.Location = new System.Drawing.Point(522, 61);
            this.recommendDown1.Name = "recommendDown1";
            this.recommendDown1.Size = new System.Drawing.Size(30, 28);
            this.recommendDown1.TabIndex = 32;
            this.recommendDown1.UseVisualStyleBackColor = true;
            this.recommendDown1.Click += new System.EventHandler(this.recommendDown1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.recommendationsCount2);
            this.panel2.Controls.Add(this.link2);
            this.panel2.Controls.Add(this.snippet2);
            this.panel2.Controls.Add(this.recommendDown2);
            this.panel2.Controls.Add(this.recommend2);
            this.panel2.Location = new System.Drawing.Point(6, 193);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(562, 95);
            this.panel2.TabIndex = 36;
            this.panel2.Visible = false;
            // 
            // recommendationsCount2
            // 
            this.recommendationsCount2.AutoSize = true;
            this.recommendationsCount2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.recommendationsCount2.Location = new System.Drawing.Point(524, 35);
            this.recommendationsCount2.Name = "recommendationsCount2";
            this.recommendationsCount2.Size = new System.Drawing.Size(0, 20);
            this.recommendationsCount2.TabIndex = 33;
            // 
            // link2
            // 
            this.link2.AutoSize = true;
            this.link2.Location = new System.Drawing.Point(4, 4);
            this.link2.Name = "link2";
            this.link2.Size = new System.Drawing.Size(40, 20);
            this.link2.TabIndex = 11;
            this.link2.TabStop = true;
            this.link2.Text = "link1";
            this.link2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link2_LinkClicked);
            // 
            // snippet2
            // 
            this.snippet2.Location = new System.Drawing.Point(4, 37);
            this.snippet2.Name = "snippet2";
            this.snippet2.Size = new System.Drawing.Size(512, 45);
            this.snippet2.TabIndex = 12;
            this.snippet2.Text = "snippet1";
            // 
            // recommendDown2
            // 
            this.recommendDown2.Image = ((System.Drawing.Image)(resources.GetObject("recommendDown2.Image")));
            this.recommendDown2.Location = new System.Drawing.Point(522, 61);
            this.recommendDown2.Name = "recommendDown2";
            this.recommendDown2.Size = new System.Drawing.Size(30, 28);
            this.recommendDown2.TabIndex = 32;
            this.recommendDown2.UseVisualStyleBackColor = true;
            this.recommendDown2.Click += new System.EventHandler(this.recommendDown2_Click);
            // 
            // recommend2
            // 
            this.recommend2.Image = ((System.Drawing.Image)(resources.GetObject("recommend2.Image")));
            this.recommend2.Location = new System.Drawing.Point(522, 4);
            this.recommend2.Name = "recommend2";
            this.recommend2.Size = new System.Drawing.Size(30, 28);
            this.recommend2.TabIndex = 32;
            this.recommend2.UseVisualStyleBackColor = true;
            this.recommend2.Click += new System.EventHandler(this.recommend2_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.recommendationsCount3);
            this.panel3.Controls.Add(this.link3);
            this.panel3.Controls.Add(this.snippet3);
            this.panel3.Controls.Add(this.recommendDown3);
            this.panel3.Controls.Add(this.recommend3);
            this.panel3.Location = new System.Drawing.Point(6, 296);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(562, 95);
            this.panel3.TabIndex = 36;
            this.panel3.Visible = false;
            // 
            // recommendationsCount3
            // 
            this.recommendationsCount3.AutoSize = true;
            this.recommendationsCount3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.recommendationsCount3.Location = new System.Drawing.Point(524, 35);
            this.recommendationsCount3.Name = "recommendationsCount3";
            this.recommendationsCount3.Size = new System.Drawing.Size(0, 20);
            this.recommendationsCount3.TabIndex = 33;
            // 
            // link3
            // 
            this.link3.AutoSize = true;
            this.link3.Location = new System.Drawing.Point(4, 4);
            this.link3.Name = "link3";
            this.link3.Size = new System.Drawing.Size(40, 20);
            this.link3.TabIndex = 11;
            this.link3.TabStop = true;
            this.link3.Text = "link1";
            this.link3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link3_LinkClicked);
            // 
            // snippet3
            // 
            this.snippet3.Location = new System.Drawing.Point(6, 35);
            this.snippet3.Name = "snippet3";
            this.snippet3.Size = new System.Drawing.Size(512, 45);
            this.snippet3.TabIndex = 12;
            this.snippet3.Text = "snippet1";
            // 
            // recommendDown3
            // 
            this.recommendDown3.Image = ((System.Drawing.Image)(resources.GetObject("recommendDown3.Image")));
            this.recommendDown3.Location = new System.Drawing.Point(522, 61);
            this.recommendDown3.Name = "recommendDown3";
            this.recommendDown3.Size = new System.Drawing.Size(30, 28);
            this.recommendDown3.TabIndex = 32;
            this.recommendDown3.UseVisualStyleBackColor = true;
            this.recommendDown3.Click += new System.EventHandler(this.recommendDown3_Click);
            // 
            // recommend3
            // 
            this.recommend3.Image = ((System.Drawing.Image)(resources.GetObject("recommend3.Image")));
            this.recommend3.Location = new System.Drawing.Point(522, 4);
            this.recommend3.Name = "recommend3";
            this.recommend3.Size = new System.Drawing.Size(30, 28);
            this.recommend3.TabIndex = 32;
            this.recommend3.UseVisualStyleBackColor = true;
            this.recommend3.Click += new System.EventHandler(this.recommend3_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.Controls.Add(this.recommendationsCount4);
            this.panel4.Controls.Add(this.link4);
            this.panel4.Controls.Add(this.snippet4);
            this.panel4.Controls.Add(this.recommendDown4);
            this.panel4.Controls.Add(this.recommend4);
            this.panel4.Location = new System.Drawing.Point(6, 400);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(562, 95);
            this.panel4.TabIndex = 36;
            this.panel4.Visible = false;
            // 
            // recommendationsCount4
            // 
            this.recommendationsCount4.AutoSize = true;
            this.recommendationsCount4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.recommendationsCount4.Location = new System.Drawing.Point(524, 35);
            this.recommendationsCount4.Name = "recommendationsCount4";
            this.recommendationsCount4.Size = new System.Drawing.Size(0, 20);
            this.recommendationsCount4.TabIndex = 33;
            // 
            // link4
            // 
            this.link4.AutoSize = true;
            this.link4.Location = new System.Drawing.Point(4, 4);
            this.link4.Name = "link4";
            this.link4.Size = new System.Drawing.Size(40, 20);
            this.link4.TabIndex = 11;
            this.link4.TabStop = true;
            this.link4.Text = "link1";
            this.link4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link4_LinkClicked);
            // 
            // snippet4
            // 
            this.snippet4.Location = new System.Drawing.Point(4, 35);
            this.snippet4.Name = "snippet4";
            this.snippet4.Size = new System.Drawing.Size(512, 45);
            this.snippet4.TabIndex = 12;
            this.snippet4.Text = "snippet1";
            // 
            // recommendDown4
            // 
            this.recommendDown4.Image = ((System.Drawing.Image)(resources.GetObject("recommendDown4.Image")));
            this.recommendDown4.Location = new System.Drawing.Point(522, 61);
            this.recommendDown4.Name = "recommendDown4";
            this.recommendDown4.Size = new System.Drawing.Size(30, 28);
            this.recommendDown4.TabIndex = 32;
            this.recommendDown4.UseVisualStyleBackColor = true;
            this.recommendDown4.Click += new System.EventHandler(this.recommendDown4_Click);
            // 
            // recommend4
            // 
            this.recommend4.Image = ((System.Drawing.Image)(resources.GetObject("recommend4.Image")));
            this.recommend4.Location = new System.Drawing.Point(522, 4);
            this.recommend4.Name = "recommend4";
            this.recommend4.Size = new System.Drawing.Size(30, 28);
            this.recommend4.TabIndex = 32;
            this.recommend4.UseVisualStyleBackColor = true;
            this.recommend4.Click += new System.EventHandler(this.recommend4_Click);
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.pictureBox1);
            this.searchPanel.Controls.Add(this.logoLabel);
            this.searchPanel.Controls.Add(this.searchButton1);
            this.searchPanel.Controls.Add(this.searchBox1);
            this.searchPanel.Location = new System.Drawing.Point(0, 38);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(2);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(573, 237);
            this.searchPanel.TabIndex = 37;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(396, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // logoLabel
            // 
            this.logoLabel.AutoSize = true;
            this.logoLabel.Font = new System.Drawing.Font("Bahnschrift Light", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.logoLabel.Location = new System.Drawing.Point(134, 43);
            this.logoLabel.Name = "logoLabel";
            this.logoLabel.Size = new System.Drawing.Size(270, 53);
            this.logoLabel.TabIndex = 4;
            this.logoLabel.Text = "Consultation";
            // 
            // searchButton1
            // 
            this.searchButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.searchButton1.Location = new System.Drawing.Point(157, 167);
            this.searchButton1.Margin = new System.Windows.Forms.Padding(2);
            this.searchButton1.Name = "searchButton1";
            this.searchButton1.Size = new System.Drawing.Size(257, 45);
            this.searchButton1.TabIndex = 1;
            this.searchButton1.Text = "Search whitelisted results";
            this.searchButton1.UseVisualStyleBackColor = true;
            this.searchButton1.Click += new System.EventHandler(this.searchButton1_Click);
            // 
            // searchBox1
            // 
            this.searchBox1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.searchBox1.Location = new System.Drawing.Point(70, 109);
            this.searchBox1.Margin = new System.Windows.Forms.Padding(2);
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.Size = new System.Drawing.Size(441, 41);
            this.searchBox1.TabIndex = 0;
            this.searchBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox1_KeyDown);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(573, 549);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pageNumberLabel);
            this.Controls.Add(this.forwardPage);
            this.Controls.Add(this.backwardsPage);
            this.Controls.Add(this.menu_strip);
            this.Controls.Add(this.Search_Button);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.searchPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu_strip;
            this.MaximizeBox = false;
            this.Name = "SearchForm";
            this.Text = "Consultation+";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menu_strip.ResumeLayout(false);
            this.menu_strip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button Search_Button;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.MenuStrip menu_strip;
        private System.Windows.Forms.ToolStripMenuItem history;
        private System.Windows.Forms.LinkLabel link1;
        private System.Windows.Forms.Label snippet1;
        private System.Windows.Forms.Label backwardsPage;
        private System.Windows.Forms.Label forwardPage;
        private System.Windows.Forms.Label pageNumberLabel;
        private System.Windows.Forms.Button recommend1;
        private System.Windows.Forms.ToolStripMenuItem dashboard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel link2;
        private System.Windows.Forms.Label snippet2;
        private System.Windows.Forms.Button recommend2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel link3;
        private System.Windows.Forms.Label snippet3;
        private System.Windows.Forms.Button recommend3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel link4;
        private System.Windows.Forms.Label snippet4;
        private System.Windows.Forms.Button recommend4;
        private System.Windows.Forms.Button searchButton1;
        private System.Windows.Forms.TextBox searchBox1;
        private System.Windows.Forms.ToolStripMenuItem whitelist;
        private System.Windows.Forms.Label recommendationsCount1;
        private System.Windows.Forms.Button recommendDown1;
        private System.Windows.Forms.Label recommendationsCount2;
        private System.Windows.Forms.Button recommendDown2;
        private System.Windows.Forms.Label recommendationsCount3;
        private System.Windows.Forms.Button recommendDown3;
        private System.Windows.Forms.Label recommendationsCount4;
        private System.Windows.Forms.Button recommendDown4;
        private System.Windows.Forms.ToolStripMenuItem privacy;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label logoLabel;
        private System.Windows.Forms.ToolStripMenuItem help;
        private System.Windows.Forms.ToolStripMenuItem about;
        public System.Windows.Forms.Panel searchPanel;
    }
}


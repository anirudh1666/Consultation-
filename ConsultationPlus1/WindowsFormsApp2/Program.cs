using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using Microsoft.Identity.Client;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Directory = System.IO.Directory;
using Process = System.Diagnostics.Process;
using System.Runtime.InteropServices;

namespace WindowsFormsApp2
{
    /** This class handles the running of the application and calling relevant functions.
    */
    public static class Program
    {
        public static List<Category> categories = new List<Category>();
        public static string[] whitelist;
        public static bool save_search = false;
        public static bool save_recommendation = false;
        public static bool connected_to_database;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            DatabaseCommunicator.CheckIfConnected();
            System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            LoginForm form = new LoginForm();
            form.StartPosition = FormStartPosition.CenterScreen;
            System.Windows.Forms.Application.Run(form);
            form.Close();
        }


        /** Auxiliary function to help send url to database. We don't want to send
         *  the exact url the user clicked but rather the whitelisted domain name that the user clicked on.
         *  @param url is the url of the link that the user clicked on.
         *  @return is the domain name of the whitelisted website that the user clicked on.
         */
        public static string GetWhitelistURL(string url)
        {
            // using Uri we are able to parse the url and obtain the domain name.
            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch (System.UriFormatException)
            {
                return "";
            }
            return uri.Host;
        }

        /** Loads whitelist.txt file that is in Resources folder. The file is a csv file of the current websites in the custom search
         *  engine. To add or delete websites modify the text file and the engine from Google's control panel. 
         */
        public static void LoadWhitelist()
        {
            string[] file = System.IO.Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "../../../../", "*.txt", SearchOption.AllDirectories);
            string text = System.IO.File.ReadAllText(file[0]);
            whitelist = text.Split(',');
        }

        /** Allows users to create their own category. When we build google query we specify which sites
         *  to exclude so we look through our whitelist and add all sites not in sites_to_include to 
         *  sites_to_exclude. Then using name, sites_to_exclude and true we create a category and add it
         *  to Program.categories.
         *  @param name is the name of the category.
         *         sites_to_include is the list of sites we want results from.
         */
        public static void CreateCategory(string name, List<string> sites_to_include)
        {
            List<string> sites_to_exclude = new List<string>();
            foreach (string site in whitelist)
            {
                if (!sites_to_include.Contains(site))
                {
                    sites_to_exclude.Add(site);
                }
            }
            categories.Add(new Category(name, sites_to_exclude, true));
        }
        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else
                {
                    throw;
                }
            }
        }
    }

    public class Result
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Snippet { get; set; }
        public string Source { get; set; }
        public string Query { get; set; }
        public string Index { get; set; }
        public int RecommendationsNumber { get; set; }
        public Boolean Upvoted { get; set; }
        public Boolean Downvoted { get; set; }
    }

    public class historyResult
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime dateTime { get; set; }
        public historyResult(string title, string link, DateTime datetime)
        {
            Title = title;
            Link = link;
            dateTime = datetime;
        }
    }

    public class Category
    {
        public string Name { get; set; }
        public List<string> Sites_to_exclude { get; set; }
        public bool Ticked { get; set; }
        public Category(string name, List<string> sites, bool ticked)
        {
            Name = name;
            Sites_to_exclude = sites;
            Ticked = ticked;
        }
    }
}
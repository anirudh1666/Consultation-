using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public static class GraphsAPIHandler
    {
        public static GraphServiceClient graphClient;
        private static string email;

        /** Logs the user into their Microsoft Live account using Microsoft Graphs API.
         *  To increase the number of permissions add to scopes array. Right now our graphs
         *  client has user email access and read/write permission to users onedrive.
         */
        public static async void Login()
        {
            var clientId = "1912fef8-e092-4702-8012-cb3e535138a8";        // microsoft graphs api ID.
            var app = PublicClientApplicationBuilder.Create(clientId)
                                                    .WithRedirectUri("http://localhost")
                                                    .Build();
            string[] scopes = new string[]
            {
                // add more scopes here if you want more access to users data using graphs api.
                "https://graph.microsoft.com/user.read",
                "https://graph.microsoft.com/email",
                "https://graph.microsoft.com/Files.ReadWrite.AppFolder"
            };
            var result = await app.AcquireTokenInteractive(scopes)
                            .ExecuteAsync();

            InteractiveAuthenticationProvider authProvider = new InteractiveAuthenticationProvider(app, scopes);
            graphClient = new GraphServiceClient(authProvider);
            var user = await graphClient.Me.Request().GetAsync();
            email = user.Mail;
            DatabaseCommunicator.email = user.Mail;
            if (Program.connected_to_database)
            {
                if (!DatabaseCommunicator.CheckRegistration(email))
                {
                    // show registration box here.
                    RegisterForm register = new RegisterForm(email);
                    register.StartPosition = FormStartPosition.CenterScreen;
                    register.Show();
                }
                else
                {
                    // User is already registered so we don't show registration.
                    // get all the ticked whitelist categories. If it is first time user then we show messagebox asking to select categories.
                    LoadCSESettings();
                    LoadPrivacySettings();
                    Program.LoadWhitelist();
                    LoadCategories();
                }
            }
            else
            {
                // App isn't connected to database so we don't need to check if user is registered or not.
                LoadCSESettings();
                LoadPrivacySettings();
                Program.LoadWhitelist();
                LoadCategories();
            }
        }

        /** Auxiliary function to help save data to users onedrive. 
         * @param found is true if file already exists else false.
         *        content is a json object in string form. We save this content to OneDrive.
         *        file_name is the name of the file where we save content.
         */
        private async static void Save(string content, string file_name)
        {
            using var n_stream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(content));
            await graphClient.Me.Drive.Special.AppRoot.ItemWithPath(file_name).Content.Request().PutAsync<DriveItem>(n_stream);
        }

        /** This saves the links that the user clicked on to their onedrive in JSON format. If user
         *  doesn't have the file or is a first time user we create it for them and save their history.
         *  This function doesn't overwrite file history it appends to existing file. However, once the
         *  history is > 4MB this function no longer works.
         *  @param historyResults contains a list of links that the user clicked on.
         *         fileName is optional and is used for testing so we can create a test folder named test_history.txt
         */
        public async static void SaveHistory(List<historyResult> historyResults, string fileName = "history.csv")
        {
            var search = await graphClient.Me.Drive.Special.AppRoot.Search(fileName).Request().GetAsync();
            string str;
            if (search.Count() == 1)
            {
                var stream = await graphClient.Me.Drive.Special.AppRoot.ItemWithPath(fileName).Content.Request().GetAsync();
                StreamReader reader = new StreamReader(stream);
                str = reader.ReadToEnd();
                var overwrite_str = "";
                foreach (historyResult result in historyResults)
                {
                    str += result.Link + "," + result.dateTime + "\n";
                    overwrite_str += result.Link + "," + result.dateTime + "\n";
                }
                if (Encoding.UTF8.GetByteCount(str) > 4000000)
                {
                    // file size over 4 MB we overwrite rather than append.
                    str = overwrite_str;

                }
            }
            else
            {
                str = "url,time\n";
                foreach (historyResult result in historyResults)
                {
                    str += result.Link + "," + result.dateTime + "\n";
                }
            }
            Save(str, "history.csv");
        }

        /** Used to save users privacy settings to their onedrive. It is saved as a csv file with the first element representing save_history_flag
         *  and second element representing recommendations_flag.
         */
        public async static void SavePrivacySettings()
        {
            var search = await graphClient.Me.Drive.Special.AppRoot.Search("privacy.txt").Request().GetAsync();
            string content = Program.save_search + "," + Program.save_recommendation;
            Save(content, "privacy.txt");
        }

        /** Used to save users category settings to OneDrive. The file that contains the categories is called Consultation_plus_categories.txt
         *  Using the graphClient it first searches to see if the file already exists in users OneDrive. If it doesnt then we create it otherwise
         *  we just overwrite the existing contents of the file.
         *  @param fileName is optional and mainly there to allowing testing by creating test_categories.txt rather than categories.txt
         */
        public async static void SaveCategories(string fileName = "categories.txt")
        {
            var search = await graphClient.Me.Drive.Special.AppRoot.Search(fileName).Request().GetAsync();
            string json = JsonConvert.SerializeObject(Program.categories);
            Save(json, fileName);
        }

        /** Saves the api key and search engine ID the user used in developer mode. If they didn't use it then we just store the
         *  keys.
         */
        public async static void SaveCSESettings()
        {
            var search = await graphClient.Me.Drive.Special.AppRoot.Search("cse_settings.txt").Request().GetAsync();
            Dictionary<string, string> cse_settings = new Dictionary<string, string>();
            cse_settings.Add("api_key", GoogleAPIHandler.api_key);
            cse_settings.Add("search_engine_id", GoogleAPIHandler.search_engine_id);
            string json = JsonConvert.SerializeObject(cse_settings);
            Save(json, "cse_settings.txt");
        }

        /** This loads the developer mode api key and search engine ID. It only stores the previous sessions keys.
         */
        public async static void LoadCSESettings()
        {
            try
            {
                var stream = await graphClient.Me.Drive.Special.AppRoot.ItemWithPath("cse_settings.txt").Content.Request().GetAsync();
                StreamReader reader = new StreamReader(stream);
                dynamic json = JsonConvert.DeserializeObject(reader.ReadToEnd());
                GoogleAPIHandler.api_key = json.api_key;
                GoogleAPIHandler.search_engine_id = json.search_engine_id;
            }
            catch (Microsoft.Graph.ServiceException)
            {
                GoogleAPIHandler.api_key = "AIzaSyDsKUb3ow6IelFWznCZlOJOq7erSWsyA_Y";
                GoogleAPIHandler.search_engine_id = "2e74ce5a7201c44de";
            }
        }

        /** Used to load the users privacy settings from their previous session. If it wasn't found we set everything to false.
         */
        public async static void LoadPrivacySettings()
        {
            try
            {
                var stream = await graphClient.Me.Drive.Special.AppRoot.ItemWithPath("privacy.txt").Content.Request().GetAsync();
                StreamReader reader = new StreamReader(stream);
                string[] content = reader.ReadToEnd().Split(',');
                if (content[0] == "True")
                {
                    Program.save_search = true;
                }
                else
                {
                    Program.save_search = false;
                }
                if (content[0] == "True")
                {
                    Program.save_recommendation = true;
                }
                else
                {
                    Program.save_recommendation = false;
                }
            }
            catch (Microsoft.Graph.ServiceException)
            {
                Program.save_search = false;
                Program.save_recommendation = false;
            }
        }

        /** Loads the users category settings from their OneDrive. Searches for a file named Consultation_plus_categories.txt. if it doesnt
        *  find a file it loads in predefined categories. The user can add more categories that they themselves create. If the file is found
        *  we simply just load their categories in.
        */
        public async static void LoadCategories()
        {
            try
            {
                var stream = await graphClient.Me.Drive.Special.AppRoot.ItemWithPath("categories.txt").Content.Request().GetAsync();
                // We found a file with categories in them so we load those.
                StreamReader reader = new StreamReader(stream);
                dynamic obj = JsonConvert.DeserializeObject(reader.ReadToEnd());

                foreach (var item in obj)
                {
                    List<string> temp = new List<string>();
                    foreach (string site in item.GetValue("Sites_to_exclude"))
                    {
                        temp.Add(site);
                    }
                    Program.categories.Add(new Category(item.Name.ToString(), temp, (bool)item.Ticked));
                }
            }
            catch (Microsoft.Graph.ServiceException)
            {
                // Load predefined categories.
                // make it so it loads it in from a text files
                List<Category> cat = new List<Category>()
                {
                    new Category("General Practice (Recommended)", new List<string>() {
                        "www.bad.org.uk",
                        "www.pcds.org.uk",
                        "www.ukdctn.org",
                        "bdng.org.uk",
                        "www.aan.com",
                        "www.thebrainmatters.org",
                        "www.neuroguide.com",
                        "www.wfneurology.org",
                        "www.neurology.co.in",
                        "aaa.org",
                        "apexcardiology.com",
                        "bhvci.com",
                        "pacificheart.com",
                        "gpnotebook.com",
                        "bnf.nice.org.uk",
                        "bnfc.nice.org.uk",
                        "products.mhra.gov.uk",
                        "www.hee.nhs.uk",
                        "www.sign.ac.uk",
                        "www.nhs.uk",
                        "rcgp.org.uk"},
                        true),
                    new Category("Dermatology", new List<string>() {
                        "healthline.com",
                        "NIH.gov",
                        "CDC.gov",
                        "drugs.com",
                        "WHO.int",
                        "medlineplus.gov",
                        "hopkinsmedicine.org",
                        "www.aan.com",
                        "www.thebrainmatters.org",
                        "www.neuroguide.com",
                        "www.wfneurology.org",
                        "www.neurology.co.in",
                        "aaa.org",
                        "apexcardiology.com",
                        "bhvci.com",
                        "pacificheart.com",
                        "gpnotebook.com",
                        "bnf.nice.org.uk",
                        "bnfc.nice.org.uk",
                        "products.mhra.gov.uk",
                        "www.hee.nhs.uk",
                        "www.sign.ac.uk",
                        "www.nhs.uk",
                        "rcgp.org.uk"},
                        true),
                    new Category("Neurology", new List<string>() {
                        "healthline.com",
                        "NIH.gov",
                        "CDC.gov",
                        "drugs.com",
                        "WHO.int",
                        "medlineplus.gov",
                        "hopkinsmedicine.org",
                        "www.bad.org.uk",
                        "www.pcds.org.uk",
                        "www.ukdctn.org",
                        "bdng.org.uk",
                        "aaa.org",
                        "apexcardiology.com",
                        "bhvci.com",
                        "pacificheart.com",
                        "gpnotebook.com",
                        "bnf.nice.org.uk",
                        "bnfc.nice.org.uk",
                        "products.mhra.gov.uk",
                        "www.hee.nhs.uk",
                        "www.sign.ac.uk",
                        "www.nhs.uk",
                        "rcgp.org.uk",
                        "www.aan.com"},
                        true),
                    new Category("Cardiology", new List<string>()
                    {
                        "healthline.com",
                        "NIH.gov",
                        "CDC.gov",
                        "drugs.com",
                        "WHO.int",
                        "medlineplus.gov",
                        "hopkinsmedicine.org",
                        "www.bad.org.uk",
                        "www.pcds.org.uk",
                        "www.ukdctn.org",
                        "bdng.org.uk",
                        "www.aan.com",
                        "www.thebrainmatters.org",
                        "www.neuroguide.com",
                        "www.wfneurology.org",
                        "www.neurology.co.in",
                        "gpnotebook.com",
                        "bnf.nice.org.uk",
                        "bnfc.nice.org.uk",
                        "products.mhra.gov.uk",
                        "www.hee.nhs.uk",
                        "www.sign.ac.uk",
                        "www.nhs.uk",
                        "rcgp.org.uk"}, 
                        true)
                };
                Program.categories = cat;
            }
            SearchForm form = new SearchForm();
            form.StartPosition = FormStartPosition.Manual;
            int oldFormHeight = form.Height;
            form.Height = form.Top - form.searchPanel.Bottom;
            form.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - form.Width,
                                   Screen.PrimaryScreen.WorkingArea.Height - oldFormHeight);
            form.Show();
        }
    }
}

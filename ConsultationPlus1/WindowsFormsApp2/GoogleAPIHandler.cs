using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    /** This class is responsible for performing searches using Googles Custom Search Engine (CSE) API.
     *  To set up the CSE API you have to create it online and access it from the control panel online. In the control panel,
     *  you should enter all the files in whitelist.txt and enter the search engine ID into the variable cx and
     *  the api key into apiKey. Once this is done this class should successfully perform searches.
     */
    public static class GoogleAPIHandler
    {
        public static string default_search_engine_id = "2e74ce5a7201c44de";
        public static string default_api_key = "AIzaSyDsKUb3ow6IelFWznCZlOJOq7erSWsyA_Y";
        public static bool developer_mode = false;
        public static string search_engine_id;
        public static string api_key;
        /** For each ticked category it searches that query for that whitelist. You can tick
         *  multiple categories. It also removes overlapping results. If no categories are ticked
         *  we should Nothing Found. The way this works is that it has to add sites to EXCLUDE in the 
         *  request. So we need to do some preprocessing to obtain the sites to exclude.
         *  @param query is the search query e.g. "kidney disease".
         *  @return list of results from google
         */
        public static List<Result> Search(string query)
        {
            if (query == "")
            {
                // user hasnt typed in anything
                return new List<Result>()
                {
                    new Result
                    {
                        Title = "Not enough links?",
                        Snippet = "",
                    }
                };
            }

            dynamic results;
            if (developer_mode)
            {
                string q = "https://www.googleapis.com/customsearch/v1?key=" + api_key + "&cx=" + search_engine_id + "&q=" + query;
                results = SendRequest(q);
            }
            else
            {
                string q = "https://www.googleapis.com/customsearch/v1?key=" + default_api_key + "&cx=" + default_search_engine_id + "&q=" + query;
                bool at_least_one_ticked = false;
                List<Category> ticked = new List<Category>();
                foreach (Category elem in Program.categories)
                {
                    // For each ticked category we add the site to exclude to list of sites then build query.
                    if (elem.Ticked)
                    {
                        ticked.Add(elem);
                        at_least_one_ticked = true;
                    }
                }

                if ((at_least_one_ticked == false))
                {
                    // User hasn't selected any categories so there is nothing to display
                    return new List<Result>()
                {
                    new Result
                    {
                        Title = "Not enough links?",
                        Snippet = "",
                    }
                };
                }
                List<string> sites_to_include = new List<string>();
                foreach (Category elem in ticked)
                {
                    // we get all the sites that each category wants to include and save them.
                    IEnumerable<string> diff = Program.whitelist.Except(elem.Sites_to_exclude);
                    foreach (string site in diff)
                    {
                        if (!sites_to_include.Contains(site))
                        {
                            sites_to_include.Add(site);
                        }
                    }
                }

                IEnumerable<string> sites_to_exclude = Program.whitelist.Except(sites_to_include);
                if (sites_to_exclude.Count() != 0)
                {
                    // we specify sites to exclude in query so we take difference
                    q += "&siteSearch=";
                    foreach (int i in Enumerable.Range(0, sites_to_exclude.Count() - 1))
                    {
                        q += sites_to_exclude.ElementAt(i) + " ";
                    }
                    q += sites_to_exclude.ElementAt(sites_to_exclude.Count() - 1) + "&siteSearchFilter=e";
                }
                results = SendRequest(q);
            }
            var ret = CreateResults(results);
            // we save each query to database.
            if (Program.save_search && Program.connected_to_database)
            {
                DatabaseCommunicator.SaveQuery(query);
            }
            return ret;
        }

        private static List<Result> CreateResults(dynamic results)
        {
            List<Result> ret = new List<Result>();
            try
            {
                foreach (int i in Enumerable.Range(0, 10))
                {
                    var result = new Result
                    {
                        Title = results.items[i].title,
                        Link = results.items[i].link,
                        Snippet = results.items[i].snippet,
                    };
                    ret.Add(result);
                }
            }
            catch (Exception)
            {
                ret.Add(new Result
                {
                    Title = "Not enough links?",
                    Snippet = "",
                });
            }
            return ret;
        }

        /** Sends a GET request to google to get results.
        * @param query is a fully formed query not the same as the one user typed. 
        * @return an object that contains the search results that google sent back.
        */
        private static dynamic SendRequest(string url)
        {
            var request = WebRequest.Create(url);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("There was an error with the Google API. The two main causes are if you've run out of queries or if the search engine ID and api key " +
                    "entered in the developer panel were incorrect.");
                return JsonConvert.DeserializeObject("{}");
            }
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject(responseString);
        }
    }
}

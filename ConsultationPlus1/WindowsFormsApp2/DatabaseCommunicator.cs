using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp2
{
    /** This class is responsible for all communication with the django database. It handles saving queries/urls,
     *  checking registration, adding new users and saving and fetching recommendation data. It uses the graphs API
     *  with the following permissions: ReadWrite.AppFolder and user.mail. Once the database is deployed modify the
     *  database_url variable.
     */
    public static class DatabaseCommunicator
    {

        private static string database_url = "http://127.0.0.1:8000/";
        public static string email;

        /** Auxiliary function to send a HTTP request to database. Returns a Json object that has been parsed.
         *  @param request is a HttpWebRequest with a url that has already been built.
         *         method is the type of http request e.g. GET, POST, PUT etc.
         *         data is the data we want to send in bytes form.
         *  @return a json object that the database sent back. Use .GetValue(key) to obtain data in JObject.
         */
        private static JObject SendRequest(HttpWebRequest request, string method, byte[] data)
        {
            request.Method = method;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (System.Net.WebException)
            {
                return (JObject) JsonConvert.DeserializeObject("{\"message\" : \"Unsuccessful\", \"test\" : \"fail\"}");
            }
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JObject ret = (JObject)JsonConvert.DeserializeObject(responseString);
            return ret;
        }

        /** Used to just check if our application can successfully connect to database at the start. If it can't
         *  we run our application anyway but it will not save urls, queries and recommendations and won't load
         *  recommendations.
         */
        public static void CheckIfConnected()
        {
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/check/email");
            var postData = "email=" + Uri.EscapeDataString("randomEmail.com");
            var data = Encoding.ASCII.GetBytes(postData);
            JObject ret = SendRequest(request, "POST", data);
            if (ret.ContainsKey("test"))
            {
                MessageBox.Show("Warning: This application is not connected to the database. Therefore, we cannot load recommendations or save any data to database.");
                Program.connected_to_database = false;
            }
            else
            {
                Program.connected_to_database = true;
            }
        }

        /** Used to save the query that the user types to django database. The query is saved when search button is
         *  clicked. We store the email, query and time.
         *  @param query is the string typed into the search box.
         *  @returns Json object for testing to see if it worked.
         */
        public static JObject SaveQuery(string query)
        {
            // send http POST request to django database. request contains query and email.
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/save/query");
            var postData = "email=" + Uri.EscapeDataString(email);
            postData += "&query=" + Uri.EscapeDataString(query);
            var data = Encoding.ASCII.GetBytes(postData);
            JObject ret = SendRequest(request, "POST", data);
            if (ret.GetValue("message").ToString() != "Success")
            {
                MessageBox.Show("Unsuccessful in saving query.");
            }
            return ret;
        }

        /** Each time a user clicks on a link we save the corresponding whitelist url to the databse.
         *  GetWhitelistURL returns the whitelist url for the linked clicked. We send a http post request
         *  to database and save the email, whitelist url and the time the link was clicked.
         *  @param url is the url of the website that the user clicked on.
         */
        public static JObject SaveURL(string url)
        {
            // similarly to saveQuery we send a http POST request to django database containing email and whitelist url.
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/save/url");
            var postData = "email=" + Uri.EscapeDataString(email);
            postData += "&url=" + Uri.EscapeDataString(Program.GetWhitelistURL(url));
            var data = Encoding.ASCII.GetBytes(postData);
            JObject ret = SendRequest(request, "POST", data);
            if (ret.GetValue("message").ToString() != "Success")
            {
                MessageBox.Show("Unsuccessful in saving URL.");
            }
            return ret;
        }

        /** Adds a user to the django database. Stores users email and location. If they don't want to disclose we send
        *  N/A as location.
        *  @param userEmail is a string that contains users email.
        *         location is the location of the user. they must select it from a predefined list.
        */
        public static JObject AddUser(string userEmail, string location)
        {
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/add/user");
            var postData = "email=" + Uri.EscapeDataString(userEmail);
            postData += "&location=" + Uri.EscapeDataString(location);
            var data = Encoding.ASCII.GetBytes(postData);

            JObject ret = SendRequest(request, "POST", data);
            if (ret.GetValue("message").ToString() != "Success")
            {
                // failed to add user.
                MessageBox.Show("Unsuccessful in adding new user.");
            }
            return ret;
        }

        /** Queries django database to see if the email parameter exists. if it does then the user 
         *  is already in database. If it doesnt exist then we must ask the user to register by inputting
         *  their location.
         *  @param email is the user email that microsoft graphs returns
         *  @return true if user exists in django database else false.
         */
        public static bool CheckRegistration(string email)
        {
            // Send http GET request to database to check if email already exists.
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/check/email");
            var postData = "email=" + Uri.EscapeDataString(email);
            var data = Encoding.ASCII.GetBytes(postData);
            JObject ret = SendRequest(request, "POST", data);
            if (ret.GetValue("message").ToString() == "Exists")
            {
                return true;
            }
            return false;
        }
        /** Queries django database to see if the link parameter exists. If it does then the result is already
        * in the database so we set its recommendationsCount to whatever is its field value. And if it's not in
        * it returns 0;
        */
        public static int SetRecommendations(string link)
        {
            if (link == null)
                return 0;
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/get/recommendations");
            var postData = "link=" + Uri.EscapeDataString(link);
            var data = Encoding.ASCII.GetBytes(postData);
            JObject ret = SendRequest(request, "POST", data);
            if (ret.GetValue("message").ToString() != "Unsuccessful")
            {
                return Int32.Parse(ret.GetValue("content").ToString());
            }
            return 0;
        }
        /** Each time the upvote or downvote button are clicked the link is getting saved in the database with param
         * @link and @recommendationsNumber
         */
        public static bool SaveRecommendations(string link, int recommendationsCount)
        {
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/save/recommendations");
            var postData = "link=" + Uri.EscapeDataString(link);
            postData += "&recommendationCount=" + Uri.EscapeDataString(recommendationsCount.ToString());
            var data = Encoding.ASCII.GetBytes(postData);
            JObject ret = SendRequest(request, "POST", data);
            if (ret.GetValue("message").ToString() != "Success")
            {
                // failed to save recommendations.
                MessageBox.Show("Unsuccessful in adding new user.");
                return false;
            }
            return true;
        }
    }
}

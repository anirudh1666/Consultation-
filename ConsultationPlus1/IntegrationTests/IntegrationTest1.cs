using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using WindowsFormsApp2;

namespace IntegrationTests
{
    [TestClass]
    public class TestDatabaseCommunicator
    {
        public static string database_url = "http://127.0.0.1:8000/";
        public static void DeleteUser(string email)
        {
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/delete/user");
            var postData = "email=" + Uri.EscapeDataString(email);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
        } 

        public void DeleteQuery(string email, string query)
        {
            var request = (HttpWebRequest)WebRequest.Create(database_url +"post/app/delete/query");
            var postData = "email=" + Uri.EscapeDataString(email);
            postData += "&query=" + Uri.EscapeDataString(query);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
        }

        public void DeleteUrl(string email, string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/delete/url");
            var postData = "email=" + Uri.EscapeDataString(email);
            postData += "&url=" + Uri.EscapeDataString(url);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
        }

        public void DeleteRecommendation(string link)
        {
            var request = (HttpWebRequest)WebRequest.Create(database_url + "post/app/delete/recommendation");
            var postData = "link=" + Uri.EscapeDataString(link);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
        }


        // Make sure to have database running for these tests.
        [TestMethod]
        public void TestCheckRegistrationReturnsFalse()
        {
            Assert.IsFalse(DatabaseCommunicator.CheckRegistration("notAUser@randomEmail.com"));
        }

        [TestMethod]
        public void TestCheckRegistrationReturnsTrue()
        {
            string user = "test_user2@randomEmail.com";
            DatabaseCommunicator.AddUser(user, "3");

            Assert.IsTrue(DatabaseCommunicator.CheckRegistration(user));

            DeleteUser(user);
        }

        [TestMethod]
        public void TestAddUserSuccessfully()
        {
            Newtonsoft.Json.Linq.JObject response = DatabaseCommunicator.AddUser("test_user3@randomEmail.com", "1");
            
            Assert.AreEqual(response.GetValue("message").ToString(), "Success");

            DeleteUser("test_user3@randomEmail.com");
        }

        [TestMethod]
        public void TestAddUserUnsuccessfully()
        {
            string user = "test_user4@randomEmail.com";
            DatabaseCommunicator.AddUser(user, "11");
            Newtonsoft.Json.Linq.JObject response = DatabaseCommunicator.AddUser(user, "9");

            Assert.AreEqual(response.GetValue("message").ToString(), "User already created.");

            DeleteUser(user);
        }

        [TestMethod]
        public void TestSaveQuerySuccessfully()
        {
            string user = "test_user1@randomEmail.com";
            string query = "chronic kidney disease";
            DatabaseCommunicator.email = user;
            DatabaseCommunicator.AddUser(user, "9");
            Newtonsoft.Json.Linq.JObject response = DatabaseCommunicator.SaveQuery(query);

            Assert.AreEqual(response.GetValue("message").ToString(), "Success");

            DeleteQuery(user, query);
            DeleteUser(user);
        }

        [TestMethod]
        public void TestSaveQueryUnsuccessfully()
        {
            string user_not_in_db = "test_user0@randomEmail.com";
            string query = "liver failure";
            DatabaseCommunicator.email = user_not_in_db;
            Newtonsoft.Json.Linq.JObject response = DatabaseCommunicator.SaveQuery(query);

            Assert.AreEqual(response.GetValue("message").ToString(), "User not found");
        }

        [TestMethod]
        public void TestSaveUrlSuccessfully()
        {
            string user = "test_user5@randomEmail.com";
            string url = "gpnotebook.com";
            DatabaseCommunicator.email = user;
            DatabaseCommunicator.AddUser(user, "7");
            Newtonsoft.Json.Linq.JObject response = DatabaseCommunicator.SaveURL(url);

            Assert.AreEqual(response.GetValue("message").ToString(), "Success");

            DeleteUrl(user, url);
            DeleteUser(user);
        }

        [TestMethod] 
        public void TestSaveUrlUnsuccessfully()
        {
            string user = "test_user6@randomEmail.com";
            string url = "gpnotebook.com";
            DatabaseCommunicator.email = user;
            Newtonsoft.Json.Linq.JObject response = DatabaseCommunicator.SaveURL(url);

            Assert.AreEqual(response.GetValue("message").ToString(), "User not found");
        }

        [TestMethod]
        public void TestSaveRecommendationSuccessfullyWithNewUrl()
        {
            string link = "www.fakeurl4.com";
            int count = 4;
            Assert.IsTrue(DatabaseCommunicator.SaveRecommendations(link, count));
            DeleteRecommendation(link);
        }

        [TestMethod]
        public void TestSaveRecommendationSuccessfullyWithAlreadyExistingUrl()
        {
            string link = "www.fakeUrl5.com";
            int count = 3;
            DatabaseCommunicator.SaveRecommendations(link, count);

            Assert.IsTrue(DatabaseCommunicator.SaveRecommendations(link, 4));
            Assert.AreEqual(DatabaseCommunicator.SetRecommendations(link), 4);

            DeleteRecommendation(link);
        }

        [TestMethod]
        public void TestGetRecommendationSuccessfully()
        {
            string link = "www.fakeurl1.com";
            DatabaseCommunicator.SaveRecommendations(link, 10);

            Assert.AreEqual(DatabaseCommunicator.SetRecommendations(link), 10);

            DeleteRecommendation(link);
        }

        [TestMethod]
        public void TestGetRecommendationWithUrlNotInDB()
        {
            Assert.AreEqual(DatabaseCommunicator.SetRecommendations("www.fakeurl2.com"), 0);
        }

        [TestMethod]
        public void TestCheckConnection()
        {
            DatabaseCommunicator.CheckIfConnected();
            Assert.IsTrue(Program.connected_to_database);
        }

        [TestMethod]
        public void TestCheckConnectionFalse()
        {
            // RUN THIS WHILE DATABASE IS NOT CONNECTED SINCE WE WANT IT TO RETURN FALSE.
            DatabaseCommunicator.CheckIfConnected();
            Assert.IsFalse(Program.connected_to_database);
        }

    }

    [TestClass]
    public class TestGoogle
    {

        [TestMethod] 
        public void TestGoogleSearchEmptyQuery()
        {
            List<Category> cat = new List<Category>()
            {
                // first one searches www.nhs.uk and rcgp.org.uk
                new Category("Test", new List<string>()
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
                    "aaa.org",
                    "apexcardiology.com",
                    "bhvci.com",
                    "pacificheart.com",
                    "gpnotebook.com",
                    "bnf.nice.org.uk",
                    "bnfc.nice.org.uk",
                    "products.mhra.gov.uk",
                    "www.hee.nhs.uk",
                    "www.sign.ac.uk"
                }, true)
            };
            Program.categories = cat;
            Program.LoadWhitelist();
            List<Result> ret = GoogleAPIHandler.Search("");

            Assert.IsTrue(ret.Count() == 1);
            Assert.IsTrue(ret[0].Title == "Not enough links?");
            Assert.IsTrue(ret[0].Snippet == "");
        }

        // this method only works if the sites in the custom search control panel are the same as the ones in
        // whitelist.txt
        [TestMethod]
        public void TestGoogleSearch()
        {
            // only 2 categories and both are ticked.
            List<Category> cat = new List<Category>()
            {
                // first one searches www.nhs.uk and rcgp.org.uk
                new Category("Test", new List<string>()
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
                    "aaa.org",
                    "apexcardiology.com",
                    "bhvci.com",
                    "pacificheart.com",
                    "gpnotebook.com",
                    "bnf.nice.org.uk",
                    "bnfc.nice.org.uk",
                    "products.mhra.gov.uk",
                    "www.hee.nhs.uk",
                    "www.sign.ac.uk"

                }, true),
                // searches bnf.nice.org.uk
                new Category("Test2", new List<string>()
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
                    "aaa.org",
                    "apexcardiology.com",
                    "bhvci.com",
                    "pacificheart.com",
                    "gpnotebook.com",
                    "bnfc.nice.org.uk",
                    "products.mhra.gov.uk",
                    "www.hee.nhs.uk",
                    "www.sign.ac.uk",
                    "www.nhs.uk",
                    "rcgp.org.uk"
                }, true)
            };
            string query = "kidney disease";
            Program.categories = cat;
            Program.LoadWhitelist();
            List<Result> ret = GoogleAPIHandler.Search(query);
            // By using Googles "Try it out" CSE API online i was able to obtain a list of results that google returns for the query and categories. There are 20 results but 
            // we can be reasonable sure that it is working by checking if the top 6 results match to the ones that GoogleAPIHandler.Search returns.
            List<Result> expected = new List<Result>()
            {
                new Result
                {
                    Title = "Chronic kidney disease - Treatment - NHS",
                    Link = "https://www.nhs.uk/conditions/kidney-disease/treatment/",
                    Snippet = "Find out about the main treatments for chronic kidney disease (CKD), including \nlifestyle changes, medication, dialysis and kidney transplants.",
                    RecommendationsNumber = 0
                },
                new Result
                {
                    Title = "Chronic kidney disease - Symptoms - NHS",
                    Link = "https://www.nhs.uk/conditions/kidney-disease/symptoms/",
                    Snippet = "Find out about the main symptoms of chronic kidney disease (CKD) and when to \nget medical advice." ,
                    RecommendationsNumber = 0
                },
                new Result
                {
                    Title = "Chronic kidney disease - NHS",
                    Link = "https://www.nhs.uk/conditions/kidney-disease/",
                    Snippet = "Find out what chronic kidney disease (CKD) is, including what the symptoms are, \nhow it's diagnosed and how it can be treated.",
                    RecommendationsNumber = 0
                },
                new Result
                {
                    Title = "Autosomal recessive polycystic kidney disease - NHS",
                    Link = "https://www.nhs.uk/conditions/autosomal-recessive-polycystic-kidney-disease-arpkd/",
                    Snippet = "Autosomal recessive polycystic kidney disease (ARPKD) is a rare inherited \nchildhood condition where the development of the kidneys and liver is abnormal.",
                    RecommendationsNumber = 0
                },
                new Result
                {
                    Title = "Chronic kidney disease - Diagnosis - NHS",
                    Link = "https://www.nhs.uk/conditions/kidney-disease/diagnosis/",
                    Snippet = "Find out how chronic kidney disease (CKD) is diagnosed, who should get tested \nand what the stages of CKD mean.",
                    RecommendationsNumber = 0
                },
                new Result
                {
                    Title = "Autosomal dominant polycystic kidney disease - Symptoms - NHS",
                    Link = "https://www.nhs.uk/conditions/autosomal-dominant-polycystic-kidney-disease-adpkd/symptoms/",
                    Snippet = "Read about the symptoms of autosomal dominant polycystic kidney disease (\nADPKD), including pain in your abdomen, side or lower back, blood in your urine\n ...",
                    RecommendationsNumber = 0
                }
            };

            foreach (int i in Enumerable.Range(0, 6))
            {
                Assert.AreEqual(ret[i].Title, expected[i].Title);
                Assert.AreEqual(ret[i].Link, expected[i].Link);
                Assert.AreEqual(ret[i].Snippet, expected[i].Snippet);
            }
        }
    }
}

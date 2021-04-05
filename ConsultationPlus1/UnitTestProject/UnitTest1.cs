using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WindowsFormsApp2;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestProgram
    {
        [TestMethod]
        public void TestGetWhitelistURL_WithCorrectURL()
        {
            var domain = Program.GetWhitelistURL("https://gpnotebook.com/homepage.cfm");
            Assert.AreEqual(domain, "gpnotebook.com");
        }

        [TestMethod]
        public void TestCreateCategory()
        {
            Program.categories = new List<Category>();
            Program.LoadWhitelist();
            Program.CreateCategory("test_category", new List<string>()
            {
                "bnf.nice.org.uk"
            });
            Assert.IsTrue(Program.categories.Count == 1);
            Assert.IsTrue(Program.categories[0].Name == "test_category");
        }

        [TestMethod]
        public void TestLoadWhitelist()
        {
            Program.LoadWhitelist();
            string[] expected =
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
                "www.sign.ac.uk",
                "www.nhs.uk",
                "rcgp.org.uk"
            };
            foreach (int i in Enumerable.Range(0, expected.Count() - 1))
            {
                Assert.AreEqual(Program.whitelist[i], expected[i]);
            }
        }
    }
}

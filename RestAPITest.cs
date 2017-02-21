using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace APITestingDemo
{
    [TestClass]
    public class RestAPITest
    {

        private RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://xkcd.com");
        }

        [TestMethod]
        public void makeASucessfulRequest()
        {
            var request = new RestRequest("/info.0.json", Method.GET);

            IRestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);
            Assert.AreEqual("OK", response.StatusCode.ToString());
        }

        [TestMethod]
        public void makeARequestForInexistentContent()
        {
            var request = new RestRequest("/xpto/info.0.json", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual("NotFound", response.StatusCode.ToString());
        }

        [TestMethod]
        public void findASpecificComic()
        {
            var request = new RestRequest("/1119/info.0.json", Method.GET);

            IRestResponse response = client.Execute(request);
            string json = response.Content;
            JObject title = JObject.Parse(json);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual("Undoing", (string) title["title"]);
        }

        [TestMethod]
        public void findAnInexistentComic()
        {
            var request = new RestRequest("/99999/info.0.json", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual("NotFound", response.StatusCode.ToString());
        }
    }
}

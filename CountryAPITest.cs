using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace APITestingDemo
{
    [TestClass]
    public class CountryAPITest
    {

        private RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("https://restcountries.eu/rest/v2");
        }

        [TestMethod]
        public void findCountryByName()
        {
            var request = new RestRequest("/name/portugal");

            IRestResponse response = client.Execute(request);
            string json = response.Content;

            JArray countries = JArray.Parse(json);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual("Portugal", (string) countries[0]["name"]);
        }

        [TestMethod]
        public void findCountryByCode()
        {
            var request = new RestRequest("/alpha/de");

            IRestResponse response = client.Execute(request);
            string json = response.Content;

            JObject country = JObject.Parse(json);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual("Germany", (string) country["name"]);
        }

        [TestMethod]
        public void findCountriesByCurrency()
        {
            var request = new RestRequest("/currency/eur");

            IRestResponse response = client.Execute(request);
            string json = response.Content;

            JArray countries = JArray.Parse(json);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual(36, countries.Count);    
        }

        [TestMethod]
        public void findCountryByCapital()
        {
            var request = new RestRequest("/capital/copenhagen");

            IRestResponse response = client.Execute(request);
            string json = response.Content;

            JArray countries = JArray.Parse(json);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual("Denmark", (string) countries[0]["name"]);
        }

        [TestMethod]
        public void findCountriesByRegion()
        {
            var request = new RestRequest("/region/europe");

            IRestResponse response = client.Execute(request);
            string json = response.Content;

            JArray countries = JArray.Parse(json);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual(53, countries.Count);
        }

        [TestMethod]
        public void findAnInexistentCountry()
        {
            var request = new RestRequest("/name/xpto");

            IRestResponse response = client.Execute(request);

            Assert.AreEqual("NotFound", response.StatusCode.ToString());
        }
    }
}

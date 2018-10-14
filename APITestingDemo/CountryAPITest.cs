using RestSharp;
using Newtonsoft.Json.Linq;
using Xunit;

namespace APITestingDemo
{
    public class CountryAPITest
    {
        private RestClient client;

        public CountryAPITest()
        {
            client = new RestClient("https://restcountries.eu/rest/v2");
        }

        [Fact]
        public void CountryApi_FindByName_ReturnsOK()
        {
            var request = new RestRequest("/name/portugal");

            IRestResponse response = client.Execute(request, Method.GET);
            string json = response.Content;

            JArray countries = JArray.Parse(json);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("Portugal", (string) countries[0]["name"]);
        }

        [Fact]
        public void CoutryAPI_FindByCountryCode_ReturnsOK()
        {
            var request = new RestRequest("/alpha/de");

            IRestResponse response = client.Execute(request, Method.GET);
            string json = response.Content;

            JObject country = JObject.Parse(json);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("Germany", (string) country["name"]);
        }

        [Fact]
        public void CountryAPI_FindByCurrency_ReturnsOK()
        {
            var request = new RestRequest("/currency/eur");

            IRestResponse response = client.Execute(request, Method.GET);
            string json = response.Content;

            JArray countries = JArray.Parse(json);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal(36, countries.Count);    
        }

        [Fact]
        public void CountryAPI_FindByCapital_ReturnsOK()
        {
            var request = new RestRequest("/capital/copenhagen");

            IRestResponse response = client.Execute(request, Method.GET);
            string json = response.Content;

            JArray countries = JArray.Parse(json);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("Denmark", (string) countries[0]["name"]);
        }

        [Fact]
        public void CountryAPI_FindByRegion_ReturnsOK()
        {
            var request = new RestRequest("/region/europe");

            IRestResponse response = client.Execute(request, Method.GET);
            string json = response.Content;

            JArray countries = JArray.Parse(json);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal(53, countries.Count);
        }

        [Fact]
        public void CountryAPI_FindANonExistingCountry_ReturnsOK()
        {
            var request = new RestRequest("/name/xpto");

            IRestResponse response = client.Execute(request, Method.GET);

            Assert.Equal("NotFound", response.StatusCode.ToString());
        }
    }
}

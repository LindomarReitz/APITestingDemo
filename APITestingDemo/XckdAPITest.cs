using RestSharp;
using Newtonsoft.Json.Linq;
using Xunit;

namespace APITestingDemo
{
    public class XckdAPITest
    {
        private RestClient client;
        
        public XckdAPITest()
        {
            client = new RestClient("http://xkcd.com");
        }

        [Fact]
        public void XckdAPI_MakeASuccessfulRequest_ReturnsOK()
        {
            var request = new RestRequest("/info.0.json", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public void XckdAPI_MakeARequestForInexistentContent_ReturnsNotFound()
        {
            var request = new RestRequest("/xpto/info.0.json", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.Equal("NotFound", response.StatusCode.ToString());
        }

        [Fact]
        public void XckdAPI_FindASpecificComic_ReturnsOK()
        {
            var request = new RestRequest("/1119/info.0.json", Method.GET);

            IRestResponse response = client.Execute(request);
            string json = response.Content;
            JObject title = JObject.Parse(json);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("Undoing", (string) title["title"]);
        }

        [Fact]
        public void XckdAPI_FindAnInexistentComic_ReturnsNotFound()
        {
            var request = new RestRequest("/99999/info.0.json", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.Equal("NotFound", response.StatusCode.ToString());
        }
    }
}

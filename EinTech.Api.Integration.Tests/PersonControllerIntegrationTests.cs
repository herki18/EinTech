using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EinTech.Api.Integration.Tests.Helpers;
using Microsoft.Net.Http.Headers;
using Xunit;

namespace EinTech.Api.Integration.Tests
{
    public class PersonControllerIntegrationTests : IClassFixture<TestingWebAppFactory>
    {
        private readonly HttpClient _client;

        public PersonControllerIntegrationTests(TestingWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_When_Called_Return_Five_Persons()
        {
            var response = await _client.GetAsync("/Persons");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Person A", responseString);
            Assert.Contains("Person B", responseString);
            Assert.Contains("Person C", responseString);
            Assert.Contains("Person D", responseString);
            Assert.Contains("Person E", responseString);
        }
        
        [Fact]
        public async Task Get_When_Called_With_Filter_Return_One_Person()
        {
            var response = await _client.GetAsync("/Persons?searchString=Person A");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Person A", responseString);
            Assert.DoesNotContain("Person B", responseString);
            Assert.DoesNotContain("Person C", responseString);
            Assert.DoesNotContain("Person D", responseString);
            Assert.DoesNotContain("Person E", responseString);
        }
        
        [Fact]
        public async Task Create_When_Called_Returns_Create_Form()
        {
            var response = await _client.GetAsync("/Persons/Create");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Create Person", responseString);
        }

        [Fact]
        public async Task Create_Sent_Wrong_Model_Returns_View_With_Error_Message()
        {
            var initResponse = await _client.GetAsync("/Persons/Create");
            var (fieldValue, cookieValue) = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Persons/Create");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, cookieValue).ToString());

            var formModel = new Dictionary<string, string>()
            {
                { AntiForgeryTokenExtractor.AntiForgeryFieldName, fieldValue },
                { "GroupId", "1" }
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("The Name field is required.", responseString);
        }

        [Fact]
        public async Task Create_When_POST_Executed_Returns_To_Index_View_With_Create_Person()
        {
            var initResponse = await _client.GetAsync("/Persons/Create");
            var (fieldValue, cookieValue) = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Persons/Create");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, cookieValue).ToString());

            var formModel = new Dictionary<string, string>
            {
                { AntiForgeryTokenExtractor.AntiForgeryFieldName, fieldValue },
                { "Name", "New Person" },
                { "GroupId", "1" }
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("New Person", responseString);
            Assert.Contains("GroupB", responseString);
        }
    }
}

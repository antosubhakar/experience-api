using Doctrina.ExperienceApi.Client;
using Doctrina.ExperienceApi.Client.Http.Headers;
using Doctrina.ExperienceApi.Data;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Doctrina.ExperienceApi.Client.Tests.Statements
{
    public class GetStatement
    {
        [Fact]
        public async Task Get_Statement_By_Id()
        {
            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("http://example.com/xAPI/statements")
                    // Respond with JSON
                    .Respond("application/json", @"{'id':'c70c2b85-c294-464f-baca-cebd4fb9b348','timestamp':'2014-12-29T12:09:37.468Z','actor':{'objectType':'Agent','mbox':'mailto:example@example.com','name':'Test User'},'verb':{'id':'http://adlnet.gov/expapi/verbs/experienced','display':{'en-US':'experienced'}},'object':{'id':'http://example.com/xAPI/activities/myactivity','objectType':'Activity'}}"); 

            // Inject the handler or client into your application code
            var httpClient = new HttpClient(mockHttp);

            var authHeader = new BasicAuthHeaderValue("admin", "password");
            var client = new LRSClient(authHeader, ApiVersion.GetLatest(), httpClient);

            var result = await client.GetStatement(Guid.Parse("c70c2b85-c294-464f-baca-cebd4fb9b348"));

            // No network connection required
            Console.Write(result.ToJson()); // {'name' : 'Test McGee'}
        }
    }
}

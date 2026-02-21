using RestSharp;
using System.Collections.Generic;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SnowE2E.Test.Helper
{
    public static class APIHelper
    {
        private static readonly RestClient _client;
        static string snowPass = Environment.GetEnvironmentVariable("SNOW_PASSWORD");
        static string snowUsername = Environment.GetEnvironmentVariable("SNOW_USERNAME");

        static string username = AppSettings.TestRunType == TestRunType.local ? AppSettings.MainUsername : snowUsername;
        static string password = AppSettings.TestRunType == TestRunType.local ? AppSettings.MainPassword : snowPass;
        static string baseUrl = AppSettings.BackOfficePage + "/api/now/table/";

        static APIHelper()
        {
            _client = new RestClient(new RestClientOptions(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            });
        }

        public static string GetRitmSysID(string ritmnumber)
        {
            string url = "sc_req_item";

            var request = new RestRequest(url);
            request.AddParameter("number", ritmnumber);
            var response = _client.Get(request);

            Root convertedresponse = JsonConvert.DeserializeObject<Root>(response.Content);

            return convertedresponse.result[0].sys_id;
        }

        public static string CreateRITM(string catItem, string openedBy)
        {
            string url = "sc_req_item";

            var request = new RestRequest(url);
            request.AddJsonBody(new
            {
                cat_item = catItem,
                opened_by = openedBy,
            });
            var response = _client.Post(request);

            Assert.That(response.IsSuccessful, $"Failed to create RITM. Status code: {response.StatusCode}, Response: {response.Content}");

            JObject jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine($"Create RITM Response: {jsonResponse}");
            string ritmNumber = jsonResponse["result"]?["number"]?.ToString() ?? string.Empty;
            return ritmNumber;

        }

        public static void CloseRITM(string ritmnumber)
        {
            string ritmSysId = GetRitmSysID(ritmnumber);

              string url = $"sc_req_item/" + ritmSysId;
           
            var jsonbody = new { state = "3" };
            var serializedJson = JsonConvert.SerializeObject(jsonbody);
            var request = new RestRequest(url);
            request.AddBody(serializedJson);
            var response = _client.Patch(request);
        }

        class Root
        {
            public List<Result> result { get; set; }
        }

        class Result
        {
            public string sys_id { get; set; }
        }
    }
}
using System.Net.Http;
using System.Net.Http.Headers;
using TennisClub.Api;

namespace TennisClub
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; private set; }
        public static readonly string BASEURL = "https://localhost:5001/api/";
        public static IApiCaller apiCaller { get; private set; } = new ApiCaller();

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static void InitializeClient(IApiCaller caller)
        {
            InitializeClient();
            apiCaller = caller;
        }
    }
}

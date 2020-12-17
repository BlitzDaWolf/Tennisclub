using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TennisClub.Api
{
    public class ApiCaller : IApiCaller
    {
        public async Task<T> GetObject<T>(string url, params string[] args) where T : class
        {
            string arguments = "";
            if(args.Length > 0)
            {
                arguments += "?" + string.Join("&", args);
            }
            string fullUri = ApiHelper.BASEURL + url;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(fullUri + arguments))
            {
                if (response.IsSuccessStatusCode)
                {
                    T r = await response.Content.ReadAsAsync<T>();
                    return r;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<T> GetById<T>(object id, params string[] args) where T : class
        {
            return await GetObject<T>($"{typeof(T).Name}/{id}", args);
        }

        public async Task<List<T>> GetAll<T>(params string[] args) where T : class
        {
            return await GetObject<List<T>>($"{typeof(T).Name}", args);
        }

        public async Task Put<T>(string url, T send, params string[] args) where T : class
        {
            string fullUri = ApiHelper.BASEURL + url;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(fullUri, send))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task Post<T>(string url, T send, params string[] args) where T : class
        {
            string fullUri = ApiHelper.BASEURL + url;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(fullUri, send))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

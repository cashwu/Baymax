using System.Collections.Generic;
using System.Net.Http;

namespace Baymax.Tester.Integration
{
    public static class HttpHelper
    {
        public static TResult GetHttpResult<TResult>(this HttpClient httpClient, string url)
        {
            var httpResult = httpClient.GetAsync(url).Result;
            return httpResult.Content.ReadAsAsync<TResult>().Result;
        }

        public static TResult PostHttpResult<TResult, TPostData>(this HttpClient httpClient, string url, TPostData postData)
        {
            var httpResult = httpClient.PostAsJsonAsync(url, postData).Result;
            return httpResult.Content.ReadAsAsync<TResult>().Result;
        }

        public static TResult PostHttpResult<TResult>(this HttpClient httpClient, string url)
        {
            var httpResult = httpClient.PostAsync(url, new FormUrlEncodedContent(new Dictionary<string, string>())).Result;
            return httpResult.Content.ReadAsAsync<TResult>().Result;
        }

        public static TResult PutHttpResult<TResult, TPostData>(this HttpClient httpClient, string url, TPostData postData)
        {
            var httpResult = httpClient.PutAsJsonAsync(url, postData).Result;
            return httpResult.Content.ReadAsAsync<TResult>().Result;
        }
    }
}
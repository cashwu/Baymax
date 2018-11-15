using System.Collections.Generic;
using System.Net.Http;

namespace Baymax.Tester.Integration
{
    public static class HttpClientExtension
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

        public static TResult PutHttpResult<TResult, TPutData>(this HttpClient httpClient, string url, TPutData putData)
        {
            var httpResult = httpClient.PutAsJsonAsync(url, putData).Result;
            return httpResult.Content.ReadAsAsync<TResult>().Result;
        }

        public static HttpResponseMessage DeleteHttpResult(this HttpClient httpClient, string url)
        {
            return httpClient.DeleteAsync(url).Result;
        }
        
        public static TResult DeleteHttpResult<TResult>(this HttpClient httpClient, string url)
        {
            var httpResult = httpClient.DeleteAsync(url).Result;
            return httpResult.Content.ReadAsAsync<TResult>().Result;
        }
    }
}
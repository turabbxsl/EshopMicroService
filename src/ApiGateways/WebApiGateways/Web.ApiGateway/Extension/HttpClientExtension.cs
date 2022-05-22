using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Web.ApiGateway.Extension
{
    public static class HttpClientExtension
    {



        // Post etdikden sonra colden data getire bilme ucun
        public async static Task<TResult> PostGetResponceAsync<TResult, TValue>(this HttpClient httpClient, String url, TValue value)
        {
            var httpRes = await httpClient.PostAsJsonAsync(url, value);

            // Eger http-den Success Kodu donmuse eger,Contenti oxuyacayig ve Colden gelen Generic tipe (TResult) cevireceyik
            return httpRes.IsSuccessStatusCode ? await httpRes.Content.ReadFromJsonAsync<TResult>() : default;
        }




        public async static Task PostAsync<TValue>(this HttpClient httpClient, String url, TValue value)
        {
            await httpClient.PostAsJsonAsync(url, value);
        }




        public async static Task<T> GetResponseAsync<T>(this HttpClient httpClient, String url)
        {
            return await httpClient.GetFromJsonAsync<T>(url);
        }




    }
}

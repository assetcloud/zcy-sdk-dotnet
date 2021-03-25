using AssetCloud.AssetCloudSDK.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AssetCloud.AssetCloudSDK
{
    public class AssetCloudClient
    {
        HttpClient client = new HttpClient();
        static JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        AssetCloudConfig config;



        public AssetCloudClient(AssetCloudConfig config)
        {
            this.config = config;
            // 基址必须以/结尾
            var postfix = config.BaseUrl.EndsWith("/") ? "" : "/";
            client.BaseAddress = new Uri(config.BaseUrl + postfix);
        }

        public async Task<AssetCloudResponse<T>> SendAsync<T>(
            string url, 
            HttpMethod method,
            IDictionary<string, object> query = null,
            object body = null)
        {
            var req = new HttpRequestMessage();
            req.Method = method;

            var fullurl = GetSignedFullUrl(url, query);
            // 存在基址的情况下，相对URL不能以/开头
            fullurl = fullurl.StartsWith("/") ? fullurl.Substring(1) : fullurl;
            req.RequestUri = new Uri(fullurl, uriKind: UriKind.Relative);

            req.Headers.Add("key", config.AppKey);
            req.Headers.UserAgent.ParseAdd("AssetCloudClient .NET");

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            req.Content = content;

            Console.WriteLine($"→ {req.Method} {req.RequestUri}");

            var res = await client.SendAsync(req);

            Console.WriteLine($"← {res.StatusCode} {res.RequestMessage.RequestUri}");

            if (!res.IsSuccessStatusCode && config.ThrowsOnFailureResponseCode)
            {
                throw new HttpRequestException($"{res.StatusCode} {res.ReasonPhrase}");
            }

            var data = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AssetCloudResponse<T>>(data, serializerOptions);

        }

        public Task<AssetCloudResponse<T>> GetAsync<T>(string url, IDictionary<string, object> query = null) 
        {
            return SendAsync<T>(url, HttpMethod.Get, query);
        }

        public Task<AssetCloudResponse<T>> PostAsync<T>(string url, IDictionary<string, object> query = null, object body = null)
        {
            return SendAsync<T>(url, HttpMethod.Post, query);
        }

        string GetSignedFullUrl(string url, IDictionary<string, object> query = null)
        {
            if (query == null) query = new Dictionary<string, object> { }; 
            query["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            var prefix = url.Contains("?") ? "&" : "?";
            var querystring = prefix + string.Join("&",
                   query.Select(p => p.Key + "=" + Uri.EscapeDataString(p.Value?.ToString() ?? ""))
            );
            var fullurl = url + querystring;

            var message = fullurl.Substring(fullurl.IndexOf("?") + 1);
            var digest = HashUtil.HMACSHA256(config.AppSecret, message);

            return fullurl + "&sign=" + digest;

        }


    }
}

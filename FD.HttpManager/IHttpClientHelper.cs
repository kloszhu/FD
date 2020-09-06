using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FD.HttpManager
{
    public interface IHttpClientHelper
    {
        HttpResponseMessage Get(string url, List<KeyValuePair<string, string>> headers = null);
        Task<HttpResponseMessage> GetAsync(string url, List<KeyValuePair<string, string>> headers = null);
        HttpResponseMessage Post(string url, List<KeyValuePair<string, string>> paramList, List<KeyValuePair<string, string>> headers = null);
        HttpResponseMessage Post(string url, string content, List<KeyValuePair<string, string>> headers = null);
        Task<HttpResponseMessage> PostAsync(string url, List<KeyValuePair<string, string>> paramList, List<KeyValuePair<string, string>> headers = null);
        Task<HttpResponseMessage> PostAsync(string url, string content, List<KeyValuePair<string, string>> headers = null);
        void Release();
        void RemoveDefaultHeaders(string name);
        void SetDefaultHeaders(string name, string value);
    }
}
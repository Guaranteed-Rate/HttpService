namespace GuaranteedRate.Net.Http.HttpService
{
    using System;
    using System.Collections.Specialized;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHttpClient : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(string url, NameValueCollection headers = null);
        Task<HttpResponseMessage> PostAsync<T>(string url, T body, NameValueCollection headers = null);
        Task<HttpResponseMessage> PutAsync<T>(string url, T body, NameValueCollection headers = null);
        Task<HttpResponseMessage> DeleteAsync(string url, NameValueCollection headers = null);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}

namespace GuaranteedRate.Net.Http.HttpService
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading.Tasks;

    public class HttpClient : IHttpClient
    {
        private readonly string _baseUrl;
        private readonly System.Net.Http.HttpClient _client;
        protected readonly NameValueCollection DefaultRequestHeaders;

        public HttpClient(string baseUrl = null, NameValueCollection defaultRequestHeaders = null)
        {
            DefaultRequestHeaders = defaultRequestHeaders ?? new NameValueCollection();
            if (DefaultRequestHeaders.AllKeys.All(key => !key.Equals("Content-Type", StringComparison.InvariantCultureIgnoreCase)))
                DefaultRequestHeaders.Add("Content-Type", "application/json");
            _client = new System.Net.Http.HttpClient();
            _baseUrl = baseUrl ?? string.Empty;
        }

        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }

        public Task<HttpResponseMessage> GetAsync(string url, NameValueCollection headers = null)
        {
            var request = CreateNewRequest(HttpMethod.Get, url, headers);
            return SendAsync(request);
        }

        public Task<HttpResponseMessage> PostAsync<T>(string url, T body, NameValueCollection headers = null)
        {
            var request = CreateNewRequest(HttpMethod.Post, url, headers, body);
            return SendAsync(request);
        }

        public Task<HttpResponseMessage> PutAsync<T>(string url, T body, NameValueCollection headers = null)
        {
            var request = CreateNewRequest(HttpMethod.Put, url, headers, body);
            return SendAsync(request);
        }

        public Task<HttpResponseMessage> DeleteAsync(string url, NameValueCollection headers = null)
        {
            var request = CreateNewRequest(HttpMethod.Delete, url, headers);
            return SendAsync(request);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _client.SendAsync(request);
        }

        private HttpRequestMessage CreateNewRequest(HttpMethod httpMethod, string url, NameValueCollection headers)
        {
            url = url ?? string.Empty;
            var request = new HttpRequestMessage { Method = httpMethod, RequestUri = new Uri(_baseUrl + url) };
            return request.AppendHeaders(DefaultRequestHeaders, headers);
        }

        private HttpRequestMessage CreateNewRequest<T>(HttpMethod httpMethod, string url, NameValueCollection headers, T body)
        {
            var request = CreateNewRequest(httpMethod, url, headers);
            if (body != null)
                request.Content = new ObjectContent<T>(body, new JsonMediaTypeFormatter());
            return request;
        }
    }
}
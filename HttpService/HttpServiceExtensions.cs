namespace GuaranteedRate.Net.Http.HttpService
{
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net.Http;

    public static class HttpServiceExtensions
    {
        public static HttpRequestMessage AppendHeaders(this HttpRequestMessage request, params NameValueCollection[] headerCollections)
        {
            if (request == null || headerCollections == null)
                return request;

            foreach (var headers in headerCollections.Where(collection => collection != null))
            {
                foreach (var key in headers.AllKeys)
                {
                    request.Headers.TryAddWithoutValidation(key, headers[key]);
                }
            }

            return request;
        }
    }
}
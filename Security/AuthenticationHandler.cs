namespace GuaranteedRate.Net.Http.Security
{
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IPrincipalService _principalService;
        private static readonly GenericPrincipal UnauthenticatedPrincipal = new GenericPrincipal(new GenericIdentity(""), new string[0]);

        public AuthenticationHandler(IPrincipalService principalService)
        {
            _principalService = principalService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Thread.CurrentPrincipal = UnauthenticatedPrincipal;
            if (HttpContext.Current != null)
                HttpContext.Current.User = UnauthenticatedPrincipal;

            var principal = await _principalService.GetPrincipal(request);

            if (principal == null)
                return await base.SendAsync(request, cancellationToken);

            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
                HttpContext.Current.User = principal;

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
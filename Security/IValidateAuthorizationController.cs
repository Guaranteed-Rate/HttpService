namespace GuaranteedRate.Net.Http.Security
{
    using System.Web.Http.Controllers;
    using System.Web.Http.Routing;

    public interface IValidateAuthorizationController : IHttpController
    {
        bool IsValid(IHttpRouteData routeData);
    }
}
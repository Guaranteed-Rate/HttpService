namespace GuaranteedRate.Net.Http.Security
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (!base.IsAuthorized(actionContext))
                return false;

            var controller = actionContext.ControllerContext.Controller as IValidateAuthorizationController;
            return controller != null && controller.IsValid(actionContext.ControllerContext.RouteData);
        }
    }
}

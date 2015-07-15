namespace GuaranteedRate.Net.Http.Security
{
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading.Tasks;

    public interface IPrincipalService
    {
        Task<IPrincipal> GetPrincipal(HttpRequestMessage request);
    }
}
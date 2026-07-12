using System.Security.Claims;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Entity;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class GenerateTokenCommand(string applicationCode, ApplicationUser applicationUser, IEnumerable<string> scopes) : CommandBase<ClaimsPrincipal>
    {
        public string ApplicationCode { get; set; } = applicationCode;
        public ApplicationUser ApplicationUser { get; set; } = applicationUser;
        public IEnumerable<string> Scopes { get; set; } = scopes;
    }
}

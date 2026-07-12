using System.Security.Claims;
using __NAMESPACE__.Dto.Base;

namespace __NAMESPACE__.Dto.User
{
    public class LoginResultDto : ReturnUrlDto
    {
        public Dictionary<string, string?> AuthProperties { get; set; } = [];
        public ClaimsPrincipal ClaimsPrincipal { get; set; } = null!;
    }
}

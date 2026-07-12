using System.Text.Json.Serialization;
using __NAMESPACE__.Dto.Base;

namespace __NAMESPACE__.Dto.User
{
    public class LoginDto : ReturnUrlDto
    {
        public string? ApplicationCode { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
        [JsonIgnore]
        public IEnumerable<string>? Scopes { get; set; } = [];
    }
}

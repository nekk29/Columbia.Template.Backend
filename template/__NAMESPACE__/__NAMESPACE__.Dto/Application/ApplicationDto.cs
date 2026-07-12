namespace __NAMESPACE__.Dto.Application
{
    public class ApplicationDto
    {
        public string? ClientId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? LogoUri { get; set; } = null!;
        public bool IncludeClient { get; set; }
        public string? ApplicationUri { get; set; } = null!;
        public string? SigninRedirectUri { get; set; } = null!;
        public string? RefreshRedirectUri { get; set; } = null!;
        public string? PostLogoutRedirectUri { get; set; } = null!;
        public string? ClientSecret { get; set; } = null!;
        public bool ClientSecretUpdate { get; set; } = false;
        public int AccessTokenLifetime { get; set; } = 3600;
    }
}

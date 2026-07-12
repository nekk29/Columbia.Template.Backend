namespace __NAMESPACE__.Dto.Client
{
    public class CreateOrUpdateClientDto
    {
        public string ApplicationCode { get; set; } = null!;
        public string OldApplicationCode { get; set; } = null!;
        public string ApplicationName { get; set; } = null!;
        public string? ApplicationUri { get; set; } = null!;
        public string? ApplicationLogoUri { get; set; } = null!;
        public string? SigninRedirectUri { get; set; } = null!;
        public string? RefreshRedirectUri { get; set; } = null!;
        public string? PostLogoutRedirectUri { get; set; } = null!;
        public string ClientType { get; set; } = null!;
        public string? ClientSecret { get; set; } = null!;
        public bool ClientSecretUpdate { get; set; } = false;
        public int AccessTokenLifetime { get; set; } = 3600;
    }
}

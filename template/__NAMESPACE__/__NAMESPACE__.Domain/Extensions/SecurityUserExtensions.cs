using __NAMESPACE__.Dto.Base;
using Microsoft.AspNetCore.Http;

namespace __NAMESPACE__.Domain.Extensions
{
    public static class SecurityUserExtensions
    {
        public static string ReturnUrlOrDefault(this ReturnUrlDto returnUrlDto)
            => !string.IsNullOrEmpty(returnUrlDto.ReturnUrl) ? returnUrlDto.ReturnUrl : "/";

        public static string GetBaseUrl(this IHttpContextAccessor httpContextAccessor)
            => $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}";
    }
}

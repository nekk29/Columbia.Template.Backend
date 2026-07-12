using __NAMESPACE__.Dto.Base;

namespace __NAMESPACE__.Dto.User
{
    public class ResetPasswordDto : ReturnUrlDto
    {
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? ConfirmPassword { get; set; } = null!;
    }
}

using __NAMESPACE__.Dto.Base;

namespace __NAMESPACE__.Dto.User
{
    public class ForgotPasswordDto : ReturnUrlDto
    {
        public string Email { get; set; } = null!;
    }
}

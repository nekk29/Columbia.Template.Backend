namespace __NAMESPACE__.Dto.User
{
    public class SearchUserFilterDto
    {
        public Guid? Id { get; set; } = null!;
        public string? Query { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? UserName { get; set; } = null!;
        public string? NormalizedUserName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? NormalizedEmail { get; set; } = null!;
        public bool? EmailConfirmed { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public bool? PhoneNumberConfirmed { get; set; } = null!;
        public bool? TwoFactorEnabled { get; set; } = null!;
        public DateTimeOffset? LockoutEnd { get; set; } = null!;
        public bool? LockoutEnabled { get; set; } = null!;
        public int? AccessFailedCount { get; set; } = null!;
    }
}

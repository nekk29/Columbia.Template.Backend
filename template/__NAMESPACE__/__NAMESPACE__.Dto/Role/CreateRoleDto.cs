namespace __NAMESPACE__.Dto.Role
{
    public class CreateRoleDto
    {
        public Guid ApplicationId { get; set; }
        public string Name { get; set; } = null!;
        public string? NormalizedName { get; set; } = null!;
    }
}

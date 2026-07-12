namespace __NAMESPACE__.Dto.Role
{
    public class GetRoleDto : RoleDto
    {
        public Guid Id { get; set; }
        public string ApplicationCode { get; set; } = null!;
        public string ApplicationName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}

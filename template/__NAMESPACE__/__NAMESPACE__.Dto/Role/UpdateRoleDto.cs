namespace __NAMESPACE__.Dto.Role
{
    public class UpdateRoleDto : RoleDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}

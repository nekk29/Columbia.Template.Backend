namespace __NAMESPACE__.Dto.User
{
    public class GetUserDto : UserDto
    {
        public Guid Id { get; set; }
        public IEnumerable<Guid> RoleIds { get; set; } = [];
        public bool IsActive { get; set; }
    }
}

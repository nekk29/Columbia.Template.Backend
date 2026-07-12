namespace __NAMESPACE__.Dto.User
{
    public class SearchUserDto : UserDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}

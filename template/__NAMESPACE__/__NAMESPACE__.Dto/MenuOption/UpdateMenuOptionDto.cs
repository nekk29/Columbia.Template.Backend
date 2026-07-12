namespace __NAMESPACE__.Dto.MenuOption
{
    public class UpdateMenuOptionDto : MenuOptionDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}

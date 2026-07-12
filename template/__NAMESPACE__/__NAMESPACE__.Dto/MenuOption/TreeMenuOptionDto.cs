namespace __NAMESPACE__.Dto.MenuOption
{
    public class TreeMenuOptionDto : GetMenuOptionDto
    {
        public IEnumerable<TreeMenuOptionDto> Children { get; set; } = null!;
    }
}

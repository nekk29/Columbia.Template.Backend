namespace __NAMESPACE__.Dto.MenuOption
{
    public class MenuOptionDto
    {
        public Guid ApplicationId { get; set; }
        public Guid? ParentMenuOptionId { get; set; }
        public Guid ActionId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? MenuUri { get; set; }
        public string? MenuIcon { get; set; }
        public int SortOrder { get; set; }
    }
}

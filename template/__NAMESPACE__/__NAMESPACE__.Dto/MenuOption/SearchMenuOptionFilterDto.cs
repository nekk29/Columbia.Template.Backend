namespace __NAMESPACE__.Dto.MenuOption
{
    public class SearchMenuOptionFilterDto
    {
        public Guid? Id { get; set; } = null!;
        public Guid? ApplicationId { get; set; } = null!;
        public Guid? ParentMenuOptionId { get; set; } = null!;
        public Guid? ActionId { get; set; } = null!;
        public string? Code { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string? MenuUri { get; set; } = null!;
        public string? MenuIcon { get; set; } = null!;
        public int? SortOrder { get; set; } = null!;
    }
}

namespace __NAMESPACE__.Dto.MenuOption
{
    public class GetMenuOptionDto : MenuOptionDto
    {
        public Guid Id { get; set; }
        public string ApplicationCode { get; set; } = null!;
        public string ApplicationName { get; set; } = null!;
        public Guid ModuleId { get; set; }
        public string ModuleCode { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public string ActionCode { get; set; } = null!;
        public string ActionName { get; set; } = null!;
        public string? ParentCode { get; set; }
        public string? ParentName { get; set; }
        public bool IsActive { get; set; }
    }
}

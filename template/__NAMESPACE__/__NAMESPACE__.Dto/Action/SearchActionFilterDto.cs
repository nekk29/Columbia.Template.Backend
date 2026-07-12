namespace __NAMESPACE__.Dto.Action
{
    public class SearchActionFilterDto
    {
        public Guid? Id { get; set; } = null!;
        public Guid? ModuleId { get; set; } = null!;
        public Guid? ParentActionId { get; set; } = null!;
        public string? Code { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
    }
}

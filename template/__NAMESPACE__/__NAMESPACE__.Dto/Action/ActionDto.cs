namespace __NAMESPACE__.Dto.Action
{
    public class ActionDto
    {
        public Guid ModuleId { get; set; }
        public Guid? ParentActionId { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
    }
}

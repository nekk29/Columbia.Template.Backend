namespace __NAMESPACE__.Dto.Module
{
    public class SearchModuleFilterDto
    {
        public Guid? Id { get; set; } = null!;
        public Guid? ApplicationId { get; set; } = null!;
        public string? Query { get; set; } = null!;
        public string? Code { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
    }
}

namespace __NAMESPACE__.Dto.Role
{
    public class SearchRoleFilterDto
    {
        public Guid? ApplicationId { get; set; } = null!;
        public string? Query { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? NormalizedName { get; set; } = null!;
    }
}

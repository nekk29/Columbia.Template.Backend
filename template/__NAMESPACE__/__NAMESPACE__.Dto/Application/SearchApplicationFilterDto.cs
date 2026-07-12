namespace __NAMESPACE__.Dto.Application
{
    public class SearchApplicationFilterDto
    {
        public string? Query { get; set; } = null!;
        public Guid? Id { get; set; } = null!;
        public string? Code { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? LogoUri { get; set; } = null!;
    }
}

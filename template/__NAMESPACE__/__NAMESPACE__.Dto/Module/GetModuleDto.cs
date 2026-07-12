namespace __NAMESPACE__.Dto.Module
{
    public class GetModuleDto : ModuleDto
    {
        public Guid Id { get; set; }
        public string ApplicationCode { get; set; } = null!;
        public string ApplicationName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}

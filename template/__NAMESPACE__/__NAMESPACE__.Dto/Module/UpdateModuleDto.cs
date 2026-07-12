namespace __NAMESPACE__.Dto.Module
{
    public class UpdateModuleDto : ModuleDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}

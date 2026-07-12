namespace __NAMESPACE__.Dto.Application
{
    public class UpdateApplicationDto : ApplicationDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}

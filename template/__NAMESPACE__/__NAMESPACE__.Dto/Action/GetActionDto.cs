namespace __NAMESPACE__.Dto.Action
{
    public class GetActionDto : ActionDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}

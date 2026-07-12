using __NAMESPACE__.Dto.Action;

namespace __NAMESPACE__.Dto.Module
{
    public class ListModuleDto : GetModuleDto
    {
        public IEnumerable<GetActionDto> Actions { get; set; } = null!;
    }
}

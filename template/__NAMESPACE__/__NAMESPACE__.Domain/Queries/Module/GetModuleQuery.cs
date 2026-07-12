using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Module;

namespace __NAMESPACE__.Domain.Queries.Module
{
    public class GetModuleQuery(Guid id) : QueryBase<GetModuleDto>
    {
        public Guid Id { get; set; } = id;
    }
}

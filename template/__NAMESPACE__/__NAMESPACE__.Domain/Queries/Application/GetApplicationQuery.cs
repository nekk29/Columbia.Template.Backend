using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Application;

namespace __NAMESPACE__.Domain.Queries.Application
{
    public class GetApplicationQuery(Guid id) : QueryBase<GetApplicationDto>
    {
        public Guid Id { get; set; } = id;
    }
}

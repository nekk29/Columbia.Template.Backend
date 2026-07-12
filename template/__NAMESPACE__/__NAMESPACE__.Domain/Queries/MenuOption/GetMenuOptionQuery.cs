using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.MenuOption;

namespace __NAMESPACE__.Domain.Queries.MenuOption
{
    public class GetMenuOptionQuery(Guid id) : QueryBase<GetMenuOptionDto>
    {
        public Guid Id { get; set; } = id;
    }
}

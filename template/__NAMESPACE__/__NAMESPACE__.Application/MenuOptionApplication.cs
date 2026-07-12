using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Application.Base;
using __NAMESPACE__.Domain.Commands.MenuOption;
using __NAMESPACE__.Domain.MenuOption;
using __NAMESPACE__.Domain.Queries.MenuOption;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.MenuOption;
using MediatR;

namespace __NAMESPACE__.Application
{
    public class MenuOptionApplication(IMediator mediator) : ApplicationBase(mediator), IMenuOptionApplication
    {
        public async Task<ResponseDto<GetMenuOptionDto>> Create(CreateMenuOptionDto createDto)
            => await _mediator.Send(new CreateMenuOptionCommand(createDto));

        public async Task<ResponseDto<GetMenuOptionDto>> Update(UpdateMenuOptionDto updateDto)
            => await _mediator.Send(new UpdateMenuOptionCommand(updateDto));

        public async Task<ResponseDto> Delete(Guid id)
            => await _mediator.Send(new DeleteMenuOptionCommand(id));

        public async Task<ResponseDto<GetMenuOptionDto>> Get(Guid id)
            => await _mediator.Send(new GetMenuOptionQuery(id));

        public async Task<ResponseDto<IEnumerable<ListMenuOptionDto>>> List(string applicationCode)
            => await _mediator.Send(new ListMenuOptionQuery(applicationCode));

        public async Task<ResponseDto<IEnumerable<ListMenuOptionDto>>> ListAll(string applicationCode)
            => await _mediator.Send(new ListMenuOptionQuery(applicationCode, true));

        public async Task<ResponseDto<IEnumerable<TreeMenuOptionDto>>> Tree(string applicationCode)
            => await _mediator.Send(new TreeMenuOptionsQuery(applicationCode));

        public async Task<ResponseDto<IEnumerable<TreeMenuOptionDto>>> TreeAll(string applicationCode)
            => await _mediator.Send(new TreeMenuOptionsQuery(applicationCode, true));

        public async Task<ResponseDto<SearchResultDto<SearchMenuOptionDto>>> Search(SearchParamsDto<SearchMenuOptionFilterDto> searchParams)
            => await _mediator.Send(new SearchMenuOptionQuery(searchParams));
    }
}

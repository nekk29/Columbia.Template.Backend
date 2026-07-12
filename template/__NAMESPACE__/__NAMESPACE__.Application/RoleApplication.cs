using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Application.Base;
using __NAMESPACE__.Domain.Commands.Role;
using __NAMESPACE__.Domain.Queries.Role;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Role;
using MediatR;

namespace __NAMESPACE__.Application
{
    public class RoleApplication(IMediator mediator) : ApplicationBase(mediator), IRoleApplication
    {
        public async Task<ResponseDto<GetRoleDto>> Create(CreateRoleDto createDto)
            => await _mediator.Send(new CreateRoleCommand(createDto));

        public async Task<ResponseDto<GetRoleDto>> Update(UpdateRoleDto updateDto)
            => await _mediator.Send(new UpdateRoleCommand(updateDto));

        public async Task<ResponseDto> Delete(Guid id)
            => await _mediator.Send(new DeleteRoleCommand(id));

        public async Task<ResponseDto<GetRoleDto>> Get(Guid id)
            => await _mediator.Send(new GetRoleQuery(id));

        public async Task<ResponseDto<IEnumerable<ListRoleDto>>> List()
            => await _mediator.Send(new ListRoleQuery());

        public async Task<ResponseDto<IEnumerable<ListRoleDto>>> List(Guid applicationId)
            => await _mediator.Send(new ListRoleQuery(applicationId));

        public async Task<ResponseDto<SearchResultDto<SearchRoleDto>>> Search(SearchParamsDto<SearchRoleFilterDto> searchParams)
            => await _mediator.Send(new SearchRoleQuery(searchParams));
    }
}

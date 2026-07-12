using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Application.Base;
using __NAMESPACE__.Domain.Commands.Module;
using __NAMESPACE__.Domain.Queries.Module;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Module;
using MediatR;

namespace __NAMESPACE__.Application
{
    public class ModuleApplication(IMediator mediator) : ApplicationBase(mediator), IModuleApplication
    {
        public async Task<ResponseDto<GetModuleDto>> Create(CreateModuleDto createDto)
            => await _mediator.Send(new CreateModuleCommand(createDto));

        public async Task<ResponseDto<GetModuleDto>> Update(UpdateModuleDto updateDto)
            => await _mediator.Send(new UpdateModuleCommand(updateDto));

        public async Task<ResponseDto> Delete(Guid id)
            => await _mediator.Send(new DeleteModuleCommand(id));

        public async Task<ResponseDto<GetModuleDto>> Get(Guid id)
            => await _mediator.Send(new GetModuleQuery(id));

        public async Task<ResponseDto<IEnumerable<ListModuleDto>>> List()
            => await _mediator.Send(new ListModuleQuery());

        public async Task<ResponseDto<IEnumerable<ListModuleDto>>> List(Guid applicationId)
            => await _mediator.Send(new ListModuleQuery(applicationId));

        public async Task<ResponseDto<IEnumerable<ListModuleDto>>> ListSimple(Guid applicationId)
            => await _mediator.Send(new ListModuleQuery(applicationId, false));

        public async Task<ResponseDto<SearchResultDto<SearchModuleDto>>> Search(SearchParamsDto<SearchModuleFilterDto> searchParams)
            => await _mediator.Send(new SearchModuleQuery(searchParams));
    }
}

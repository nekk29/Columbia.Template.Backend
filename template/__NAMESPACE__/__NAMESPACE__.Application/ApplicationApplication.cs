using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Application.Base;
using __NAMESPACE__.Domain.Commands.Application;
using __NAMESPACE__.Domain.Queries.Application;
using __NAMESPACE__.Dto.Application;
using __NAMESPACE__.Dto.Base;
using MediatR;

namespace __NAMESPACE__.Application
{
    public class ApplicationApplication(IMediator mediator) : ApplicationBase(mediator), IApplicationApplication
    {
        public async Task<ResponseDto<GetApplicationDto>> Create(CreateApplicationDto createDto)
            => await _mediator.Send(new CreateApplicationCommand(createDto));

        public async Task<ResponseDto<GetApplicationDto>> Update(UpdateApplicationDto updateDto)
            => await _mediator.Send(new UpdateApplicationCommand(updateDto));

        public async Task<ResponseDto> Delete(Guid id)
            => await _mediator.Send(new DeleteApplicationCommand(id));

        public async Task<ResponseDto<GetApplicationDto>> Get(Guid id)
            => await _mediator.Send(new GetApplicationQuery(id));

        public async Task<ResponseDto<IEnumerable<ListApplicationDto>>> List()
            => await _mediator.Send(new ListApplicationQuery());

        public async Task<ResponseDto<SearchResultDto<SearchApplicationDto>>> Search(SearchParamsDto<SearchApplicationFilterDto> searchParams)
            => await _mediator.Send(new SearchApplicationQuery(searchParams));
    }
}

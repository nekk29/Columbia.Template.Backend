using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;

namespace __NAMESPACE__.Domain.Commands.Application
{
    public class DeleteApplicationCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        DeleteApplicationCommandValidator validator,
        IRepository<Entity.Application> applicationRepository
    ) : CommandHandlerBase<DeleteApplicationCommand>(unitOfWork, mapper, validator)
    {
        public override async Task<ResponseDto> HandleCommand(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var application = await applicationRepository.GetByAsync(x => x.Id == request.Id);

            if (application != null)
            {
                await applicationRepository.DeleteAsync(application);
                await applicationRepository.SaveAsync();

            }

            response.AddOkResult(Resources.Common.DeleteSuccessMessage);

            return response;
        }
    }
}

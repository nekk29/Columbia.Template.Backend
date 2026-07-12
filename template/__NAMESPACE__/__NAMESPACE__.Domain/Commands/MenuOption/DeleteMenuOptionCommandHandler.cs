using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;

namespace __NAMESPACE__.Domain.Commands.MenuOption
{
    public class DeleteMenuOptionCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        DeleteMenuOptionCommandValidator validator,
        IRepository<Entity.MenuOption> menuOptionRepository
    ) : CommandHandlerBase<DeleteMenuOptionCommand>(unitOfWork, mapper, validator)
    {
        public override async Task<ResponseDto> HandleCommand(DeleteMenuOptionCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var menuOption = await menuOptionRepository.GetByAsync(x => x.Id == request.Id);

            if (menuOption != null)
            {
                await menuOptionRepository.DeleteAsync(menuOption);
                await menuOptionRepository.SaveAsync();
            }

            response.AddOkResult(Resources.Common.DeleteSuccessMessage);

            return response;
        }
    }
}

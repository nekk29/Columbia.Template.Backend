using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class DeleteUserCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        DeleteUserCommandValidator validator,
        IRepository<Entity.ApplicationUser> applicationUserRepository
    ) : CommandHandlerBase<DeleteUserCommand>(unitOfWork, mapper, validator)
    {
        public override async Task<ResponseDto> HandleCommand(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var user = await applicationUserRepository.GetByAsync(x => x.Id == request.Id);

            if (user != null)
            {
                await applicationUserRepository.DeleteAsync(user);
                await applicationUserRepository.SaveAsync();
            }

            response.AddOkResult(Resources.Common.DeleteSuccessMessage);

            return response;
        }
    }
}

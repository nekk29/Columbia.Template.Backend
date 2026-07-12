using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;

namespace __NAMESPACE__.Domain.Commands.Role
{
    public class DeleteRoleCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        DeleteRoleCommandValidator validator,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : CommandHandlerBase<DeleteRoleCommand>(unitOfWork, mapper, validator)
    {
        public override async Task<ResponseDto> HandleCommand(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var role = await applicationRoleRepository.GetByAsync(x => x.Id == request.Id);

            if (role != null)
            {
                await applicationRoleRepository.DeleteAsync(role);
                await applicationRoleRepository.SaveAsync();
            }

            response.AddOkResult(Resources.Common.DeleteSuccessMessage);

            return response;
        }
    }
}

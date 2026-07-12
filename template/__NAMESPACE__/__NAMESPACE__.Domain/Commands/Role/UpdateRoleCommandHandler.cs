using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Role;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;

namespace __NAMESPACE__.Domain.Commands.Role
{
    public class UpdateRoleCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UpdateRoleCommandValidator validator,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : CommandHandlerBase<UpdateRoleCommand, GetRoleDto>(unitOfWork, mapper, validator)
    {
        public override async Task<ResponseDto<GetRoleDto>> HandleCommand(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetRoleDto>();

            var role = await applicationRoleRepository.GetByAsync(x => x.Id == request.UpdateDto.Id);
            if (role != null)
            {
                _mapper?.Map(request.UpdateDto, role);
                role.NormalizedName = role.Name?.ToUpper();
                await applicationRoleRepository.UpdateAsync(role);
            }

            var roleDto = _mapper?.Map<GetRoleDto>(role);
            if (roleDto != null) response.UpdateData(roleDto);

            response.AddOkResult(Resources.Common.UpdateSuccessMessage);

            return await Task.FromResult(response);
        }
    }
}

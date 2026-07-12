using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.User;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;
using Microsoft.AspNetCore.Identity;

namespace __NAMESPACE__.Domain.Commands.User
{
    public class UpdateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UpdateUserCommandValidator validator,
        UserManager<Entity.ApplicationUser> userManager,
        IRepository<Entity.ApplicationUser> applicationUserRepository,
        IRepository<Entity.ApplicationRole> applicationRoleRepository
    ) : CommandHandlerBase<UpdateUserCommand, GetUserDto>(unitOfWork, mapper, validator)
    {
        protected override bool UseTransaction => false;

        public override async Task<ResponseDto<GetUserDto>> HandleCommand(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetUserDto>();

            var user = await applicationUserRepository.GetByAsync(x => x.Id == request.UpdateDto.Id);
            if (user != null)
            {
                _mapper?.Map(request.UpdateDto, user);

                user.NormalizedEmail = user.Email?.ToUpper();
                user.NormalizedUserName = user.UserName?.ToUpper();

                await applicationUserRepository.UpdateAsync(user);
                await applicationUserRepository.SaveAsync();

                response.AddOkResult(Resources.Common.UpdateSuccessMessage);

                var roleIds = request.UpdateDto.RoleIds ?? [];
                var roles = await applicationRoleRepository.FindByAsNoTrackingAsync(x => roleIds.Contains(x.Id));

                if (roles.Any())
                {
                    var applicationUser = await userManager.FindByEmailAsync(user.Email!);
                    var roleNames = await userManager.GetRolesAsync(applicationUser!);

                    var rolesToRemove = roleNames.Where(r => !roles.Select(x => x.Name).Contains(r));
                    if (rolesToRemove.Any())
                    {
                        var removeRolesResult = await userManager.RemoveFromRolesAsync(applicationUser!, roleNames);
                        if (!removeRolesResult.Succeeded)
                            removeRolesResult.Errors.ToList().ForEach(e => { response.AddErrorResult($"{e.Code}: {e.Description}"); });
                    }

                    var rolesToAdd = roles.Where(x => !roleNames.Contains(x.Name!)).Select(x => x.NormalizedName);
                    if (rolesToAdd.Any())
                    {
                        var addRolesResult = await userManager.AddToRolesAsync(applicationUser!, rolesToAdd!);
                        if (!addRolesResult.Succeeded)
                            addRolesResult.Errors.ToList().ForEach(e => { response.AddErrorResult($"{e.Code}: {e.Description}"); });
                    }
                }
            }

            var userDto = _mapper?.Map<GetUserDto>(user);
            if (userDto != null) response.UpdateData(userDto);

            return await Task.FromResult(response);
        }
    }
}

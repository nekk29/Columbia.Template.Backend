using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.MenuOption;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;

namespace __NAMESPACE__.Domain.Commands.MenuOption
{
    public class UpdateMenuOptionCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UpdateMenuOptionCommandValidator validator,
        IRepository<Entity.MenuOption> menuOptionRepository
    ) : CommandHandlerBase<UpdateMenuOptionCommand, GetMenuOptionDto>(unitOfWork, mapper, validator)
    {
        public override async Task<ResponseDto<GetMenuOptionDto>> HandleCommand(UpdateMenuOptionCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetMenuOptionDto>();

            var menuOption = await menuOptionRepository.GetByAsync(x => x.Id == request.UpdateDto.Id);
            if (menuOption != null)
            {
                _mapper?.Map(request.UpdateDto, menuOption);
                await menuOptionRepository.UpdateAsync(menuOption);
            }

            var menuOptionDto = _mapper?.Map<GetMenuOptionDto>(menuOption);
            if (menuOptionDto != null) response.UpdateData(menuOptionDto);

            response.AddOkResult(Resources.Common.UpdateSuccessMessage);

            return await Task.FromResult(response);
        }
    }
}

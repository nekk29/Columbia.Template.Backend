using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Action;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;

namespace __NAMESPACE__.Domain.Commands.Action
{
    public class UpdateActionCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UpdateActionCommandValidator validator,
        IRepository<Entity.Action> actionRepository
    ) : CommandHandlerBase<UpdateActionCommand, GetActionDto>(unitOfWork, mapper, validator)
    {
        public override async Task<ResponseDto<GetActionDto>> HandleCommand(UpdateActionCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetActionDto>();

            var action = await actionRepository.GetByAsync(x => x.Id == request.UpdateDto.Id);
            if (action != null)
            {
                _mapper?.Map(request.UpdateDto, action);
                await actionRepository.UpdateAsync(action);
            }

            var actionDto = _mapper?.Map<GetActionDto>(action);
            if (actionDto != null) response.UpdateData(actionDto);

            response.AddOkResult(Resources.Common.UpdateSuccessMessage);

            return await Task.FromResult(response);
        }
    }
}

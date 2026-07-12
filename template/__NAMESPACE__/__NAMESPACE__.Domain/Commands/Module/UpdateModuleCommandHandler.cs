using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Module;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;

namespace __NAMESPACE__.Domain.Commands.Module
{
    public class UpdateModuleCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UpdateModuleCommandValidator validator,
        IRepository<Entity.Module> moduleRepository
    ) : CommandHandlerBase<UpdateModuleCommand, GetModuleDto>(unitOfWork, mapper, validator)
    {
        public override async Task<ResponseDto<GetModuleDto>> HandleCommand(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetModuleDto>();

            var module = await moduleRepository.GetByAsync(x => x.Id == request.UpdateDto.Id);
            if (module != null)
            {
                _mapper?.Map(request.UpdateDto, module);
                await moduleRepository.UpdateAsync(module);
            }

            var moduleDto = _mapper?.Map<GetModuleDto>(module);
            if (moduleDto != null) response.UpdateData(moduleDto);

            response.AddOkResult(Resources.Common.UpdateSuccessMessage);

            return await Task.FromResult(response);
        }
    }
}

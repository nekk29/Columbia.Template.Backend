using AutoMapper;
using __NAMESPACE__.Domain.Queries.Base;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Module;
using __NAMESPACE__.Repository.Abstractions.Base;

namespace __NAMESPACE__.Domain.Queries.Module
{
    public class GetModuleQueryHandler(
        IMapper mapper,
        IRepository<Entity.Module> moduleRepository
    ) : QueryHandlerBase<GetModuleQuery, GetModuleDto>(mapper)
    {
        protected override async Task<ResponseDto<GetModuleDto>> HandleQuery(GetModuleQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetModuleDto>();
            var module = await moduleRepository.GetByAsync(x => x.Id == request.Id, x => x.Application);
            var moduleDto = _mapper?.Map<GetModuleDto>(module);

            if (moduleDto != null)
                response.UpdateData(moduleDto);

            return await Task.FromResult(response);
        }
    }
}

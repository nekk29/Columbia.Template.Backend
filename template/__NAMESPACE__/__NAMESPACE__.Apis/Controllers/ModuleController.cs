using __NAMESPACE__.Apis.Controllers.Base;
using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Module;
using Microsoft.AspNetCore.Mvc;

namespace __NAMESPACE__.Apis.Controllers
{
    [ApiController]
    [Security.Authorize]
    [Route("api/module")]
    public class ModuleController(
        IServiceProvider serviceProvider,
        IModuleApplication moduleApplication
    ) : ApiControllerBase(serviceProvider)
    {
        [HttpPost]
        public async Task<ResponseDto<GetModuleDto>> Create(CreateModuleDto createDto)
            => await moduleApplication.Create(createDto);

        [HttpPut]
        public async Task<ResponseDto<GetModuleDto>> Update(UpdateModuleDto updateDto)
            => await moduleApplication.Update(updateDto);

        [HttpDelete("{id}")]
        public async Task<ResponseDto> Delete(Guid id)
            => await moduleApplication.Delete(id);

        [HttpGet("{id}")]
        public async Task<ResponseDto<GetModuleDto>> Get(Guid id)
            => await moduleApplication.Get(id);

        [HttpGet("list")]
        public async Task<ResponseDto<IEnumerable<ListModuleDto>>> List()
            => await moduleApplication.List();

        [HttpGet("{applicationId}/list")]
        public async Task<ResponseDto<IEnumerable<ListModuleDto>>> List(Guid applicationId)
            => await moduleApplication.List(applicationId);

        [HttpGet("{applicationId}/list-simple")]
        public async Task<ResponseDto<IEnumerable<ListModuleDto>>> ListSimple(Guid applicationId)
            => await moduleApplication.ListSimple(applicationId);

        [HttpPost("search")]
        public async Task<ResponseDto<SearchResultDto<SearchModuleDto>>> Search(SearchParamsDto<SearchModuleFilterDto> searchParams)
            => await moduleApplication.Search(searchParams);
    }
}

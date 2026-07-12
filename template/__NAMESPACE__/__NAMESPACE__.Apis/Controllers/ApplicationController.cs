using __NAMESPACE__.Apis.Controllers.Base;
using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Dto.Application;
using __NAMESPACE__.Dto.Base;
using Microsoft.AspNetCore.Mvc;

namespace __NAMESPACE__.Apis.Controllers
{
    [ApiController]
    [Security.Authorize]
    [Route("api/application")]
    public class ApplicationController(
        IServiceProvider serviceProvider,
        IApplicationApplication applicationApplication
    ) : ApiControllerBase(serviceProvider)
    {
        [HttpPost]
        public async Task<ResponseDto<GetApplicationDto>> Create(CreateApplicationDto createDto)
            => await applicationApplication.Create(createDto);

        [HttpPut]
        public async Task<ResponseDto<GetApplicationDto>> Update(UpdateApplicationDto updateDto)
            => await applicationApplication.Update(updateDto);

        [HttpDelete("{id}")]
        public async Task<ResponseDto> Delete(Guid id)
            => await applicationApplication.Delete(id);

        [HttpGet("{id}")]
        public async Task<ResponseDto<GetApplicationDto>> Get(Guid id)
            => await applicationApplication.Get(id);

        [HttpGet("list")]
        public async Task<ResponseDto<IEnumerable<ListApplicationDto>>> List()
            => await applicationApplication.List();

        [HttpPost("search")]
        public async Task<ResponseDto<SearchResultDto<SearchApplicationDto>>> Search(SearchParamsDto<SearchApplicationFilterDto> searchParams)
            => await applicationApplication.Search(searchParams);
    }
}

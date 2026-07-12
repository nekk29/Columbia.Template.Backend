using __NAMESPACE__.Apis.Controllers.Base;
using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.MenuOption;
using Microsoft.AspNetCore.Mvc;

namespace __NAMESPACE__.Apis.Controllers
{
    [ApiController]
    [Security.Authorize]
    [Route("api/menu-option")]
    public class MenuOptionController(
        IServiceProvider serviceProvider,
        IMenuOptionApplication menuOptionApplication
    ) : ApiControllerBase(serviceProvider)
    {
        [HttpPost]
        public async Task<ResponseDto<GetMenuOptionDto>> Create(CreateMenuOptionDto createDto)
            => await menuOptionApplication.Create(createDto);

        [HttpPut]
        public async Task<ResponseDto<GetMenuOptionDto>> Update(UpdateMenuOptionDto updateDto)
            => await menuOptionApplication.Update(updateDto);

        [HttpDelete("{id}")]
        public async Task<ResponseDto> Delete(Guid id)
            => await menuOptionApplication.Delete(id);

        [HttpGet("{id}")]
        public async Task<ResponseDto<GetMenuOptionDto>> Get(Guid id)
            => await menuOptionApplication.Get(id);

        [HttpGet("{applicationCode}/list")]
        public async Task<ResponseDto<IEnumerable<ListMenuOptionDto>>> List(string applicationCode)
            => await menuOptionApplication.List(applicationCode);

        [HttpGet("{applicationCode}/list-all")]
        public async Task<ResponseDto<IEnumerable<ListMenuOptionDto>>> ListAll(string applicationCode)
            => await menuOptionApplication.ListAll(applicationCode);

        [HttpGet("{applicationCode}/tree")]
        public async Task<ResponseDto<IEnumerable<TreeMenuOptionDto>>> Tree(string applicationCode)
            => await menuOptionApplication.Tree(applicationCode);

        [HttpGet("{applicationCode}/tree-all")]
        public async Task<ResponseDto<IEnumerable<TreeMenuOptionDto>>> TreeAll(string applicationCode)
            => await menuOptionApplication.TreeAll(applicationCode);

        [HttpPost("search")]
        public async Task<ResponseDto<SearchResultDto<SearchMenuOptionDto>>> Search(SearchParamsDto<SearchMenuOptionFilterDto> searchParams)
            => await menuOptionApplication.Search(searchParams);
    }
}

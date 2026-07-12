using __NAMESPACE__.Dto.Action;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.RestClient.Abstractions;
using __NAMESPACE__.RestClient.Base;

namespace __NAMESPACE__.RestClient.Implementation
{
    public class ActionRestService(IServiceProvider serviceProvider) : BaseService(serviceProvider), IActionRestService
    {
        protected override string ApiController => "api/action";

        public async Task<ResponseDto<GetActionDto>> Create(CreateActionDto createDto)
            => await Post<CreateActionDto, ResponseDto<GetActionDto>>(string.Empty, createDto)!;

        public async Task<ResponseDto<GetActionDto>> Update(UpdateActionDto updateDto)
            => await Put<UpdateActionDto, ResponseDto<GetActionDto>>(string.Empty, updateDto)!;

        public async Task<ResponseDto> Delete(Guid id)
            => await Delete<ResponseDto>($"/{id}")!;

        public async Task<ResponseDto<GetActionDto>> Get(Guid id)
            => await Get<ResponseDto<GetActionDto>>($"/{id}")!;

        public async Task<ResponseDto<IEnumerable<ListActionDto>>> List()
            => await Get<ResponseDto<IEnumerable<ListActionDto>>>("/list")!;

        public async Task<ResponseDto<SearchResultDto<SearchActionDto>>> Search(SearchParamsDto<SearchActionFilterDto> filter)
            => await Post<SearchParamsDto<SearchActionFilterDto>, ResponseDto<SearchResultDto<SearchActionDto>>>("/search", filter)!;
    }
}

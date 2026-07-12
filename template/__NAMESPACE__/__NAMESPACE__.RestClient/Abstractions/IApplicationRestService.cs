using __NAMESPACE__.Dto.Application;
using __NAMESPACE__.Dto.Base;

namespace __NAMESPACE__.RestClient.Abstractions
{
    public interface IApplicationRestService
    {
        Task<ResponseDto<GetApplicationDto>> Create(CreateApplicationDto createDto);
        Task<ResponseDto<GetApplicationDto>> Update(UpdateApplicationDto updateDto);
        Task<ResponseDto> Delete(Guid id);
        Task<ResponseDto<GetApplicationDto>> Get(Guid id);
        Task<ResponseDto<IEnumerable<ListApplicationDto>>> List();
        Task<ResponseDto<SearchResultDto<SearchApplicationDto>>> Search(SearchParamsDto<SearchApplicationFilterDto> filter);
    }
}

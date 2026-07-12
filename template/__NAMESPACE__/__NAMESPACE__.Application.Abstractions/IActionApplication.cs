using __NAMESPACE__.Dto.Action;
using __NAMESPACE__.Dto.Base;

namespace __NAMESPACE__.Application.Abstractions
{
    public interface IActionApplication
    {
        Task<ResponseDto<GetActionDto>> Create(CreateActionDto createDto);
        Task<ResponseDto<GetActionDto>> Update(UpdateActionDto updateDto);
        Task<ResponseDto> Delete(Guid id);
        Task<ResponseDto<GetActionDto>> Get(Guid id);
        Task<ResponseDto<IEnumerable<ListActionDto>>> List();
        Task<ResponseDto<IEnumerable<ListActionDto>>> List(Guid moduleId);
        Task<ResponseDto<SearchResultDto<SearchActionDto>>> Search(SearchParamsDto<SearchActionFilterDto> searchParams);
    }
}

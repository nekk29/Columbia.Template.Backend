using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Role;

namespace __NAMESPACE__.Application.Abstractions
{
    public interface IRoleApplication
    {
        Task<ResponseDto<GetRoleDto>> Create(CreateRoleDto createDto);
        Task<ResponseDto<GetRoleDto>> Update(UpdateRoleDto updateDto);
        Task<ResponseDto> Delete(Guid id);
        Task<ResponseDto<GetRoleDto>> Get(Guid id);
        Task<ResponseDto<IEnumerable<ListRoleDto>>> List();
        Task<ResponseDto<IEnumerable<ListRoleDto>>> List(Guid applicationId);
        Task<ResponseDto<SearchResultDto<SearchRoleDto>>> Search(SearchParamsDto<SearchRoleFilterDto> searchParams);
    }
}

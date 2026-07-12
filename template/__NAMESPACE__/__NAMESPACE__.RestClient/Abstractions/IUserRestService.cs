using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.User;

namespace __NAMESPACE__.RestClient.Abstractions
{
    public interface IUserRestService
    {
        Task<ResponseDto<GetUserDto>> Create(CreateUserDto createDto);
        Task<ResponseDto<GetUserDto>> Update(UpdateUserDto updateDto);
        Task<ResponseDto> Delete(Guid id);
        Task<ResponseDto<GetUserDto>> Get(Guid id);
        Task<ResponseDto<IEnumerable<ListUserDto>>> List();
        Task<ResponseDto<SearchResultDto<SearchUserDto>>> Search(SearchParamsDto<SearchUserFilterDto> filter);
        Task<ResponseDto<LoginResultDto>> Login(LoginDto loginDto);
    }
}

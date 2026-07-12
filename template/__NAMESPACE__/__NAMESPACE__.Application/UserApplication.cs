using __NAMESPACE__.Application.Abstractions;
using __NAMESPACE__.Application.Base;
using __NAMESPACE__.Domain.Commands.User;
using __NAMESPACE__.Domain.Queries.User;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.User;
using MediatR;

namespace __NAMESPACE__.Application
{
    public class UserApplication(IMediator mediator) : ApplicationBase(mediator), IUserApplication
    {
        public async Task<ResponseDto<GetUserDto>> Create(CreateUserDto createDto)
            => await _mediator.Send(new CreateUserCommand(createDto));

        public async Task<ResponseDto<GetUserDto>> Update(UpdateUserDto updateDto)
            => await _mediator.Send(new UpdateUserCommand(updateDto));

        public async Task<ResponseDto> Delete(Guid id)
            => await _mediator.Send(new DeleteUserCommand(id));

        public async Task<ResponseDto<GetUserDto>> Get(Guid id)
            => await _mediator.Send(new GetUserQuery(id));

        public async Task<ResponseDto<IEnumerable<ListUserDto>>> List()
            => await _mediator.Send(new ListUserQuery());

        public async Task<ResponseDto<SearchResultDto<SearchUserDto>>> Search(SearchParamsDto<SearchUserFilterDto> searchParams)
            => await _mediator.Send(new SearchUserQuery(searchParams));

        public async Task<ResponseDto<LoginResultDto>> Login(LoginDto loginDto)
            => await _mediator.Send(new LoginCommand(loginDto));

        public async Task<ResponseDto> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
            => await _mediator.Send(new ForgotPasswordCommand(forgotPasswordDto));

        public async Task<ResponseDto> ResetPassword(ResetPasswordDto resetPasswordDto)
            => await _mediator.Send(new ResetPasswordCommand(resetPasswordDto));
    }
}

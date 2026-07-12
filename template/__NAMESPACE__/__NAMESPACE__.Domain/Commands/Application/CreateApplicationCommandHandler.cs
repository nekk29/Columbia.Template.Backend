using AutoMapper;
using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Domain.Commands.Client;
using __NAMESPACE__.Dto.Application;
using __NAMESPACE__.Dto.Base;
using __NAMESPACE__.Dto.Client;
using __NAMESPACE__.Repository.Abstractions.Base;
using __NAMESPACE__.Repository.Abstractions.Transactions;
using MediatR;

namespace __NAMESPACE__.Domain.Commands.Application
{
    public class CreateApplicationCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator mediator,
        CreateApplicationCommandValidator validator,
        IRepository<Entity.Application> applicationRepository
    ) : CommandHandlerBase<CreateApplicationCommand, GetApplicationDto>(unitOfWork, mapper, mediator, validator)
    {
        public override async Task<ResponseDto<GetApplicationDto>> HandleCommand(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<GetApplicationDto>();
            var application = _mapper?.Map<Entity.Application>(request.CreateDto);

            if (application != null)
            {
                application.ApplicationUri = application.ApplicationUri?.EndsWith('/') == true
                    ? application.ApplicationUri[0..^1]
                    : application.ApplicationUri;

                if (!request.CreateDto.IncludeClient) application.ClientId = null!;

                await applicationRepository.AddAsync(application);
                await applicationRepository.SaveAsync();

                if (request.CreateDto.IncludeClient)
                {
                    var createDto = new CreateOrUpdateClientDto
                    {
                        ApplicationCode = application!.Code,
                        OldApplicationCode = null!,
                        ApplicationName = application!.Name,
                        ApplicationUri = application!.ApplicationUri,
                        ApplicationLogoUri = application!.LogoUri,
                        SigninRedirectUri = request.CreateDto!.SigninRedirectUri,
                        RefreshRedirectUri = request.CreateDto!.RefreshRedirectUri,
                        PostLogoutRedirectUri = request.CreateDto!.PostLogoutRedirectUri,
                        ClientSecret = request.CreateDto!.ClientSecret,
                        ClientSecretUpdate = true,
                        AccessTokenLifetime = request.CreateDto!.AccessTokenLifetime,
                    };

                    var createClientResponse = await _mediator?.Send(new CreateOrUpdateClientCommand(createDto), cancellationToken)!;
                    if (!createClientResponse.IsValid)
                    {
                        response.AttachResults(createClientResponse);
                        return response;
                    }

                    application.ClientId = createClientResponse.Data!.ClientId;

                    await applicationRepository.UpdateAsync(application);
                    await applicationRepository.SaveAsync();
                }
            }

            var applicationDto = _mapper?.Map<GetApplicationDto>(application);
            if (applicationDto != null) response.UpdateData(applicationDto);

            response.AddOkResult(Resources.Common.CreateSuccessMessage);

            return await Task.FromResult(response);
        }
    }
}

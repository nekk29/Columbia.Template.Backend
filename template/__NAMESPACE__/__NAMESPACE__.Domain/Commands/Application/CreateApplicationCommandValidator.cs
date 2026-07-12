using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.Application
{
    public class CreateApplicationCommandValidator : CommandValidatorBase<CreateApplicationCommand>
    {
        public CreateApplicationCommandValidator()
        {
            RequiredInformation(x => x.CreateDto)
                .DependentRules(() =>
                {
                    //RequiredString(x => x.CreateDto.Code, Resources.Application.Code, {Min}, {Max});
                    //RequiredString(x => x.CreateDto.Name, Resources.Application.Name, {Min}, {Max});
                });
        }
    }
}

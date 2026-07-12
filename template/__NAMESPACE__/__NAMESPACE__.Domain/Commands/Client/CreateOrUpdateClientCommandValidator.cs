using __NAMESPACE__.Domain.Commands.Base;

namespace __NAMESPACE__.Domain.Commands.Client
{
    public class CreateOrUpdateClientCommandValidator : CommandValidatorBase<CreateOrUpdateClientCommand>
    {
        public CreateOrUpdateClientCommandValidator()
        {

            RequiredField(x => x.CreateDto.ApplicationCode, Resources.Client.ApplicationCode);
            RequiredField(x => x.CreateDto.ApplicationUri, Resources.Client.ApplicationUri);
        }
    }
}

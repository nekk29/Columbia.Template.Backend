using __NAMESPACE__.Domain.Commands.Base;
using __NAMESPACE__.Dto.Client;

namespace __NAMESPACE__.Domain.Commands.Client
{
    public class CreateOrUpdateClientCommand(CreateOrUpdateClientDto createDto) : CommandBase<CreateOrUpdateClientResultDto>
    {
        public CreateOrUpdateClientDto CreateDto { get; set; } = createDto;
    }
}

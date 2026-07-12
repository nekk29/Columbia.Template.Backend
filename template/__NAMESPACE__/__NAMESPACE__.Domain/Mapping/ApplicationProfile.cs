using AutoMapper;
using __NAMESPACE__.Dto.Application;

namespace __NAMESPACE__.Domain.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Entity.Application, ApplicationDto>()
                .ReverseMap();

            CreateMap<Entity.Application, CreateApplicationDto>()
                .ReverseMap();

            CreateMap<Entity.Application, UpdateApplicationDto>()
                .ReverseMap();

            CreateMap<Entity.Application, GetApplicationDto>()
                .ReverseMap();

            CreateMap<Entity.Application, ListApplicationDto>()
                .ReverseMap();

            CreateMap<Entity.Application, SearchApplicationDto>()
                .ReverseMap();
        }
    }
}

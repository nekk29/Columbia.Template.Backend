using AutoMapper;
using __NAMESPACE__.Dto.Module;

namespace __NAMESPACE__.Domain.Mapping
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<Entity.Module, ModuleDto>()
                .ReverseMap();

            CreateMap<Entity.Module, CreateModuleDto>()
                .ReverseMap();

            CreateMap<Entity.Module, UpdateModuleDto>()
                .ReverseMap();

            CreateMap<Entity.Module, GetModuleDto>()
                .ForMember(m => m.ApplicationCode, opt => opt.MapFrom(m => m.Application != null ? m.Application.Code : null))
                .ForMember(m => m.ApplicationName, opt => opt.MapFrom(m => m.Application != null ? m.Application.Name : null))
                .ReverseMap();

            CreateMap<Entity.Module, ListModuleDto>()
                .ForMember(m => m.ApplicationCode, opt => opt.MapFrom(m => m.Application != null ? m.Application.Code : null))
                .ForMember(m => m.ApplicationName, opt => opt.MapFrom(m => m.Application != null ? m.Application.Name : null))
                .ReverseMap();

            CreateMap<Entity.Module, SearchModuleDto>()
                .ForMember(m => m.ApplicationCode, opt => opt.MapFrom(m => m.Application != null ? m.Application.Code : null))
                .ForMember(m => m.ApplicationName, opt => opt.MapFrom(m => m.Application != null ? m.Application.Name : null))
                .ReverseMap();
        }
    }
}

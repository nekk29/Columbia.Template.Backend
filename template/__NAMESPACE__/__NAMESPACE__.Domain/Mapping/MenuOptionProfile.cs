using AutoMapper;
using __NAMESPACE__.Dto.MenuOption;

namespace __NAMESPACE__.Domain.Mapping
{
    public class MenuOptionProfile : Profile
    {
        public MenuOptionProfile()
        {
            CreateMap<Entity.MenuOption, GetMenuOptionDto>()
                .ForMember(m => m.ApplicationCode, opt => opt.MapFrom(m => m.Application != null ? m.Application.Code : null))
                .ForMember(m => m.ApplicationName, opt => opt.MapFrom(m => m.Application != null ? m.Application.Name : null))
                .ForMember(m => m.ParentCode, opt => opt.MapFrom(m => m.ParentMenuOption != null ? m.ParentMenuOption.Code : null))
                .ForMember(m => m.ParentName, opt => opt.MapFrom(m => m.ParentMenuOption != null ? m.ParentMenuOption.Name : null))
                .ForMember(m => m.ActionCode, opt => opt.MapFrom(m => m.Action != null ? m.Action.Code : null))
                .ForMember(m => m.ActionName, opt => opt.MapFrom(m => m.Action != null ? m.Action.Name : null))
                .ForMember(m => m.ModuleId, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Id : default) : default))
                .ForMember(m => m.ModuleCode, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Code : null) : null))
                .ForMember(m => m.ModuleName, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Name : null) : null))
                .ReverseMap();

            CreateMap<Entity.MenuOption, ListMenuOptionDto>()
                .ForMember(m => m.ApplicationCode, opt => opt.MapFrom(m => m.Application != null ? m.Application.Code : null))
                .ForMember(m => m.ApplicationName, opt => opt.MapFrom(m => m.Application != null ? m.Application.Name : null))
                .ForMember(m => m.ParentCode, opt => opt.MapFrom(m => m.ParentMenuOption != null ? m.ParentMenuOption.Code : null))
                .ForMember(m => m.ParentName, opt => opt.MapFrom(m => m.ParentMenuOption != null ? m.ParentMenuOption.Name : null))
                .ForMember(m => m.ActionCode, opt => opt.MapFrom(m => m.Action != null ? m.Action.Code : null))
                .ForMember(m => m.ActionName, opt => opt.MapFrom(m => m.Action != null ? m.Action.Name : null))
                .ForMember(m => m.ModuleId, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Id : default) : default))
                .ForMember(m => m.ModuleCode, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Code : null) : null))
                .ForMember(m => m.ModuleName, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Name : null) : null))
                .ReverseMap();

            CreateMap<Entity.MenuOption, TreeMenuOptionDto>()
                .ForMember(m => m.ApplicationCode, opt => opt.MapFrom(m => m.Application != null ? m.Application.Code : null))
                .ForMember(m => m.ApplicationName, opt => opt.MapFrom(m => m.Application != null ? m.Application.Name : null))
                .ForMember(m => m.ParentCode, opt => opt.MapFrom(m => m.ParentMenuOption != null ? m.ParentMenuOption.Code : null))
                .ForMember(m => m.ParentName, opt => opt.MapFrom(m => m.ParentMenuOption != null ? m.ParentMenuOption.Name : null))
                .ForMember(m => m.ActionCode, opt => opt.MapFrom(m => m.Action != null ? m.Action.Code : null))
                .ForMember(m => m.ActionName, opt => opt.MapFrom(m => m.Action != null ? m.Action.Name : null))
                .ForMember(m => m.ModuleId, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Id : default) : default))
                .ForMember(m => m.ModuleCode, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Code : null) : null))
                .ForMember(m => m.ModuleName, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Name : null) : null))
                .ReverseMap();

            CreateMap<Entity.MenuOption, SearchMenuOptionDto>()
                .ForMember(m => m.ApplicationCode, opt => opt.MapFrom(m => m.Application != null ? m.Application.Code : null))
                .ForMember(m => m.ApplicationName, opt => opt.MapFrom(m => m.Application != null ? m.Application.Name : null))
                .ForMember(m => m.ParentCode, opt => opt.MapFrom(m => m.ParentMenuOption != null ? m.ParentMenuOption.Code : null))
                .ForMember(m => m.ParentName, opt => opt.MapFrom(m => m.ParentMenuOption != null ? m.ParentMenuOption.Name : null))
                .ForMember(m => m.ActionCode, opt => opt.MapFrom(m => m.Action != null ? m.Action.Code : null))
                .ForMember(m => m.ActionName, opt => opt.MapFrom(m => m.Action != null ? m.Action.Name : null))
                .ForMember(m => m.ModuleId, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Id : default) : default))
                .ForMember(m => m.ModuleCode, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Code : null) : null))
                .ForMember(m => m.ModuleName, opt => opt.MapFrom(m => m.Action != null ? (m.Action.Module != null ? m.Action.Module.Name : null) : null))
                .ReverseMap();

            CreateMap<Entity.MenuOption, CreateMenuOptionDto>().ReverseMap();
            CreateMap<Entity.MenuOption, UpdateMenuOptionDto>().ReverseMap();
        }
    }
}

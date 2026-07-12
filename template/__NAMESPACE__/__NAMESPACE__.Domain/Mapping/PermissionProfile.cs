using AutoMapper;
using __NAMESPACE__.Dto.Permission;

namespace __NAMESPACE__.Domain.Mapping
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Entity.Permission, ListPermissionDto>()
                .ForMember(m => m.ModuleCode, opt => opt.MapFrom(m => m.Action!.Module!.Code))
                .ForMember(m => m.ModuleName, opt => opt.MapFrom(m => m.Action!.Module!.Name))
                .ForMember(m => m.ParentActionId, opt => opt.MapFrom(m => m.Action!.ParentActionId))
                .ForMember(m => m.ActionCode, opt => opt.MapFrom(m => m.Action!.Code))
                .ForMember(m => m.ActionName, opt => opt.MapFrom(m => m.Action!.Name))
                .ReverseMap()
                .ForMember(m => m.RoleId, opt => opt.MapFrom(m => m.RoleId));
        }
    }
}

using AutoMapper;
using __NAMESPACE__.Dto.Role;

namespace __NAMESPACE__.Domain.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Entity.ApplicationRole, GetRoleDto>().ReverseMap();
            CreateMap<Entity.ApplicationRole, ListRoleDto>().ReverseMap();
            CreateMap<Entity.ApplicationRole, SearchRoleDto>().ReverseMap();
            CreateMap<Entity.ApplicationRole, CreateRoleDto>().ReverseMap();
            CreateMap<Entity.ApplicationRole, UpdateRoleDto>().ReverseMap();

            CreateMap<Entity.ApplicationRole, GetRoleDto>()
                .ReverseMap();

            CreateMap<Entity.ApplicationRole, ListRoleDto>()
                .ReverseMap();

            CreateMap<Entity.ApplicationRole, SearchRoleDto>()
                .ReverseMap();

            CreateMap<Entity.ApplicationRole, CreateRoleDto>().ReverseMap();
            CreateMap<Entity.ApplicationRole, UpdateRoleDto>().ReverseMap();
        }
    }
}

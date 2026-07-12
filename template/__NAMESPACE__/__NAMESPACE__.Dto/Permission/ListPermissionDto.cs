namespace __NAMESPACE__.Dto.Permission
{
    public class ListPermissionDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string ModuleCode { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public Guid? ParentActionId { get; set; }
        public Guid ActionId { get; set; }
        public string ActionCode { get; set; } = null!;
        public string ActionName { get; set; } = null!;
        public bool IsAssigned { get; set; }
    }
}

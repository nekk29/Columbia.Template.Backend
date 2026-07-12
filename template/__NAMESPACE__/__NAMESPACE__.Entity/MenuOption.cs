using __NAMESPACE__.Entity.Base;

namespace __NAMESPACE__.Entity
{
    public partial class MenuOption : SystemEntity
    {
        public MenuOption()
        {
            InverseParentMenuOption = new HashSet<MenuOption>();
        }

        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid? ParentMenuOptionId { get; set; }
        public Guid ActionId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? MenuUri { get; set; }
        public string? MenuIcon { get; set; }
        public int SortOrder { get; set; }

        public virtual Application Application { get; set; } = null!;
        public virtual MenuOption? ParentMenuOption { get; set; }
        public virtual Action Action { get; set; } = null!;
        public virtual ICollection<MenuOption> InverseParentMenuOption { get; set; }
    }
}

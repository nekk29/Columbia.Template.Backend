using System.ComponentModel.DataAnnotations.Schema;
using __NAMESPACE__.Entity.Base;

namespace __NAMESPACE__.Entity
{
    public partial class Action : SystemEntity
    {
        public Action()
        {
            InverseParentAction = new HashSet<Action>();
            MenuOptions = new HashSet<MenuOption>();
            Permissions = new HashSet<Permission>();
        }

        public Guid Id { get; set; }
        public Guid? ParentActionId { get; set; }
        public Guid ModuleId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }


        [ForeignKey("ParentActionId")]
        public virtual Action? ParentAction { get; set; }

        [ForeignKey("ModuleId")]
        public virtual Module Module { get; set; } = null!;
        public virtual ICollection<Action> InverseParentAction { get; set; }
        public virtual ICollection<MenuOption> MenuOptions { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}

using __NAMESPACE__.Entity.Base;

namespace __NAMESPACE__.Entity
{
    public partial class Permission : SystemEntity
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid ActionId { get; set; }

        public virtual Action Action { get; set; } = null!;
    }
}

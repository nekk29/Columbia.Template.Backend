using __NAMESPACE__.Entity.Base;

namespace __NAMESPACE__.Entity
{
    public class Application : SystemEntity
    {
        public Application()
        {
            Modules = new HashSet<Module>();
            MenuOptions = new HashSet<MenuOption>();
        }

        public Guid Id { get; set; }
        public string? ClientId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? LogoUri { get; set; } = null!;
        public string? ApplicationUri { get; set; } = null!;

        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<MenuOption> MenuOptions { get; set; }
    }
}

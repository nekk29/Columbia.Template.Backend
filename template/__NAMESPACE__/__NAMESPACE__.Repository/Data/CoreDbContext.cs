using __NAMESPACE__.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace __NAMESPACE__.Repository.Data
{
    public partial class CoreDbContext(DbContextOptions<CoreDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
    {
        public virtual DbSet<Entity.Action> Actions { get; set; } = null!;
        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<Email> Emails { get; set; } = null!;
        public virtual DbSet<MenuOption> MenuOptions { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity.Action>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.ModuleId, "IX_Actions_ModuleId");

                entity.HasIndex(e => e.ParentActionId, "IX_Actions_ParentActionId");

                entity.Property(e => e.Code).HasMaxLength(64);

                entity.Property(e => e.CreationUser).HasMaxLength(64);

                entity.Property(e => e.Description).HasMaxLength(1024);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.UpdateUser).HasMaxLength(64);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ParentAction)
                    .WithMany(p => p.InverseParentAction)
                    .HasForeignKey(d => d.ParentActionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.Code).HasMaxLength(32);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.LogoUri).HasMaxLength(2048);

                entity.Property(e => e.ApplicationUri).HasMaxLength(256);

                entity.Property(e => e.CreationUser).HasMaxLength(64);

                entity.Property(e => e.UpdateUser).HasMaxLength(64);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.Code, e.Language }, "AK_Emails_Code")
                    .IsUnique();

                entity.Property(e => e.CcEmails).HasMaxLength(1024);

                entity.Property(e => e.Code).HasMaxLength(32);

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.Language).HasMaxLength(6);

                entity.Property(e => e.Subject).HasMaxLength(256);

                entity.Property(e => e.ToEmails).HasMaxLength(1024);

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuOption>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.ApplicationId, "IX_MenuOptions_ApplicationId");

                entity.HasIndex(e => e.ActionId, "IX_MenuOptions_ActionId");

                entity.HasIndex(e => e.ParentMenuOptionId, "IX_MenuOptions_ParentMenuOptionId");

                entity.Property(e => e.Code).HasMaxLength(64);

                entity.Property(e => e.CreationUser).HasMaxLength(64);

                entity.Property(e => e.Description).HasMaxLength(1024);

                entity.Property(e => e.MenuIcon).IsUnicode(false);

                entity.Property(e => e.MenuUri).IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.UpdateUser).HasMaxLength(64);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.MenuOptions)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ParentMenuOption)
                    .WithMany(p => p.InverseParentMenuOption)
                    .HasForeignKey(d => d.ParentMenuOptionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.MenuOptions)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.ApplicationId, "IX_Modules_ApplicationId");

                entity.Property(e => e.Code).HasMaxLength(64);

                entity.Property(e => e.CreationUser).HasMaxLength(64);

                entity.Property(e => e.Description).HasMaxLength(1024);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.UpdateUser).HasMaxLength(64);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Modules)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.ActionId, "IX_Permissions_ActionId");

                entity.HasIndex(e => e.RoleId, "IX_Permissions_RoleId");

                entity.Property(e => e.CreationUser).HasMaxLength(64);

                entity.Property(e => e.UpdateUser).HasMaxLength(64);

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => new { e.Group, e.Code });

                entity.Property(e => e.Group).HasMaxLength(16);

                entity.Property(e => e.Code).HasMaxLength(64);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(1024);
            });
        }
    }
}

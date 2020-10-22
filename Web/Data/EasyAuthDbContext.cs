using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class EasyAuthDbContext : IdentityDbContext<User, Role, string>
    {
        public EasyAuthDbContext(DbContextOptions<EasyAuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(builder =>
            {
                builder.ToTable("Users");
                builder.HasMany(u => u.UserPermissions)
                    .WithOne()
                    .HasForeignKey("UserId")
                    .IsRequired();
            });
            builder.Entity<IdentityUserClaim<string>>(builder => builder.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(builder => builder.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<string>>(builder => builder.ToTable("UserTokens"));
            builder.Entity<Role>(builder =>
            {
                builder.ToTable("Roles");
                builder.HasMany(r => r.RolePermissions)
                    .WithOne()
                    .HasForeignKey("RoleId")
                    .IsRequired();
            });
            builder.Entity<IdentityRoleClaim<string>>(builder => builder.ToTable("RoleClaims"));
            builder.Entity<IdentityUserRole<string>>(builder => builder.ToTable("UserRoles"));

            builder.Entity<UserPermission>(builder =>
            {
                builder.ToTable("UserPermissions")
                    .HasKey(up => up.Id);
                builder.OwnsOne(up => up.Permission);
            });

            builder.Entity<RolePermission>(builder =>
            {
                builder.ToTable("RolePermissions")
                    .HasKey(rp => rp.Id);
                builder.OwnsOne(rp => rp.Permission);
            });
        }
    }
}

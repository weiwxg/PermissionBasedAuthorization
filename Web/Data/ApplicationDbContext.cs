using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RolePermission>(m =>
            {
                m.HasKey(p => p.Id);
                m.OwnsOne(p => p.Permission);
            });

            builder.Entity<AppRole>(m => m.HasMany(p => p.RolePermissions).WithOne().HasForeignKey("RoleId"));
        }
    }
}

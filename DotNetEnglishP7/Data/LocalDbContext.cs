using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DotNetEnglishP7.Identity;

namespace Dot.Net.WebApi.Data
{
    public class LocalDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public LocalDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed admin role
            builder.Entity<AppRole>().HasData(new AppRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                Id = 1,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            builder.Entity<AppRole>().HasData(new AppRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = 2,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });

            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            //create user
            var adminUser = new AppUser
            {
                Id = 1,
                FullName = "Super Admin",
                UserName = "SuperAdmin",
                NormalizedUserName = "SUPERADMIN",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var regularUser = new AppUser
            {
                Id = 2,
                FullName = "Standard User",
                UserName = "standardUser",
                NormalizedUserName = "STANDARDUSER",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            //set user password
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Password.123");
            regularUser.PasswordHash = ph.HashPassword(regularUser, "Password.123");

            //seed user
            builder.Entity<AppUser>().HasData(adminUser);
            builder.Entity<AppUser>().HasData(regularUser);

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>() { RoleId = 1, UserId = 1 });
            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>() { RoleId = 2, UserId = 2 });

        }
        public DbSet<BidList> BidLists { get; set;}
        public DbSet<CurvePoint> CurvePoints { get; set;}
        public DbSet<Rating> Ratings { get; set;}
        public DbSet<Rule> Rules { get; set;}
        public DbSet<Trade> Trades { get; set;}
    }
}
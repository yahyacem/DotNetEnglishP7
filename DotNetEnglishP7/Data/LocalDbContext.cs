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
            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

        }
        public DbSet<BidList> BidLists { get; set;}
        public DbSet<CurvePoint> CurvePoints { get; set;}
        public DbSet<Rating> Ratings { get; set;}
        public DbSet<Rule> Rules { get; set;}
        public DbSet<Trade> Trades { get; set;}
    }
}
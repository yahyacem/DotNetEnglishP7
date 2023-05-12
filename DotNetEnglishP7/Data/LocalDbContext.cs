using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set;}
        public DbSet<BidList> BidLists { get; set;}
        public DbSet<CurvePoint> CurvePoints { get; set;}
        public DbSet<Rating> Ratings { get; set;}
        public DbSet<Rule> Rules { get; set;}
        public DbSet<Trade> Trades { get; set;}
    }
}
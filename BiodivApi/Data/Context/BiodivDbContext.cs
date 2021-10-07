using Biodiv.Entities;
using BiodivApi.Entities;
using BiodivApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BiodivApi.Data.Context
{
    public class BiodivDbContext : DbContext
    {
        public BiodivDbContext(DbContextOptions<BiodivDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Specie>()
                .HasMany(s => s.LocalNames)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Specie>()
                .HasMany(s => s.LocalDistributions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Specie>()
                .HasMany(s => s.SpeciePhotos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Seed();
        }

        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<LocalName> LocalNames { get; set; }
        public DbSet<LocalDistribution> LocalDistributions { get; set; }
        public DbSet<SpeciePhoto> SpeciePhotos { get; set; }
    }
}
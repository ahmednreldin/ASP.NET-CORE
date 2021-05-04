using Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure
{
   public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PortfolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    Id = Guid.NewGuid(),
                    FullName = "Ahmed Nour-eldin",
                    Avatar = "avatar.jpg",
                    Profile =".NET Developer",
                }
                );
        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }


    }
}

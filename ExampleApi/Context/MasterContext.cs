using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Context
{
    public class MasterContext : DbContext
    {
        public MasterContext()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Activity> Activities { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=root;Uid=root;Pwd=root;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<Plan>().HasKey(e => e.Id);
            modelBuilder.Entity<Activity>().HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_App
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TaskManager> TaskManager { get; set; }
        public DbSet<Task> Task {  get; set; }
        public  DbSet<Notification> Notification { get; set; }

        // Optionally, you can override the OnModelCreating method to configure the model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your model here using modelBuilder.Entity<TEntity>()
            // For example:
            // modelBuilder.Entity<User>().HasKey(u => u.Id);
            // modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
        }

    }
}

 
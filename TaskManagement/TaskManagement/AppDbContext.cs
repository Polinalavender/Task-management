using Microsoft.EntityFrameworkCore;

namespace TaskManagement
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Task> Task { get; set; } // Use singular name
        public DbSet<TaskManager> TaskManagers { get; set; } // Use plural name
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AppDbContext;Trusted_Connection=True;",
                options => options.EnableRetryOnFailure(maxRetryCount: 10)); // Increase the maxRetryCount
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId); 
            modelBuilder.Entity<Team>().HasKey(t => t.TeamId); 
            modelBuilder.Entity<Task>().HasKey(t => t.TaskId);

            modelBuilder.Entity<TaskManager>().HasNoKey();
        }
    }
}

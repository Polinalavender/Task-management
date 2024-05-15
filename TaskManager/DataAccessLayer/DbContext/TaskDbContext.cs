using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager;
using TaskManager.Models;

namespace DataAccessLayer.DbContext
{
    public class TaskDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TaskModel> Tasks { get; set; }
        public virtual DbSet<UserTeam> UserTeam { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:taskmanagementdb.database.windows.net,1433;Initial Catalog=taskmanagementdb;Persist Security Info=False;User ID=taskmanagementdb;Password=UY33mUhxeVqhj8hUY33mUhxeVqhj8h;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTeam>().HasKey(ba => new { ba.UserId, ba.TeamId });
            modelBuilder.Entity<UserTeam>().HasOne(ba => ba.User).WithMany(b => b.UserTeam).HasForeignKey(ba => ba.UserId);
            modelBuilder.Entity<UserTeam>().HasOne(ba => ba.Team).WithMany(a => a.UserTeam).HasForeignKey(ba => ba.TeamId);
        }
    }
}

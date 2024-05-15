using DataAccessLayer.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager;
using TaskManager.Models;

namespace DataAccessLayer.Repositories
{
    public class TaskRepository
    {
        private readonly TaskDbContext _dbContext;

        public TaskRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<TaskModel> CreateTaskAsync(TaskModel task)
        {
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        // Read
        public async Task<TaskModel> GetTaskByIdAsync(int id)
        {
            return await _dbContext.Tasks.FindAsync(id);
        }

        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            return await _dbContext.Tasks.Include(a=> a.AssignedUser).ToListAsync();
        }

        public async Task<List<TaskModel>> GetTeamsTasksAsync(int?[] userids)
        {
            return await _dbContext.Tasks.Where(a => userids.Contains(a.AssignedUserID)).Include(a => a.AssignedUser).ToListAsync();
        }

        // Update
        public async Task<TaskModel> UpdateTaskAsync(TaskModel task)
        {
            _dbContext.Entry(task).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return task;
        }

        // Delete
        public async Task DeleteTaskAsync(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}

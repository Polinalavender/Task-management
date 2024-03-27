using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name 
        { 
            get => Name; 
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or empty.");
                }
                Name = value;
            } 
        }
        public string Deadline
        {
            get => Deadline;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Deadline cannot be null or empty.");
                }
                Deadline = value;
            }
        }
        public int AssignedUserID { get; set; }
        public string TaskDescription
        {
            get => TaskDescription;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be null or empty.");
                }
                TaskDescription = value;
            }
        }
        public int TeamID { get; set; }

        // Navigation properties
        public Team Team { get; set; }
        public User AssignedUser { get; set; }

        public Task(string name, string deadline)
        {
            Name = name;
            Deadline = deadline;
        }

        // Methods
        public void AssignTask(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            AssignedUserID = user.UserId;
            AssignedUser = user;

            // save to db
            using (var dbContext = new AppDbContext())
            {
                dbContext.Task.Update(this);
                dbContext.SaveChanges();
            }
        }
    }
}

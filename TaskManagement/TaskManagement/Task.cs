using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement
{
    public class Task
    {
        public int  TaskId { get; set; }
        public string Name { get; set; }
        public string Deadline { get; set; }
        public int AssignedUserID { get; set; }
        public string TaskDescription { get; set; }
        public int TeamID { get; set; }

        // Navigation properties
        public Team Team { get; set; }
        public User AssignedUser { get; set; }
    }
}

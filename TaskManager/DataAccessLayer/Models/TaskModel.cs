using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class TaskModel
    {
        [Key]
        public int  Id { get; set; }
        public string? Name { get; set; }
        public DateTime Deadline { get; set; }
        public int? AssignedUserID { get; set; }
        public string? TaskDescription { get; set; }
        public User? AssignedUser { get; set; }
        public TaskEnum TaskStatus { get; set; } = TaskEnum.ToDo;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_App
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Deadline { get; set; }
        public int assigneedTo { get; set; }
        public string details { get; set; }

        public Task() { }

    }
}

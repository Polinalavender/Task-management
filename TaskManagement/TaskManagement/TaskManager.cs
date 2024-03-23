using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement
{
    public class TaskManager
    {
        public Team teams { get; set; }
        public Notification notification { get; set; }

        [NotMapped]
        public Mutex resousrseMutex { get; set; }

    }
}

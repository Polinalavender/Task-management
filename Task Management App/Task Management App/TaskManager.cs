using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_App
{
    public class TaskManager
    {
        public Team teams { get; set; } 
        public Notification notification {  get; set; }
        public Mutex resousrseMutex {  get; set; }



    }
}

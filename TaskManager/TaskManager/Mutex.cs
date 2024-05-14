using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement
{


    public class Mutex
    {

        [NotMapped]
        public IntPtr Handle { get; set; }
    }

}
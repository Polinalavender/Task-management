using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models;

namespace DataAccessLayer.Models
{
    public class UserTeam
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }

}

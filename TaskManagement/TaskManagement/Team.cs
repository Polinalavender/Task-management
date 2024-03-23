using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public User memebers { get; set; }
        public int CreatorUserID { get; set; }
        // Navigation property for creator user
        public User CreatorUser { get; set; }

    }
}

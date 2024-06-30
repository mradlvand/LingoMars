using Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class UserProgress
    {
        public int Id { get; set; }
        public UserProgressType UserProgressType { get; set; }
        public int UserId { get; set; }
        public int Time { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
    }
}

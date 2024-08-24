using Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MacAddress { get; set; }
        public DateTime UpdateTime { get; set; }
        public UserRole Role { get; set; }
        public Category UserCategory { get; set; }
        public bool Status { get; set; }
    }
}

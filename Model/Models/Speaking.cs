using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Speaking
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Context { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string? Video { get; set; }
        public bool Status { get; set; }

        public Lesson Lesson { get; set; }
        [ForeignKey("LessonId")]
        public int LessonId { get; set; }
    }
}

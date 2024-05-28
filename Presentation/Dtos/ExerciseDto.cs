using Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public ExerciseType Type { get; set; }
        public ContentType ContentType { get; set; }
        public int LessonId { get; set; }
        public string ModelContent { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool Status { get; set; }
    }
}

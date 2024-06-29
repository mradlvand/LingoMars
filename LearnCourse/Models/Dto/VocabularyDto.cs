using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCourse.Models.Dto
{
    public class VocabularyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LessonId { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public int CreationDateTime { get; set; }
        public int UpdateDateTime { get; set; }
        public string Icon { get; set; }
        public string Picture { get; set; }
        public bool Status { get; set; }

    }
}

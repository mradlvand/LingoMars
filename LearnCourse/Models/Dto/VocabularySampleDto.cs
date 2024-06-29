using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCourse.Models.Dto
{
    public class VocabularySampleDto
    {
        public int Id { get; set; }
        public string Sentence { get; set; }
        public string PersianTranslation { get; set; }
        public int VocabularyId { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public int CreationDateTime { get; set; }
        public int UpdateDateTime { get; set; }
        public bool Status { get; set; }
    }
}

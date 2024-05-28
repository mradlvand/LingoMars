using Model.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public Status StatusId { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string? Video { get; set; }
        public bool Status { get; set; }

        public Level Level { get; set; }
        [ForeignKey("LevelId")]
        public int LevelId { get; set; }

        public List<Vocabulary> Vocabularies { get; set; }
        public List<Speaking> Speakings { get; set; }
        public List<Grammer> Grammers { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}

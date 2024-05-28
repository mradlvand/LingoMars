using Model.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models
{
    public class VocabularyDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Context { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string? Video { get; set; }
        public bool Status { get; set; }
    }
}

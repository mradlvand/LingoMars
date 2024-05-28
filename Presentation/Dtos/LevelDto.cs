﻿using Model.General;

namespace Presentation.Dtos
{
    public class LevelDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TeacherId { get; set; }
        public int Order { get; set; }
        public Category CategoryId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string? Icon { get; set; }
        public string? Picture { get; set; }
        public bool Status { get; set; }
    }

    public class GetLevelDtoRequest
    {
        public int TeacherId { get; set;}
    }
}

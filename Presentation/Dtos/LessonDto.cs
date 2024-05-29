using Model.General;

namespace Presentation.Dtos
{
    public class LessonDto
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
        public int LevelId { get; set; }
    }

    public class GetLessonsDtoRequest
    {
        public int LevelId { get; set; }
    }

    public class GetLessonDtoRequest
    {
        public int LessonId { get; set; }
    }
}

namespace Presentation.Dtos
{
    public class SpeakingDto
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

    public class GetSpeakingsDtoRequest
    {
        public int LessonId { get; set; }
    }

    public class GetSpeakingDtoRequest
    {
        public int SpeakingId { get; set; }
    }
}

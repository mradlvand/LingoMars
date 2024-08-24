using Model.General;

namespace Presentation.Dtos
{

    public class GetUserProgressDtoRequest
    {
        public int UserId { get; set;}
    }

    public class GetUserProgressDtoResponce
    {
        public List<UserProgressDto> UserProgress { get; set; }
    }

    public class UserProgressDto
    {
        public int Id { get; set; }
        public UserProgressType UserProgressType { get; set; }
        public int UserId { get; set; }
        public int Time { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
    }
}

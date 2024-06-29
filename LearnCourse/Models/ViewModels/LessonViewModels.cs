using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.General;
using Model.Models;

namespace LearnCourse.Models.ViewModels
{
    public class LessonViewModels
    {
        public Lesson Lesson { get; set; }
        public string? Icon { get; set; }
        public string? Picture { get; set; }
        public UserRole UserRole { get; set; }
        public SelectList Levels { get; set; }
        public int OrderLesson { get; set; }
    }

    public class LessonViewModelsIndex
    {
        public Lesson Lesson { get; set; }
        public UserRole UserRole { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
    }
}

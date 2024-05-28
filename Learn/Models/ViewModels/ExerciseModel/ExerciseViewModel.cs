using Microsoft.AspNetCore.Mvc.Rendering;
using Model.General;
using Model.Models;

namespace Learn.Models.ViewModels.ExerciseModel
{
    public class ExerciseViewModel
    {
        public Exercise Exercise { get; set; }
        public SelectList Lessons { get; set; }
        public List<ContentType> ContentTypes { get; set; }
        public UserRole UserRole { get; set; }
        public string BaseUrl { get; set; }
    }
}

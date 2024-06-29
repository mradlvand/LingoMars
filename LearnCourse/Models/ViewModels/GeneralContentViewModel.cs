using Microsoft.AspNetCore.Mvc.Rendering;
using Model.General;
using Model.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnCourse.Models.ViewModels
{
    public class GeneralContentViewModel
    {
        public GeneralContent GeneralContent { get; set; }
        public string? Video { get; set; }
        public string? Picture { get; set; }
        public UserRole UserRole { get; set; }
        public SelectList Lessons { get; set; }
        public List<GeneralContentSample> Samples { get; set; }
        public PageModel PageModel { get; set; }
        public string BaseUrl {  get; set; }
    }

    public class PageModel
    {
        public string Title { get; set; }
    }

    public class GeneralContentViewModelIndex
    {
        public GeneralContent GeneralContent { get; set; }
        public UserRole UserRole { get; set; }
       public IEnumerable<GeneralContent> GeneralContents { get; set; }
        public PageModel PageModel { get; set; }
    }

    public class GeneralContentSample
    {
        public string Sentence { get; set; }
        public string PersianTranslation { get; set; }
        public string Url { get; set; }

    }

    public class GeneralContent
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Context { get; set; }
        public int Order { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string? Video { get; set; }
        public bool Status { get; set; }

        public Lesson Lesson { get; set; }
        [ForeignKey("LessonId")]
        public int LessonId { get; set; }
    }
}

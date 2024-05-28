
namespace Learn.Models.ViewModels.ExerciseModel
{
    public class MultipleExercise
    {
        public string Url { get; set; }
        public List<Option> Options { get; set; }
    }

    public class Option
    {
        public string Sentence { get; set; }
        public bool Answer { get; set;}
    }
}

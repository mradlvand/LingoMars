using Model.General;
using Model.Models;

namespace Learn.Models.ViewModels
{
    public class CreateLevelViewModels
    {
        public Level Level { get; set; }
        public List<Category> Categories { get; set; }
        public string? Icon { get; set; }
        public string? Picture { get; set; }
        public UserRole UserRole { get; set; }
    }

    public class CreateLevelViewModelsIndex
    {
        public IEnumerable<Level> Levels { get; set; }
        public UserRole UserRole { get; set; }

        public Level Level { get; set; }

        public string UserName { get; set; }
    }
}

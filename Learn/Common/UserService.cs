using Data.Context;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Learn.Common
{
    public interface IUserService
    {
        List<Level> GetUserLevel(int userId);
        List<Lesson> GetUserLesson(int userId);
        List<Vocabulary> GetUserVocab(int userId);
        List<Grammer> GetUserGrammer(int userId);
        List<Speaking> GetUserSpeak(int userId);
        //List<Conversation> GetUserConversation(int userId);
        //List<GeneralContent> GetUserGeneralContent(int userId);
    }

    public class UserService : IUserService
    {
        private readonly DBLearnContext _context;

        public UserService(DBLearnContext context)
        {
            _context = context;
        }

        public List<Level> GetUserLevel(int userId)
        {
            return _context.Levels.Where(x=>x.TeacherId == userId).ToList();
        }

        public List<Lesson> GetUserLesson(int userId)
        {
            var levels = GetUserLevel(userId).Select(x=>x.Id).ToList();

            return _context.Lessons.Where(x => levels.Contains(x.LevelId)).Include(l => l.Level).ToList();
        }

        public List<Vocabulary> GetUserVocab(int userId)
        {
            var lessons = GetUserLesson(userId).Select(x => x.Id).ToList();

            return _context.Vocabularies.Where(x => lessons.Contains(x.LessonId)).Include(l => l.Lesson).ToList();
        }

        public List<Grammer> GetUserGrammer(int userId)
        {
            var lessons = GetUserLesson(userId).Select(x => x.Id).ToList();

            return _context.Grammers.Where(x => lessons.Contains(x.LessonId)).Include(l => l.Lesson).ToList();
        }

        public List<Speaking> GetUserSpeak(int userId)
        {
            var lessons = GetUserLesson(userId).Select(x => x.Id).ToList();

            return _context.Speakings.Where(x => lessons.Contains(x.LessonId)).Include(l => l.Lesson).ToList();
        }

        //public List<GeneralContent> GetUserGeneralContent(int userId)
        //{
        //    var lessons = GetUserLesson(userId).Select(x => x.Id).ToList();

        //    return _context.GeneralContents.Where(x => lessons.Contains(x.LessonId)).Include(l => l.Lesson).ToList();
        //}
    }
}

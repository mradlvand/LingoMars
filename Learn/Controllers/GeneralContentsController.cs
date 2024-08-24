using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Model.Models;
using Learn.Models.ViewModels;
using System.Drawing;
using Learn.Common;
using Newtonsoft.Json;
using Humanizer.Inflections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Learn.Controllers
{
    [Authorize]
    public class GeneralContentsController : BaseController
    {
        private readonly DBLearnContext _context;
        private IUserService _userService { get; }

        private readonly IConfiguration _configuration;

        public GeneralContentsController(DBLearnContext context, IUserService userService,IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _configuration = configuration;
        }

        // GET: GeneralContents
        public async Task<IActionResult> IndexVocab()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Model.Models.Vocabulary, GeneralContent>());
            var mapper = config.CreateMapper();

            var model = new GeneralContentViewModelIndex()
            {
                UserRole = GetUserRole(),
                PageModel = new Models.ViewModels.PageModel() { Title = "vocab" }
            };

            model.GeneralContents = mapper.Map<IEnumerable<GeneralContent>>(_userService.GetUserVocab(GetUserId()));

            return View("Index", model);
        }

        public async Task<IActionResult> IndexGrammer()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Model.Models.Grammer, GeneralContent>());
            var mapper = config.CreateMapper();

            var model = new GeneralContentViewModelIndex()
            {
                UserRole = GetUserRole(),
                PageModel = new Models.ViewModels.PageModel() { Title = "Grammer" }
            };

            model.GeneralContents = mapper.Map<IEnumerable<GeneralContent>>(_userService.GetUserGrammer(GetUserId()));
            return View("Index", model);
        }

        public async Task<IActionResult> IndexSpeak()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Model.Models.Speaking, GeneralContent>());
            var mapper = config.CreateMapper();

            var model = new GeneralContentViewModelIndex()
            {
                UserRole = GetUserRole(),
                PageModel = new Models.ViewModels.PageModel() { Title = "speak" }
            };

            model.GeneralContents = mapper.Map<IEnumerable<GeneralContent>>(_userService.GetUserSpeak(GetUserId()));
            return View("Index", model);
        }

        public async Task<IActionResult> Details(int? id, string pageName)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Model.Models.Vocabulary, GeneralContent>());
            var mapper = config.CreateMapper();
            var model = new GeneralContent();

            if (pageName == "vocab")
            {
                var generalContent = await _context.Vocabularies
                 .Include(g => g.Lesson)
                 .FirstOrDefaultAsync(m => m.Id == id);

                model = mapper.Map<GeneralContent>(generalContent);
            }
            else if (pageName == "grammer")
            {
                var generalContent = await _context.Grammers
                .Include(g => g.Lesson)
                .FirstOrDefaultAsync(m => m.Id == id);

                model = mapper.Map<GeneralContent>(generalContent);
            }
            else if (pageName == "speak")
            {
                var generalContent = await _context.Speakings
                .Include(g => g.Lesson)
                .FirstOrDefaultAsync(m => m.Id == id);

                model = mapper.Map<GeneralContent>(generalContent);
            }

            return View(model);
        }

        // GET: GeneralContents/Create
        public IActionResult Create(string pageName)
        {
            var baseUrl = _configuration["Statics:BaseUrl"];

            var userLesson = _userService.GetUserLesson(GetUserId());

            var model = new GeneralContentViewModel()
            {
                Lessons = new SelectList(userLesson, "Id", "Title"),
                UserRole = GetUserRole(),
                PageModel = new Models.ViewModels.PageModel() { Title = pageName },
                BaseUrl = baseUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GeneralContent model, string pageName)
        {
            if (pageName.ToLower() == "vocab")
            {
                var vocabModel = new Model.Models.Vocabulary()
                {
                    Header = model.Header,
                    Context = model.Context,
                    Description = model.Description,
                    UpdateDateTime = DateTime.Now,
                    Video = model.Video,
                    Status = true,
                    LessonId = model.LessonId,
                };
                _context.Vocabularies.Add(vocabModel);
            }
            else if (pageName.ToLower() == "speak")
            {
                var vocabModel = new Model.Models.Speaking()
                {
                    Header = model.Header,
                    Context = model.Context,
                    Description = model.Description,
                    UpdateDateTime = DateTime.Now,
                    Video = model.Video,
                    Status = true,
                    LessonId = model.LessonId,
                };
                _context.Speakings.Add(vocabModel);
            }
            else if (pageName.ToLower() == "grammer")
            {
                var vocabModel = new Model.Models.Grammer()
                {
                    Header = model.Header,
                    Context = model.Context,
                    Description = model.Description,
                    UpdateDateTime = DateTime.Now,
                    Video = model.Video,
                    Status = true,
                    LessonId = model.LessonId,
                };
                _context.Grammers.Add(vocabModel);
            }

            await _context.SaveChangesAsync();
            return Json(new { status = "success" });
        }

        public async Task<IActionResult> Edit(int id, string pageName)
        {
            var baseUrl = _configuration["Statics:BaseUrl"];
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Model.Models.Vocabulary, GeneralContent>());
            var mapper = config.CreateMapper();

            var userLesson = _userService.GetUserLesson(GetUserId());

            var model = new GeneralContentViewModel()
            {
                Lessons = new SelectList(userLesson, "Id", "Title"),
                UserRole = GetUserRole(),
                PageModel = new Models.ViewModels.PageModel() { Title = pageName },
                BaseUrl = baseUrl,
            };

            if (pageName == "vocab")
            {
                var res = await _context.Vocabularies.FindAsync(id);

                model.GeneralContent = mapper.Map<GeneralContent>(res);
            }
            else if (pageName == "grammer")
            {
                var res = await _context.Grammers.FindAsync(id);

                model.GeneralContent = mapper.Map<GeneralContent>(res);
            }
            else if (pageName == "speak")
            {
                var res = await _context.Speakings.FindAsync(id);

                model.GeneralContent = mapper.Map<GeneralContent>(res);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GeneralContent model, string pageName)
        {
            if (pageName == "vocab")
            {
                var vocabModel = new Model.Models.Vocabulary()
                {
                    Id = model.Id,
                    Header = model.Header,
                    Context = model.Context,
                    Description = model.Description,
                    UpdateDateTime = DateTime.Now,
                    Video = model.Video,
                    Status = true,
                    LessonId = model.LessonId,
                };
                _context.Vocabularies.Update(vocabModel);
            }
            else if (pageName == "speak")
            {
                var vocabModel = new Model.Models.Speaking()
                {
                    Id = model.Id,
                    Header = model.Header,
                    Context = model.Context,
                    Description = model.Description,
                    UpdateDateTime = DateTime.Now,
                    Video = model.Video,
                    Status = true,
                    LessonId = model.LessonId,
                };
                _context.Speakings.Update(vocabModel);
            }
            else if (pageName == "grammer")
            {
                var vocabModel = new Model.Models.Grammer()
                {
                    Id = model.Id,
                    Header = model.Header,
                    Context = model.Context,
                    Description = model.Description,
                    UpdateDateTime = DateTime.Now,
                    Video = model.Video,
                    Status = true,
                    LessonId = model.LessonId,
                };
                _context.Grammers.Update(vocabModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { pageName = pageName });
        }

        public async Task<IActionResult> Delete(int? id, string pageName)
        {
            if (pageName.ToLower() == "vocab")
            {
                var generalContent = await _context.Vocabularies
                 .Include(g => g.Lesson)
                 .FirstOrDefaultAsync(m => m.Id == id);

                if (generalContent == null)
                    return NotFound();

                return View("DeleteVocab", generalContent);
            }
            else if (pageName.ToLower() == "grammer")
            {
                var generalContent = await _context.Grammers
                .Include(g => g.Lesson)
                .FirstOrDefaultAsync(m => m.Id == id);

                if (generalContent == null)
                    return NotFound();

                return View("DeleteGrammer", generalContent);
            }
            else
            {
                var generalContent = await _context.Speakings
                .Include(g => g.Lesson)
                .FirstOrDefaultAsync(m => m.Id == id);

                if (generalContent == null)
                    return NotFound();

                return View("DeleteSpeak", generalContent);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedVocab(int id)
        {
            var generalContent = await _context.Vocabularies.FindAsync(id);

            if (generalContent != null)
                _context.Vocabularies.Remove(generalContent);

            await _context.SaveChangesAsync();
            return RedirectToAction("IndexVocab");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedGrammer(int id)
        {
            var generalContent = await _context.Grammers.FindAsync(id);

            if (generalContent != null)
                _context.Grammers.Remove(generalContent);

            await _context.SaveChangesAsync();
            return RedirectToAction("IndexGrammer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSpeak(int id)
        {
            var generalContent = await _context.Speakings.FindAsync(id);

            if (generalContent != null)
                _context.Speakings.Remove(generalContent);

            await _context.SaveChangesAsync();
            return RedirectToAction("IndexSpeak");
        }

    }
}

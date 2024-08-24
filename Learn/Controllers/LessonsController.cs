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
using Model.General;
using Learn.Common;
using Microsoft.AspNetCore.Authorization;

namespace Learn.Controllers
{
    [Authorize]
    public class LessonsController : BaseController
    {
        private readonly DBLearnContext _context;
        private IUserService _userService { get; }

        public LessonsController(DBLearnContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var model = new LessonViewModelsIndex()
            {
                UserRole = GetUserRole(),
                Lessons = _userService.GetUserLesson(GetUserId())
            };
            return View(model);
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            var userLevel = _userService.GetUserLevel(GetUserId());
            var orderLesson = _userService.GetUserLesson(GetUserId()).Count();
            var model = new LessonViewModels()
            {
                UserRole = GetUserRole(),
                Levels = new SelectList(userLevel, "Id", "Title"),
                OrderLesson = ++orderLesson
            };

            return View(model);
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LessonViewModels model, IFormFile Picture, IFormFile Icon)
        {
            if (Picture != null)
                model.Lesson.Video = Common.Common.UploadFile(Picture);

            //if (Icon != null)
            //    model.Lesson.Icon = Common.Common.UploadFile(Icon);

            model.Lesson.Status = true;
            model.Lesson.UpdateDateTime = DateTime.Now;
            model.Lesson.StatusId = Status.Pendding;

            _context.Add(model.Lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userLevel = _userService.GetUserLevel(GetUserId());
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            var model = new LessonViewModels()
            {
                Lesson = lesson,
                Levels = new SelectList(userLevel, "Id", "Title")
            };

            return View(model);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LessonViewModels model, IFormFile Picture, IFormFile Icon)
        {
            if (Picture != null)
                model.Lesson.Video = Common.Common.UploadFile(Picture);

            //if (Icon != null)
            //    model.Lesson.Icon = Common.Common.UploadFile(Icon);

            model.Lesson.Status = true;
            model.Lesson.UpdateDateTime = DateTime.Now;

            _context.Lessons.Update(model.Lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lessons == null)
            {
                return Problem("Entity set 'DBLearnContext.Lessons'  is null.");
            }
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
          return (_context.Lessons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Model.Models;
using Model.General;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Learn.Models.ViewModels.ExerciseModel;
using Learn.Common;
using Learn.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Learn.Controllers
{
    [Authorize]
    public class ExercisesController : BaseController
    {
        private readonly DBLearnContext _context;
        private IUserService _userService { get; }
        private readonly IConfiguration _configuration;

        public ExercisesController(DBLearnContext context, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _configuration = configuration;
        }
        // GET: Exercises
        public async Task<IActionResult> Index(int lessonId)
        {
            if (lessonId == 0)
                return View(await _context.Exercise.ToListAsync());
            else
                return View(await _context.Exercise.Where(x => x.LessonId == lessonId).ToListAsync());
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create(string pageName)
        {
            var baseUrl = _configuration["Statics:BaseUrl"];
            var userLesson = _userService.GetUserLesson(GetUserId());

            var model = new ExerciseViewModel()
            {
                Lessons = new SelectList(userLesson, "Id", "Title"),
                UserRole = GetUserRole(),
                BaseUrl = baseUrl,
            };

            model.ContentTypes = Enum.GetValues(typeof(ContentType)).Cast<ContentType>().ToList();

            return View(pageName, model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Exercise model)
        {
            model.Status = true;
            model.CreationDateTime = DateTime.Now;
            model.UpdateDateTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,ContentType,LessonId,ModelContent,CreationDateTime,UpdateDateTime,Status")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exercise == null)
            {
                return Problem("Entity set 'DBLearnContext.Exercise'  is null.");
            }
            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercise.Remove(exercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return (_context.Exercise?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

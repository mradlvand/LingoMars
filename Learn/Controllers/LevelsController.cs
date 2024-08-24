using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Model.Models;
using Learn.Models.Dto;
using Model.General;
using Learn.Models.ViewModels;
using System.Security.Claims;
using Learn.Common;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;

namespace Learn.Controllers
{
    [Authorize]
    public class LevelsController : BaseController
    {
        private readonly DBLearnContext _context;

        public LevelsController(DBLearnContext context)
        {
            _context = context;
        }

        // GET: Levels
        public async Task<IActionResult> Index()
        {
            var model = new CreateLevelViewModelsIndex()
            {
                UserRole = GetUserRole(),
                UserName = GetUserName()
            };

            if (GetUserRole() == UserRole.Teacher)
                model.Levels = await _context.Levels.Where(x => x.CategoryId == GetUserCategory()).ToListAsync();
            else
                model.Levels = await _context.Levels.ToListAsync();

            return View(model);
        }

        // GET: Levels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }

            var level = await _context.Levels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (level == null)
            {
                return NotFound();
            }

            return View(level);
        }

        // GET: Levels/Create
        public IActionResult Create()
        {
            var model = new CreateLevelViewModels() { UserRole = GetUserRole() };

            if (GetUserRole() == UserRole.Teacher)
            {
                List<Category> cats = new List<Category> { GetUserCategory() };

                model.Categories = cats;

                return View(model);
            }

            model.Categories = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

            return View(model);
        }

        // POST: Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLevelViewModels model, IFormFile Picture, IFormFile Icon)
        {
            if (true)
            {
                if (Picture != null)
                    model.Level.Picture = Common.Common.UploadFile(Picture);

                if (Icon != null)
                    model.Level.Icon = Common.Common.UploadFile(Icon);

                model.Level.Status = true;
                model.Level.UpdateDateTime = DateTime.Now;

                if (GetUserRole() == UserRole.Teacher)
                    model.Level.TeacherId = GetUserId();

                _context.Add(model.Level);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model.Level);
        }

        // GET: Levels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;


            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }

            var level = await _context.Levels.FindAsync(id);
            if (level == null)
            {
                return NotFound();
            }

            var model = new CreateLevelViewModels() { UserRole = GetUserRole(), Level = level };

            if (GetUserRole() == UserRole.Teacher)
            {
                List<Category> cats = new List<Category> { GetUserCategory() };

                model.Categories = cats;

                return View(model);
            }

            model.Categories = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

            return View(model);
        }

        // POST: Levels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateLevelViewModels model, IFormFile Picture, IFormFile Icon)
        {
            if (true)
            {
                try
                {
                    if (Picture != null)
                        model.Level.Picture = Common.Common.UploadFile(Picture);

                    if (Icon != null)
                        model.Level.Icon = Common.Common.UploadFile(Icon);

                    model.Level.Status = true;
                    model.Level.UpdateDateTime = DateTime.Now;

                    _context.Levels.Update(model.Level);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Levels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }

            var level = await _context.Levels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (level == null)
            {
                return NotFound();
            }

            return View(level);
        }

        // POST: Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Levels == null)
            {
                return Problem("Entity set 'DBLearnContext.Levels'  is null.");
            }
            var level = await _context.Levels.FindAsync(id);
            if (level != null)
            {
                _context.Levels.Remove(level);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LevelExists(int id)
        {
            return (_context.Levels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

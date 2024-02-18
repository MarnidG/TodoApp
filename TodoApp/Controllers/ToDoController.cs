using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ToDoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ToDo
        public async Task<IActionResult> Index(int CategoryId = 1,int StatusId = 1)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var query = _context.ToDos
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Where(t => t.userId == user.Id);

            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", CategoryId);
            ViewData["Statuses"] = new SelectList(_context.Statuses, "Id", "Name",StatusId);
            // ViewBag.Categories = _context.Categories.ToList();
            // ViewBag.Statuses = _context.Statuses.ToList();
            
            if (CategoryId != 1)
            {
                query = query.Where(t => t.CategoryId == CategoryId);
            }

            if (StatusId != 1)
            {
                query = query.Where(t => t.StatusId == StatusId);
            }
            
            return View(await query.ToListAsync());
        }

        // GET: ToDo/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.Id != 1), "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Statuses.Where(s => s.Id != 1), "Id", "Name");
            return View();
        }

        // POST: ToDo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Opis,DueDate,CategoryId,StatusId,userId")] ToDo toDo)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            toDo.userId = user.Id;
            
            if (ModelState.IsValid)
            {
                _context.Add(toDo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.Id != 1), "Id", "Name", toDo.CategoryId);
            ViewData["StatusId"] = new SelectList(_context.Statuses.Where(s => s.Id != 1), "Id", "Name", toDo.StatusId);
            return View(toDo);
        }

        // GET: ToDo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDos
                .Where(t => t.Id == id)
                .Where(t => t.userId == user.Id)
                .FirstOrDefaultAsync();
            if (toDo == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.Id != 1), "Id", "Name", toDo.CategoryId);
            ViewData["StatusId"] = new SelectList(_context.Statuses.Where(s => s.Id != 1), "Id", "Name", toDo.StatusId);
            return View(toDo);
        }

        // POST: ToDo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Opis,DueDate,CategoryId,StatusId")] ToDo toDo)
        {
            IdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            if (id != toDo.Id)
            {
                return NotFound();
            }
            toDo.userId = user.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.Id != 1), "Id", "Name", toDo.CategoryId);
            ViewData["StatusId"] = new SelectList(_context.Statuses.Where(s => s.Id != 1), "Id", "Name", toDo.StatusId);
            return View(toDo);
        }

        // GET: ToDo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDos
                .Include(t => t.Category)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo != null)
            {
                _context.ToDos.Remove(toDo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
    }
}

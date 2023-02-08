using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowCSFinal.Data;
using FlowCSFinal.Models;

namespace FlowCSFinal.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly FlowCSDbContext _context;

        public TemplatesController(FlowCSDbContext context)
        {
            _context = context;
        }

        // GET: Templates
        public async Task<IActionResult> Index()
        {
              return View(await _context.Templates.ToListAsync());
        }

        // GET: Templates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Templates == null)
            {
                return NotFound();
            }

            var templates = await _context.Templates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (templates == null)
            {
                return NotFound();
            }

            return View(templates);
        }

        // GET: Templates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Template,Background,Kategoria")] Templates templates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(templates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(templates);
        }

        // GET: Templates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Templates == null)
            {
                return NotFound();
            }

            var templates = await _context.Templates.FindAsync(id);
            if (templates == null)
            {
                return NotFound();
            }
            return View(templates);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Template,Background,Kategoria")] Templates templates)
        {
            if (id != templates.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(templates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplatesExists(templates.Id))
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
            return View(templates);
        }

        // GET: Templates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Templates == null)
            {
                return NotFound();
            }

            var templates = await _context.Templates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (templates == null)
            {
                return NotFound();
            }

            return View(templates);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Templates == null)
            {
                return Problem("Entity set 'FlowCSDbContext.Templates'  is null.");
            }
            var templates = await _context.Templates.FindAsync(id);
            if (templates != null)
            {
                _context.Templates.Remove(templates);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplatesExists(int id)
        {
          return _context.Templates.Any(e => e.Id == id);
        }
    }
}

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
    public class RedirectController : Controller
    {
        private readonly FlowCSDbContext _context;

        public RedirectController(FlowCSDbContext context)
        {
            _context = context;
        }

        // GET: Redirect
        public async Task<IActionResult> Index()
        {
              return View(await _context.Redirect.ToListAsync());
        }

        // GET: Redirect/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Redirect == null)
            {
                return NotFound();
            }

            var redirect = await _context.Redirect
                .FirstOrDefaultAsync(m => m.Id == id);
            if (redirect == null)
            {
                return NotFound();
            }

            return View(redirect);
        }

        // GET: Redirect/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Redirect/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Ikona,Description")] Redirect redirect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(redirect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(redirect);
        }

        // GET: Redirect/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Redirect == null)
            {
                return NotFound();
            }

            var redirect = await _context.Redirect.FindAsync(id);
            if (redirect == null)
            {
                return NotFound();
            }
            return View(redirect);
        }

        // POST: Redirect/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ikona,Description")] Redirect redirect)
        {
            if (id != redirect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(redirect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RedirectExists(redirect.Id))
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
            return View(redirect);
        }

        // GET: Redirect/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Redirect == null)
            {
                return NotFound();
            }

            var redirect = await _context.Redirect
                .FirstOrDefaultAsync(m => m.Id == id);
            if (redirect == null)
            {
                return NotFound();
            }

            return View(redirect);
        }

        // POST: Redirect/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Redirect == null)
            {
                return Problem("Entity set 'FlowCSDbContext.Redirect'  is null.");
            }
            var redirect = await _context.Redirect.FindAsync(id);
            if (redirect != null)
            {
                _context.Redirect.Remove(redirect);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RedirectExists(int id)
        {
          return _context.Redirect.Any(e => e.Id == id);
        }
    }
}

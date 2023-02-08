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
    public class CollectionsController : Controller
    {
        private readonly FlowCSDbContext _context;

        public CollectionsController(FlowCSDbContext context)
        {
            _context = context;
        }

        // GET: Collections
        public async Task<IActionResult> Index()
        {
              return View(await _context.Collections.ToListAsync());
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Collections == null)
            {
                return NotFound();
            }

            var collections = await _context.Collections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collections == null)
            {
                return NotFound();
            }

            return View(collections);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CollectionName")] Collections collections)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collections);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collections);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collections == null)
            {
                return NotFound();
            }

            var collections = await _context.Collections.FindAsync(id);
            if (collections == null)
            {
                return NotFound();
            }
            return View(collections);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CollectionName")] Collections collections)
        {
            if (id != collections.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collections);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionsExists(collections.Id))
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
            return View(collections);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Collections == null)
            {
                return NotFound();
            }

            var collections = await _context.Collections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collections == null)
            {
                return NotFound();
            }

            return View(collections);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Collections == null)
            {
                return Problem("Entity set 'FlowCSDbContext.Collections'  is null.");
            }
            var collections = await _context.Collections.FindAsync(id);
            if (collections != null)
            {
                _context.Collections.Remove(collections);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionsExists(int id)
        {
          return _context.Collections.Any(e => e.Id == id);
        }
    }
}

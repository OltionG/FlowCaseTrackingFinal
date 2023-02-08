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
    public class WorkspaceController : Controller
    {
        private readonly FlowCSDbContext _context;

        public WorkspaceController(FlowCSDbContext context)
        {
            _context = context;
        }

        // GET: Workspace
        public async Task<IActionResult> Index()
        {
              return View(await _context.Workspace.ToListAsync());
        }

        // GET: Workspace/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Workspace == null)
            {
                return NotFound();
            }

            var workspace = await _context.Workspace
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workspace == null)
            {
                return NotFound();
            }

            return View(workspace);
        }

        // GET: Workspace/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workspace/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Emri,UserName")] Workspace workspace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workspace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workspace);
        }

        // GET: Workspace/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Workspace == null)
            {
                return NotFound();
            }

            var workspace = await _context.Workspace.FindAsync(id);
            if (workspace == null)
            {
                return NotFound();
            }
            return View(workspace);
        }

        // POST: Workspace/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri,UserName")] Workspace workspace)
        {
            if (id != workspace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workspace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkspaceExists(workspace.Id))
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
            return View(workspace);
        }

        // GET: Workspace/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Workspace == null)
            {
                return NotFound();
            }

            var workspace = await _context.Workspace
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workspace == null)
            {
                return NotFound();
            }

            return View(workspace);
        }

        // POST: Workspace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Workspace == null)
            {
                return Problem("Entity set 'FlowCSDbContext.Workspace'  is null.");
            }
            var workspace = await _context.Workspace.FindAsync(id);
            if (workspace != null)
            {
                _context.Workspace.Remove(workspace);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkspaceExists(int id)
        {
          return _context.Workspace.Any(e => e.Id == id);
        }
    }
}

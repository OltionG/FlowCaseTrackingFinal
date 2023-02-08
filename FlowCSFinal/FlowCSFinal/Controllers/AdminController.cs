using FlowCSFinal.Data;
using FlowCSFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using X.PagedList;

namespace FlowCSFinal.Controllers
{
    public class AdminController : Controller
    {
        private readonly FlowCSDbContext _context;
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;

        public AdminController(UserManager<AppUser> userManager, FlowCSDbContext context, IPasswordHasher<AppUser> passwordHasher)
        {
            this.userManager = userManager;
            _context = context;
            this.passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UsernameSortParm = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.LastnameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var users = from s in _context.Users
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Mbiemri.Contains(searchString)
                                       || s.Emri.Contains(searchString)
                                       || s.UserName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "username_desc":
                    users = users.OrderByDescending(u => u.UserName);
                    break;
                case "name_desc":
                    users = users.OrderByDescending(u => u.Emri);
                    break;
                case "lastname_desc":
                    users = users.OrderByDescending(u => u.Mbiemri);
                    break;
                case "email_desc":
                    users = users.OrderByDescending(u => u.Email);
                    break;
                default:
                    users = users.OrderBy(s => s.Emri);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<AppUser>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Users user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    Emri = user.Emri,
                    Mbiemri = user.Mbiemri,
                    UserName = user.Username,
                    Email = user.Email
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string passwordHash, string emri, string mbiemri, string username)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(emri))
                    user.Emri = emri;
                else
                    ModelState.AddModelError("", "Name cannot be empty");

                if (!string.IsNullOrEmpty(mbiemri))
                    user.Mbiemri = mbiemri;
                else
                    ModelState.AddModelError("", "Last Name cannot be empty");

                if (!string.IsNullOrEmpty(username))
                    user.UserName = username;
                else
                    ModelState.AddModelError("", "Username cannot be empty");

                if (!string.IsNullOrEmpty(passwordHash))
                    user.PasswordHash = passwordHasher.HashPassword(user, passwordHash);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(passwordHash) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(emri) && !string.IsNullOrEmpty(mbiemri))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var user = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AppUsers == null)
            {
                return Problem("Entity set 'FlowCaseDbContext.Cards'  is null.");
            }
            var user = await _context.AppUsers.FindAsync(id);
            if (user != null)
            {
                _context.AppUsers.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Task4.Data;
using Task4.Models;

namespace Task4.Controllers
{
    [Authorize]
    [Authorize(Policy = "ActiveUserPolicy")]
    public class UserController : Controller
    {
        // GET: UserController
        ApplicationDbContext _context;
        UserManager<UserProfile> _userManager;
        SignInManager<UserProfile> _signInManager;
        public UserController(ApplicationDbContext context, UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            var viewModel = new UserModel { Users = users };
            return View(viewModel);
        }

        public void Block(List<UserProfile> users, bool block = false)
        {
            foreach (var user in users)
            {
                var updated = _context.Find<UserProfile>(user.Id);
                if (updated != null)
                {
                    updated.IsActive = block;
                    _context.Update(updated);
                }
            }

            _context.SaveChanges();
        }

        public void Delete(List<UserProfile> users)
        {
            foreach (var user in users)
            {
                var delete = _context.Find<UserProfile>(user.Id);
                if (delete != null)
                {
                    _context.Remove(delete);
                }
            }

            _context.SaveChanges();
        }


        public IActionResult Action(UserModel viewModel, [FromForm] string action)
        {
            var selected = viewModel.Users;
            if (!viewModel.SelectAll)
            {
                selected = viewModel.Users.Where(u => u.Selected).ToList();
            }
            switch (action)
            {
                case "Block":
                    Block(selected);
                    break;
                case "Unblock":
                    Block(selected, true);
                    break;
                case "Delete":
                    Delete(selected);
                    break;
                default:
                    break;
            }

            return RedirectToAction(nameof(Index));
        }
    }

    public class UserIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UserIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UserProfile> Users { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                Users = await _context.Users.ToListAsync();
            }
        }
    }
}

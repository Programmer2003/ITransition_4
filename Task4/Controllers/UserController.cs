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

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Delete(UserModel viewModel)
        {
            var selected = viewModel.Users;
            if (!viewModel.SelectAll)
            {
                selected = viewModel.Users.Where(u => u.Selected).ToList();
            }

            foreach (var user in selected)
            {
                _context.Remove(user);
            }

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

    public class UserIndexModel : PageModel
    {
        private readonly Task4.Data.ApplicationDbContext _context;

        public UserIndexModel(Task4.Data.ApplicationDbContext context)
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

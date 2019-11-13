using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository.DAL;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDAO _userDAO;

        public UsersController(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userDAO.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,Email,Password,ConfirmPassword")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (await _userDAO.HasUserWithEmail(user.Email))
            {
                ModelState.AddModelError("", $"Já existe um conta com o email: \"{user.Email}\"");
                return View(user);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.ConfirmPassword = string.Empty;

            await _userDAO.Save(user);

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userDAO.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Email,Password,ConfirmPassword,Id")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userDAO.Save(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_userDAO.Find(user.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
            }

            return View(user);
        }
    }
}

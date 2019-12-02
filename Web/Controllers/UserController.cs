using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository.DAL;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using Microsoft.AspNetCore.Authorization;
using Repository;
using Web.Services;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDAO _userDAO;
		private readonly UserRepository _userRepository;

        public UserController(UserDAO userDAO, UserRepository userRepository)
        {
            _userDAO = userDAO;
			_userRepository = userRepository;
        }

        // GET: Users/Details/5
		[Authorize(Policy = "ManageAccounts")]
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
		[AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[AllowAnonymous]
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

            user.Image = await IdenticonService.RequestInBase64(user.Email);

			await _userRepository.New(user);
			await AuthenticateUser(user);

            return RedirectToAction("Index", "Home");
        }

		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public async Task<IActionResult> Login([Bind("Email,Password")] User user)
		{
			var dbUser = await _userDAO.FindByEmail(user.Email);

			if (!BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
			{
				ModelState.AddModelError(string.Empty, "Credenciais inválidas");
				user.Password = string.Empty;

				return View(user);
			}

			await AuthenticateUser(dbUser);

			return RedirectToAction("Index", "Home");
		}

		protected async Task AuthenticateUser(User user)
		{
			var claims = new List<Claim>()
			{
				new Claim("Id", user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Name),
				new Claim(ClaimTypes.Email, user.Email)
			};

			foreach (PermissionType permission in user.GetPermissions())
			{
				claims.Add(new Claim(permission.ToString(), permission.ToString()));
			}

			var claimsIdentify = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				IssuedUtc = DateTime.Now
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentify),
				authProperties);
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Index", "Home");
		}

        // GET: Users/Edit/5
		[Authorize]
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
		[Authorize]
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

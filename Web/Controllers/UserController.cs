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
using Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDAO _userDAO;
		private readonly UserRepository _userRepository;
        private readonly GroupDAO _groupDAO;

        public UserController(UserDAO userDAO, UserRepository userRepository, GroupDAO groupDAO, AuthUser authUser)
            : base(authUser)
        {
            _userDAO = userDAO;
			_userRepository = userRepository;
            _groupDAO = groupDAO;
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

            user.Image = await IdenticonService.RequestInBase64(Hasher.HashMD5(user.Email));

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

        [Authorize(Policy = "ManageAccounts")]
        public async Task<IActionResult> All()
        {
            return View(await _userDAO.GetAll());
        }

        // GET: Users/Edit/5
        [Authorize(Policy = "ManageAccounts")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userDAO.FindOrDefault(id);
            if (user == null)
            {
                return NotFound();
            }

            await PutGroupsInView(user);

            return View((UserEditable)user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Policy = "ManageAccounts")]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Email,Password,Id")] UserEditable userE, int[] groups)
        {
            if (id != userE.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User user = (User)userE;
                    User original = await _userDAO.FindOrDefault(user.Id);

                    if (original == null)
                    {
                        return NotFound();
                    }

                    original.Name = user.Name;
                    original.Email = user.Email;

                    if (!string.IsNullOrWhiteSpace(user.Password))
                    {
                        original.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    }
                    
                    await _userRepository.Update(original, groups);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_userDAO.Find(userE.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            await PutGroupsInView((User)userE);
            return View(userE);
        }

        [Authorize]
        public async Task<IActionResult> Settings()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string id = identity.FindFirst("Id").Value ?? "0";

            if (id.Equals("0"))
            {
                return RedirectToAction(nameof(Login));
            }

            var user = await _userDAO.FindOrDefault(Convert.ToInt32(id));
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Settings([Bind("Name,Email,Password,ConfirmPassword,Id")] User user)
        {
            var originalUser = await _userDAO.FindOrDefault(user.Id);

            if (originalUser == null)
            {
                return RedirectToAction(nameof(Login));
            }

            if (!string.IsNullOrEmpty(user.Name))
            {
                originalUser.Name = user.Name;
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                originalUser.Email = user.Email;
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                originalUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userDAO.Save(originalUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_userDAO.FindOrDefault(user.Id) == null)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        private async Task PutGroupsInView(User u = null)
        {
            ViewBag.Groups = new MultiSelectList(
                await _groupDAO.GetAll(),
                "Id", "Name", u?.GetGroups().Select(x => x.Id).ToArray());
        }
    }
}

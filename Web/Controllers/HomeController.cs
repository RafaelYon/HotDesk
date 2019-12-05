using Microsoft.AspNetCore.Mvc;
using Repository;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
	public class HomeController : Web.Controllers.Controller
    {
		private Context _context;

		public HomeController(Context context, AuthUser authUser) : base(authUser)
		{
			_context = context;
		}

		public IActionResult Index()
        {
			if (!_authUser.IsAuthenticated(this))
			{
				return RedirectToAction("Login", "User");
			}

			return RedirectToAction("Index", "Issues");
        }
	}
}
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Models;
using MicrosoftMVC = Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class Controller : MicrosoftMVC.Controller
	{
		private const string ALERT_KEY = "Alert";

        protected readonly AuthUser _authUser;

        public Controller(AuthUser authUser)
        {
            _authUser = authUser;
        }

		protected void AddAlert(Alert alert)
		{
			TempData[ALERT_KEY] = alert;
		}

		protected void AddAlert(AlertType type, string content)
		{
			AddAlert(new Alert
			{
				Type = type,
				Content = content
			});
		}

		protected void AddAlert(AlertType type, TitleType titleType, string title, string content)
		{
			AddAlert(new Alert
			{
				Type = type,
				TitleType = titleType,
				Title = title,
				Content = content
			});
		}

        protected IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "User");
        }
	}
}

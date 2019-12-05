using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
			TempData[ALERT_KEY] = JsonConvert.SerializeObject(alert);
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

		[NonAction]
        public override ViewResult View()
		{
			PutManagePermissionsOnView();

			return base.View();
		}

		[NonAction]
        public override ViewResult View(object model)
		{
			PutManagePermissionsOnView();

			return base.View(model);
		}

        protected void PutManagePermissionsOnView()
        {
			try
			{
				ViewBag.CanManageAccounts = _authUser.HasPermission(this, PermissionType.ManageAccounts);
				ViewBag.CanManageCategories = _authUser.HasPermission(this, PermissionType.ManageCategories);
				ViewBag.CanManageGroups = _authUser.HasPermission(this, PermissionType.ManageGroups);
			}
			catch (Exception) { }
        }
    }
}

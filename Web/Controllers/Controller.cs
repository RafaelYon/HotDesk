using Web.Models;
using MicrosoftMVC = Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class Controller : MicrosoftMVC.Controller
	{
		private const string ALERT_KEY = "Alert";

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
	}
}

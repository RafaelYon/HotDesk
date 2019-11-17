using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public enum AlertType
	{
		Light,
		Danger,
		Warning,
		Success,
		Info,
		Primary,
		Secondary
	}

	public enum TitleType
	{
		None,
		H1,
		H2,
		H3,
		H4,
		H5,
		H6
	}

	public class Alert
	{
		public AlertType Type { get; set; }

		public TitleType TitleType { get; set; }
		public string Title { get; set; }

		public string Content { get; set; }

		public string GetAlertClass()
		{
			return $"alert-{Type.ToString().ToLower()}";
		}

		public string GetTitle()
		{
			if (TitleType == TitleType.None)
			{
				return null;
			}

			string tagIdentifier = TitleType.ToString().ToLower();

			return $"<{tagIdentifier}>{Title}</{tagIdentifier}>";
		}
	}
}

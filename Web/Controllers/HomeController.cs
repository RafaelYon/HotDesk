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
			//var permission = new Permission()
			//{
			//	Name = "Criar chamado"
			//};

			//_context.Permissions.Add(permission);

			//var group = new Group()
			//{
			//	Name = "Cliente",
			//};

			//_context.Groups.Add(group);

			//_context.SaveChanges();

			//group.GroupPermission.Add(new GroupPermission()
			//{
			//	PermissionId = permission.Id,
			//	GroupId = group.Id
			//});

			//_context.Groups.Update(group);

			//_context.SaveChanges();

			return View();
        }
	}
}
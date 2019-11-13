using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
		private Context _context;

		public HomeController(Context context)
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
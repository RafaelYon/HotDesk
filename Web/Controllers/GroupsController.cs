using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository;
using Microsoft.AspNetCore.Authorization;
using Repository.DAL;

namespace Web.Controllers
{
    [Authorize(Policy = "ManageGroups")]
    public class GroupsController : Controller
    {
        private readonly GroupDAO _groupDAO;
        private readonly Context _context;

        public GroupsController(GroupDAO groupDAO, Context context)
        {
            _groupDAO = groupDAO;
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            return View(await _groupDAO.GetAll());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _groupDAO.FindOrDefault(id);

            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public async Task<IActionResult> Create()
        {
            await PutPermissionsInView();

            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Default")] Group @group, int[] permissions)
        {
            if (ModelState.IsValid)
            {
                if (await GroupExists(@group.Name))
                {
                    ModelState.AddModelError("", $"O nome {group.Name} já está em uso");

                    await PutPermissionsInView();
                    return View(@group);
                }

                await SaveGroup(@group, permissions);

                return RedirectToAction(nameof(Index));
            }

            await PutPermissionsInView();
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _groupDAO.FindOrDefault(id);
            if (@group == null)
            {
                return NotFound();
            }

            await PutPermissionsInView(@group);

            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Default,Id")] Group @group, int[] permissions)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await CheckIfNameIsUsedByAnother(@group))
                    {
                        ModelState.AddModelError("", $"O nome {@group.Name} já está em uso");
                        await PutPermissionsInView(@group);
                        return View(@group);
                    }

                    await SaveGroup(@group, permissions);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            await PutPermissionsInView(@group);
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _groupDAO.FindOrDefault(id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _groupDAO.Find(id);
            await _groupDAO.Delete(@group);
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _groupDAO.FindOrDefault(id) != null;
        }

        private async Task<bool> GroupExists(string name)
        {
            return await _groupDAO.FindByName(name) != null;
        }

        private async Task<bool> CheckIfNameIsUsedByAnother(Group group)
        {
            return await _groupDAO.FindAnotherByName(group, group.Name) != null;
        }

        private async Task PutPermissionsInView(Group g = null)
        {
            ViewBag.Permissions = new MultiSelectList(
                await _context.Permissions.ToListAsync(),
                "Id", "Name", g?.GetPermissions().Select(x => x.Id).ToArray());
        }

        private async Task SaveGroup(Group @group, int[] permissions)
        {
            if (@group.Id == 0)
            {
                await _groupDAO.Save(@group);
            }

            foreach (var permissionId in permissions)
            {
                @group.GroupPermissions.Add(new GroupPermission
                {
                    GroupId = @group.Id,
                    PermissionId = permissionId
                });
            }

            await _groupDAO.Save(@group);
        }
    }
}

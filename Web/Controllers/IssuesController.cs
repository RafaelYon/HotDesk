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
using Web.Helpers;
using Repository.DAL;

namespace Web.Controllers
{
    [Authorize]
    public class IssuesController : Controller
    {
        private readonly IssueDAO _issueDAO;
        private readonly CategoryDAO _categoryDAO;

        public IssuesController(IssueDAO issueDAO, CategoryDAO categoryDAO, AuthUser authUser) : base(authUser)
        {
            _issueDAO = issueDAO;
            _categoryDAO = categoryDAO;
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _authUser.GetUser(this);

                return View(user.IssuesCreated);
            }
            catch (Exception)
            {
                return RedirectToLogin();
            }
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                Issue issue = null;

                if (_authUser.HasPermission(this, PermissionType.IssueClose))
                {
                    issue = await _issueDAO.FindOrDefault(id);
                }
                else
                {
                    issue = await _issueDAO.GetIssueOfRelatedUser(await _authUser.GetUser(this), id ?? 0);
                }

                if (issue == null)
                {
                    return NotFound();
                }

                return View(issue);
            }
            catch (Exception)
            {
                return RedirectToLogin();
            }
        }

        // GET: Issues/Create
        [Authorize(Policy = "IssueCreate")]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryDAO.GetAll();

            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description")] Issue issue, int categoryId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    issue.Category = await _categoryDAO.FindOrDefault(categoryId);
                    issue.Owner = await _authUser.GetUser(this);

                    if (issue.Category == null)
                    {
                        return NotFound();
                    }

                    await _issueDAO.Save(issue);

                    return RedirectToAction(nameof(Index));
                }

                return View(issue);
            }
            catch (Exception)
            {
                return RedirectToLogin();
            }
        }

        private async Task<bool> IssueExists(int id)
        {
            return await _issueDAO.FindOrDefault(id) != null;
        }
    }
}

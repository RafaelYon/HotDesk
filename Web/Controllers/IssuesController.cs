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
                List<Issue> issues = null;

                if (_authUser.HasPermission(this, PermissionType.IssueClose))
                {
                    issues = await _issueDAO.GetAllOrderned();
                }
                else
                {
                    issues = await _issueDAO.GetIssuesOfRelatedUser(await _authUser.GetUser(this));
                }

                return View(issues);
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

                var user = await _authUser.GetUser(this);

                if (_authUser.HasPermission(this, PermissionType.IssueClose))
                {
                    issue = await _issueDAO.FindOrDefault(id);
                }
                else
                {
                    issue = await _issueDAO.GetIssueOfRelatedUser(user, id ?? 0);
                }

                if (issue?.Status != IssueStatus.Closed)
                {
                    ViewBag.CanComment = true;

                    if (issue?.Owner.Id != user.Id && issue?.Responsible == null && _authUser.HasPermission(this, PermissionType.IssueAccept))
                    {
                        ViewBag.CanAssign = true;
                    }
                    else
                    {
                        ViewBag.CanAssign = false;
                    }

                    if (issue?.Responsible?.Id == user.Id || _authUser.HasPermission(this, PermissionType.IssueClose))
                    {
                        ViewBag.CanClose = true;
                    }
                    else
                    {
                        ViewBag.CanClose = false;
                    }

                    ViewBag.CanRate = false;
                }
                else 
                {
                    ViewBag.CanComment = false;
                    ViewBag.CanAssign = false;
                    ViewBag.CanClose = false;

                    if (issue?.Owner.Id == user.Id)
                    {
                        ViewBag.CanRate = true;
                    }
                    else
                    {
                        ViewBag.CanRate = false;
                    }
                }

                if (issue == null)
                {
                    return NotFound();
                }

                return View(issue);
            }
            catch (Exception e)
            {
                return RedirectToLogin();
            }
        }

        // GET: Issues/Create
        [Authorize(Policy = "IssueCreate")]
        public async Task<IActionResult> Create()
        {
            await PutCategoriesInView();

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

                    return RedirectToDetaits(issue.Id);
                }

                await PutCategoriesInView();
                return View(issue);
            }
            catch (Exception)
            {
                return RedirectToLogin();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IssueAccept")]
        public async Task<IActionResult> Assign(int id)
        {
            var issue = await _issueDAO.FindOrDefault(id);

            if (issue == null)
            {
                return NotFound();
            }

            if (issue.Responsible == null || issue.Status == IssueStatus.Closed)
            {
                AddAlert(Models.AlertType.Warning, "O chamado não está disponível para efetuar esta ação");
                return RedirectToDetaits(id);
            }

            issue.Responsible = await _authUser.GetUser(this);
            await _issueDAO.Save(issue);

            AddAlert(Models.AlertType.Success, "Você foi associação como reponsável pelo chamado!");
            return RedirectToDetaits(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Close(int id)
        {
            var issue = await _issueDAO.FindOrDefault(id);

            if (issue == null)
            {
                return NotFound();
            }

            var user = await _authUser.GetUser(this);

            if (issue?.Responsible?.Id != user.Id && !_authUser.HasPermission(this, PermissionType.IssueClose))
            {
                AddAlert(Models.AlertType.Danger, "Desculpe, mas você não pode executar esta ação");
                return RedirectToDetaits(id);
            }

            issue.Status = IssueStatus.Closed;
            await _issueDAO.Save(issue);

            AddAlert(Models.AlertType.Warning, "O chamado foi finalizado");
            return RedirectToDetaits(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IssueRateAssistence")]
        public async Task<IActionResult> Rate(int id, float rate)
        {
            var issue = await _issueDAO.FindOrDefault(id);

            if (issue == null)
            {
                return NotFound();
            }

            var user = await _authUser.GetUser(this);

            if (issue.Status != IssueStatus.Closed || issue.Owner.Id != user.Id)
            {
                AddAlert(Models.AlertType.Danger, "Desculpe, mas você não pode executar esta ação");
                return RedirectToDetaits(id);
            }

            issue.Rate = rate;
            await _issueDAO.Save(issue);

            AddAlert(Models.AlertType.Warning, "O chamado foi avaliado");
            return RedirectToDetaits(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(int id, string comment)
        {
            var issue = await _issueDAO.FindOrDefault(id);

            if (issue == null)
            {
                return NotFound();
            }

            var user = await _authUser.GetUser(this);

            if (issue.Owner.Id != user.Id && issue.Responsible?.Id != user.Id && !_authUser.HasPermission(this, PermissionType.IssueClose))
            {
                AddAlert(Models.AlertType.Danger, "Desculpe, mas você não pode executar esta ação");
                return RedirectToDetaits(id);
            }

            issue.Comments.Add(new IssuesComment
            {
                Comment = comment,
                CreatedBy = user,
                Issue = issue
            });
            
            await _issueDAO.Save(issue);

            AddAlert(Models.AlertType.Warning, "Comentário adicionado com sucesso");
            return RedirectToDetaits(id);
        }

        private async Task PutCategoriesInView()
        {
            ViewBag.Categories = new SelectList(
                await _categoryDAO.GetAll(),
                "Id", "Name"
            );
        }

        private async Task<bool> IssueExists(int id)
        {
            return await _issueDAO.FindOrDefault(id) != null;
        }

        private IActionResult RedirectToDetaits(int idValue)
        {
            return RedirectToAction(nameof(Details), new {id = idValue});
        }
    }
}

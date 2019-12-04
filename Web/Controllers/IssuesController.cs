﻿using System;
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
                return RedirectToAction(nameof(Details), id);
            }

            issue.Responsible = await _authUser.GetUser(this);
            await _issueDAO.Save(issue);

            AddAlert(Models.AlertType.Success, "Você foi associação como reponsável pelo chamado!");
            return RedirectToAction(nameof(Details), id);
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

            if (issue.Responsible.Id != user.Id && !_authUser.HasPermission(this, PermissionType.IssueClose))
            {
                AddAlert(Models.AlertType.Danger, "Desculpe, mas você não pode executar esta ação");
                return RedirectToAction(nameof(Details), id);
            }

            issue.Status = IssueStatus.Closed;
            await _issueDAO.Save(issue);

            AddAlert(Models.AlertType.Warning, "O chamado foi finalizado");
            return RedirectToAction(nameof(Details), id);
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
                return RedirectToAction(nameof(Details), id);
            }

            issue.Rate = rate;
            await _issueDAO.Save(issue);

            AddAlert(Models.AlertType.Warning, "O chamado foi avaliado");
            return RedirectToAction(nameof(Details), id);
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
                return RedirectToAction(nameof(Details), id);
            }

            issue.Comments.Add(new IssuesComment
            {
                Comment = comment,
                CreatedBy = user,
                Issue = issue
            });
            
            await _issueDAO.Save(issue);

            AddAlert(Models.AlertType.Warning, "Comentário adicionado com sucesso");
            return RedirectToAction(nameof(Details), id);
        }

        private async Task<bool> IssueExists(int id)
        {
            return await _issueDAO.FindOrDefault(id) != null;
        }
    }
}

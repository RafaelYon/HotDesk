using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DAL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IssueDAO _issueDAO;

        public IssuesController(IssueDAO issueDAO)
        {
            _issueDAO = issueDAO;
        }

        [HttpGet]
        public async Task<ActionResult<List<Issue>>> Get()
        {
            return await _issueDAO.GetAll();
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<List<Issue>>> Get(string status)
        {
            try
            {
                IssueStatus iStatus = (IssueStatus)Enum.Parse(typeof(IssueStatus), status);

                return await _issueDAO.GetBystatus(iStatus);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("Rate")]
        public async Task<ActionResult<dynamic>> Rate()
        {
            return await _issueDAO.GetRates();
        }
    }
}
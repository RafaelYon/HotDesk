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
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryDAO _categoryDAO;

        public CategoriesController(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return await _categoryDAO.GetAll();
        }
    }
}
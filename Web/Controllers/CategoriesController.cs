using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository.DAL;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
	[Authorize(Policy = "ManageCategories")]
	public class CategoriesController : Controller
    {
		private readonly CategoryDAO _categoryDAO;

        public CategoriesController(CategoryDAO categoryDAO)
        {
			_categoryDAO = categoryDAO;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryDAO.GetAll());
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (await CategoryExists(category.Name)) {
                    ModelState.AddModelError("", $"Já foi cadastrado uma categoria com o nome \"{category.Name}\"");

                    return View(category);
                }
                
				await _categoryDAO.Save(category);

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryDAO.FindOrDefault(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					await _categoryDAO.Save(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var category = await _categoryDAO.FindOrDefault(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
			var category = await _categoryDAO.FindOrDefault(id);

			if (category == null)
			{
				return NotFound();
			}

			await _categoryDAO.Delete(category);

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
			return _categoryDAO.FindOrDefault(id) != null;
        }

        private async Task<bool> CategoryExists(string name)
        {
            return await _categoryDAO.FindByName(name) != null;
        }
    }
}

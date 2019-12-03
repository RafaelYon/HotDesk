using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository.DAL
{
	public class CategoryDAO : ModelDAO<Category>
	{
		public CategoryDAO(Context context) : base(context) { }

		protected override DbSet<Category> GetDbSet()
		{
			return _context.Categories;
		}

		public async Task<Category> FindByName(string name)
		{
			return await GetDbSet().FirstOrDefaultAsync(x => x.Name.Equals(name));
		}

		public async Task<Category> FindAnotherByName(Category categoryToIgnore, string name)
		{
			return await GetDbSet().FirstOrDefaultAsync(x => x.Id != categoryToIgnore.Id && x.Name.Equals(name));
		}
	}
}

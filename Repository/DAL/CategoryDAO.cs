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
	}
}

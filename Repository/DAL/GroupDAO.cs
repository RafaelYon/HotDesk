using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.DAL
{
	public class GroupDAO : ModelDAO<Group>
	{
		public GroupDAO(Context context) : base(context) { }

		protected override DbSet<Group> GetDbSet()
		{
			return _context.Groups;
		}

		public async Task<List<Group>> GetDefaults()
		{
			return await GetDbSet().Where(x => x.Default == true).ToListAsync();
		}
	}
}

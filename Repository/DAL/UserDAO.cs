using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class UserDAO : ModelDAO<User>
    {
        public UserDAO(Context context) : base(context) { }

        protected override DbSet<User> GetDbSet()
        {
            return _context.Users;
        }

		protected override DbSet<User> GetDbSetWithIncludes()
		{
			return (DbSet<User>) GetDbSet().Include(x => x.GroupUser)
				.ThenInclude(gu => gu.Group)
				.ThenInclude(g => g.GroupPermissions)
				.ThenInclude(gp => gp.Permission);
		}

		public async Task<User> FindByEmail(string email)
        {
			return await GetDbSet().FirstAsync(x => x.Email.Equals(email));
        }

        public async Task<bool> HasUserWithEmail(string email)
        {
            try
            {
                await FindByEmail(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

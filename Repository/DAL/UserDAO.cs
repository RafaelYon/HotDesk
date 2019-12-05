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

		public async Task<User> FindByEmail(string email)
        {
			return await GetDbSet().FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<bool> HasUserWithEmail(string email)
        {
            try
            {
                var user = await FindByEmail(email);

                return user != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

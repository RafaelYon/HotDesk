using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
	public class UserRepository
	{
		private readonly UserDAO _userDAO;
		private readonly GroupDAO _groupDAO;
        private readonly Context _context;

		public UserRepository(UserDAO userDAO, GroupDAO groupDAO, Context context)
		{
			_userDAO = userDAO;
			_groupDAO = groupDAO;
            _context = context;
		}

		public async Task New(User user)
		{
			foreach (Group defaultGroup in await _groupDAO.GetDefaults())
			{
				user.GroupUser.Add(new GroupUser
				{
					Group = defaultGroup,
					User = user
				});
			}

			await _userDAO.Save(user);
		}

        public async Task Update(User user, int[] groups = null)
        {
            if (groups != null)
            {
                user.ResetGroups();

                // Clear old groups
                var oldGroups = await _context.GroupUser.Where(x => x.UserId == user.Id).ToListAsync();
                _context.GroupUser.RemoveRange(oldGroups);

                foreach (var groupId in groups)
                {
                    user.GroupUser.Add(new GroupUser
                    {
                        GroupId = groupId,
                        UserId = user.Id
                    });
                }
            }

            await _userDAO.Save(user);
        }
	}
}

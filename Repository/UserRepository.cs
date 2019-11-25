using Domain;
using Repository.DAL;
using System.Threading.Tasks;

namespace Repository
{
	public class UserRepository
	{
		private readonly UserDAO _userDAO;
		private readonly GroupDAO _groupDAO;

		public UserRepository(UserDAO userDAO, GroupDAO groupDAO)
		{
			_userDAO = userDAO;
			_groupDAO = groupDAO;
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
	}
}

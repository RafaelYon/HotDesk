using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Repository.DAL
{
    public class IssueDAO : ModelDAO<Issue>
    {
        public IssueDAO(Context context) : base(context) { }

        protected override DbSet<Issue> GetDbSet()
        {
            return _context.Issues;
        }

        public async Task<Issue> GetIssueOfRelatedUser(User user, int id)
        {
            return await GetDbSet().FirstOrDefaultAsync(
                x => (x.Owner.Id == user.Id || x.Responsible.Id == user.Id)
                    && x.Id == id
            );
        }
    }
}

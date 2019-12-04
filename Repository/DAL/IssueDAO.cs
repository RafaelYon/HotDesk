using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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
                x => (x.Owner.Id == user.Id || (x.Responsible != null && x.Responsible.Id == user.Id))
                    && x.Id == id
            );
        }

        public async Task<List<Issue>> GetIssuesOfRelatedUser(User user)
        {
            return await GetDbSet()
                .Where(x => x.Owner.Id == user.Id || (x.Responsible != null && x.Responsible.Id == user.Id))
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Issue>> GetAllOrderned()
        {
            return await GetDbSet().OrderBy(x => x.CreatedAt).ToListAsync();
        }
    }
}

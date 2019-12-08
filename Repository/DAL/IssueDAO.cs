using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

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

        public async Task<List<Issue>> GetBystatus(IssueStatus status)
        {
            return await GetDbSet().Where(x => x.Status == status).ToListAsync();
        }

        public async Task<dynamic> GetRates()
        {
            return await GetDbSet()
                .GroupBy(x => new { Id = x.Responsible.Id })
                .Select(x => new { Responsible = x.Key.Id, Average = x.Average(p => p.Rate) })
                .ToListAsync();
        }
    }
}

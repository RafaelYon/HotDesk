using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public abstract class ModelDAO<T> : IDAO<T> where T : Model
    {
        protected readonly Context _context;

        public ModelDAO(Context context)
        {
            _context = context;
        }

        protected abstract DbSet<T> GetDbSet();

        public async Task Save(T model)
        {
            if (model.Id == 0)
            {
                await GetDbSet().AddAsync(model);
            }
            else
            {
                model.UpdateDate();
                GetDbSet().Update(model);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(T model)
        {
            GetDbSet().Remove(model);

            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await GetDbSet().ToListAsync();
        }

        public async Task<T> Find(params object[] keysValues)
        {
			return await GetDbSet().FindAsync(keysValues);
        }

		public async Task<T> FindOrDefault(params object[] keyValues)
		{
			try
			{
				return await Find(keyValues);
			}
			catch (Exception)
			{
				return null;
			}
		}
    }
}

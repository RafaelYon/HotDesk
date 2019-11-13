using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public interface IDAO<T>
    {
        Task Save(T model);

        Task Delete(T model);

        Task<List<T>> GetAll(T model);

        Task<T> Find(params object[] keysValues);
    }
}

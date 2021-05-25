using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync(string sqlCommand=null);
        Task<int> UpsertAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}

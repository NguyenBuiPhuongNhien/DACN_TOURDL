using DO_AN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public interface IDanhlamtcRepository
    {
        Task<IEnumerable<Danhlamtc>> GetAllAsync();
        Task<Danhlamtc> GetByIdAsync(int madl);
        Task AddAsync(Danhlamtc danhlamtc);
        Task UpdateAsync(Danhlamtc danhlamtc);
        Task DeleteAsync(int madl);
    }
}

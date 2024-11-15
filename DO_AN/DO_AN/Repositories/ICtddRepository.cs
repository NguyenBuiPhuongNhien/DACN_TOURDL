using System.Collections.Generic;
using System.Threading.Tasks;
using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface ICtddRepository
    {
        Task<IEnumerable<Ctdd>> GetAllAsync();
        Task<Ctdd> GetByIdAsync(int malt, int maks, int madtq);
        Task AddAsync(Ctdd ctdd);
        Task UpdateAsync(Ctdd ctdd);
        Task DeleteAsync(int malt, int maks, int madtq);
        Task<IEnumerable<Ctdd>> GetallMaltAsync(int malt);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface ICtDtqRepository
    {
        Task<IEnumerable<CtDtq>> GetAllAsync();
        Task<CtDtq> GetByIdAsync(int madtq, int madl);
        Task AddAsync(CtDtq ctdtq);
        Task UpdateAsync(CtDtq ctdtq);
        Task DeleteAsync(int madtq, int madl);

    }
}

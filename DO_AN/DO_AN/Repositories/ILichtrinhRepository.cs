using DO_AN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public interface ILichtrinhRepository
    {
        Task<IEnumerable<Lichtrinh>> GetAllAsync();
        Task<Lichtrinh> GetByIdAsync(int malt);
        Task AddAsync(Lichtrinh lichtrinh);
        Task UpdateAsync(Lichtrinh lichtrinh);
        Task DeleteAsync(int malt);
    }
}

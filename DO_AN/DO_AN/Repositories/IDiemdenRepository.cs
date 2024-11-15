using DO_AN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public interface IDiemdenRepository
    {
        Task<IEnumerable<Diemden>> GetAllAsync();
        Task<Diemden> GetByIdAsync(int madd);
        Task AddAsync(Diemden diemden);
        Task UpdateAsync(Diemden diemden);
        Task DeleteAsync(int madd);
    }
}

using DO_AN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public interface IDiemthamquanRepository
    {
        Task<IEnumerable<Diemthamquan>> GetAllAsync();
        Task<Diemthamquan> GetByIdAsync(int madtq);
        Task AddAsync(Diemthamquan diemthamquan);
        Task UpdateAsync(Diemthamquan diemthamquan);
        Task DeleteAsync(int madtq);
    }
}

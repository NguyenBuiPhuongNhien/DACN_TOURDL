using DO_AN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public interface IKhuyenmaiRepository
    {
        Task<IEnumerable<Khuyenmai>> GetAllAsync();
        Task<Khuyenmai> GetByIdAsync(string makm);
        Task AddAsync(Khuyenmai khuyenmai);
        Task UpdateAsync(Khuyenmai khuyenmai);
        Task DeleteAsync(string makm);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface IDanhgiaRepository
    {
        Task<IEnumerable<Danhgia>> GetAllAsync();
        Task<Danhgia> GetByIdAsync(int makh, int matour);
        Task AddAsync(Danhgia danhgia);
        Task UpdateAsync(Danhgia danhgia);
        Task DeleteAsync(int makh, int matour);
    }
}

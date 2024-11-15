using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface IHinhanhRepository
    {
        Task<IEnumerable<Hinhanh>> GetAllAsync();
        Task<Hinhanh> GetByIdAsync(int mah);
        Task AddAsync(Hinhanh hinhanh);
        Task UpdateAsync(Hinhanh hinhanh);
        Task DeleteAsync(int mah);
    }
}

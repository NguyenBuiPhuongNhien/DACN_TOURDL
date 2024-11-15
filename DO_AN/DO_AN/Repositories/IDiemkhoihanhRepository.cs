using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface IDiemkhoihanhRepository
    {
        Task<IEnumerable<Diemkhoihanh>> GetAllAsync();
        Task<Diemkhoihanh> GetByIdAsync(int madkh);
        Task AddAsync(Diemkhoihanh diemkhoihanh);
        Task UpdateAsync(Diemkhoihanh diemkhoihanh);
        Task DeleteAsync(int madkh);
    }
}

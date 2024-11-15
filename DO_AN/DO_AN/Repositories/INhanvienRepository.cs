using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface INhanvienRepository
    {
        Task<IEnumerable<Nhanvien>> GetAllAsync();
        Task<Nhanvien> GetByIdAsync(int manv);
        Task<Nhanvien> GetBySdtAsync(string sdt);
        Task AddAsync(Nhanvien nhanvien);
        Task UpdateAsync(Nhanvien nhanvien);
        Task DeleteAsync(int manv);
    }
}

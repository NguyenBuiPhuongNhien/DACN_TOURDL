using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface IKhachhangRepository
    {
        Task<IEnumerable<Khachhang>> GetAllAsync();
        Task<Khachhang> GetByIdAsync(int makh);
        Task<Khachhang> GetBySdtAsync(string sdt);
        Task AddAsync(Khachhang khachhang);
        Task UpdateAsync(Khachhang khachhang);
        Task DeleteAsync(int makh);
    }
}

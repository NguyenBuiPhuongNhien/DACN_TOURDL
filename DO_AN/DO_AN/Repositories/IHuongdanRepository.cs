using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface IHuongdanRepository
    {
        Task<IEnumerable<Huongdan>> GetAllAsync();
        Task<Huongdan> GetByIdAsync(int manv, int matour);
        Task AddAsync(Huongdan huongdan);
        Task UpdateAsync(Huongdan huongdan);
        Task DeleteAsync(int manv, int matour);
    }
}

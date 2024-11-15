using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface IKhachsanRepository
    {
        Task<IEnumerable<Khachsan>> GetAllAsync();
        Task<Khachsan> GetByIdAsync(int maks);
        Task AddAsync(Khachsan khachsan);
        Task UpdateAsync(Khachsan khachsan);
        Task DeleteAsync(int maks);
    }
}

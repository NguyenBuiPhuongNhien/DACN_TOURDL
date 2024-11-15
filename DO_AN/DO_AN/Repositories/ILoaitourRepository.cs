using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface ILoaitourRepository
    {
        Task<IEnumerable<Loaitour>> GetAllAsync();
        Task<Loaitour> GetByIdAsync(int maloai);
        Task AddAsync(Loaitour loaitour);
        Task UpdateAsync(Loaitour loaitour);
        Task DeleteAsync(int maloai);
    }
}

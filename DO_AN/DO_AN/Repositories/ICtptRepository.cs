using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface ICtptRepository
    {
        Task<IEnumerable<Ctpt>> GetAllAsync();
        Task<Ctpt> GetByIdAsync(int matour, int mapt);
        Task AddAsync(Ctpt ctpt);
        Task UpdateAsync(Ctpt ctpt);
        Task DeleteAsync(int matour, int mapt);
    }
}

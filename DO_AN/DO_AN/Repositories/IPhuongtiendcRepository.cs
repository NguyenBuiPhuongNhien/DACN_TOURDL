using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface IPhuongtiendcRepository
    {
        Task<IEnumerable<Phuongtiendc>> GetAllAsync();
        Task<Phuongtiendc> GetByIdAsync(int mapt);
        Task AddAsync(Phuongtiendc phuongtiendc);
        Task UpdateAsync(Phuongtiendc phuongtiendc);
        Task DeleteAsync(int mapt);
    }
}

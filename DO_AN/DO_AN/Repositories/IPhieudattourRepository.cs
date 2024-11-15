using System.Collections.Generic;
using System.Threading.Tasks;
using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface IPhieudattourRepository
    {
        Task<IEnumerable<Phieudattour>> GetAllAsync();
        Task<Phieudattour> GetByIdAsync(int mapdt);
        Task AddAsync(Phieudattour phieudattour);
        Task UpdateAsync(Phieudattour phieudattour);
        Task DeleteAsync(int mapdt);
        Task UpdateSLTour(int matour);
        Task UpdateSLTourDaDat(int makh);
    }
}

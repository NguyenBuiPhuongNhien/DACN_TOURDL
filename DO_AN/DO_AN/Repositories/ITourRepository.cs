using DO_AN.Models;

namespace DO_AN.Repositories
{
    public interface ITourRepository
    {
        Task<IEnumerable<Tour>> GetAllAsync();
        Task<Tour> GetByIdAsync(int matour);
        Task AddAsync(Tour tour);
        Task UpdateAsync(Tour tour);
        Task DeleteAsync(int matour);
        Task<IEnumerable<Tour>> GetToursByDestinationAsync(int madd);
        Task<IEnumerable<Tour>> GetTourInDiemthamquan(int madtq);
        Task<IEnumerable<Tour>> GetTourInDiemden(int madd);
        Task<IEnumerable<Tour>> GetTourInloaitour(int maloai);
        Task<IEnumerable<Tour>> GetTourInDiemkhoihanh(int madkh);
        Task<IEnumerable<Tour>> GetTourInDanhlam(int madl);

        Task<IEnumerable<Tour>> SearchByNameAsync(string query);

    }
}

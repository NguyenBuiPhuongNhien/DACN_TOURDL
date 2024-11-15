using DO_AN.Models;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.Repositories
{
    public class EFDiemkhoihanhRepository : IDiemkhoihanhRepository
    {
        private readonly DoAnContext _context;


        public EFDiemkhoihanhRepository(DoAnContext context)
        {
            _context = context;
        }
        public async  Task AddAsync(Diemkhoihanh diemkhoihanh)
        {
            _context.Diemkhoihanhs.Add(diemkhoihanh);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int madkh)
        {
            var diemkhoihanh = await _context.Diemkhoihanhs.FindAsync(madkh);
            _context.Diemkhoihanhs.Remove(diemkhoihanh);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Diemkhoihanh>> GetAllAsync()
        {
            return await _context.Diemkhoihanhs.ToListAsync();
                     // Include thông tin về category
                    
        }

        public async Task<Diemkhoihanh> GetByIdAsync(int madkh)
        {
            return await _context.Diemkhoihanhs.FirstOrDefaultAsync(p => p.Madkh == madkh);
        }

        public async Task UpdateAsync(Diemkhoihanh diemkhoihanh)
        {
            _context.Diemkhoihanhs.Update(diemkhoihanh);
            await _context.SaveChangesAsync();
        }
    }
}

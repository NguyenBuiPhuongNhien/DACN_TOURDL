using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFHuongdanRepository : IHuongdanRepository
    {
        private readonly DoAnContext _context;

        public EFHuongdanRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Huongdan>> GetAllAsync()
        {
            return await _context.Huongdans
                .Include(h => h.ManvNavigation)
                .Include(h => h.MatourNavigation)
                .ToListAsync();
        }

        public async Task<Huongdan> GetByIdAsync(int manv, int matour)
        {
            return await _context.Huongdans
                .Include(h => h.ManvNavigation)
                .Include(h => h.MatourNavigation)
                .FirstOrDefaultAsync(h => h.Manv == manv && h.Matour == matour);
        }

        public async Task AddAsync(Huongdan huongdan)
        {
            _context.Huongdans.Add(huongdan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Huongdan huongdan)
        {
            _context.Huongdans.Update(huongdan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int manv, int matour)
        {
            var huongdan = await _context.Huongdans.FindAsync(manv, matour);
            if (huongdan != null)
            {
                _context.Huongdans.Remove(huongdan);
                await _context.SaveChangesAsync();
            }
        }
    }
}

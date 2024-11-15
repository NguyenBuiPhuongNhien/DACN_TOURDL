using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFHinhanhRepository : IHinhanhRepository
    {
        private readonly DoAnContext _context;

        public EFHinhanhRepository(DoAnContext context)
        {
            _context = context;
        }

        
        public async Task<IEnumerable<Hinhanh>> GetAllAsync()
        {
            return await _context.Hinhanhs.ToListAsync();
        }

        public async Task<Hinhanh> GetByIdAsync(int mah)
        {
            return await _context.Hinhanhs
                .Include(h => h.MadlNavigation)
                .FirstOrDefaultAsync(h => h.Mah == mah);
        }

        public async Task AddAsync(Hinhanh hinhanh)
        {
            _context.Hinhanhs.Add(hinhanh);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hinhanh hinhanh)
        {
            _context.Hinhanhs.Update(hinhanh);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int mah)
        {
            var hinhanh = await _context.Hinhanhs.FindAsync(mah);
            if (hinhanh != null)
            {
                _context.Hinhanhs.Remove(hinhanh);
                await _context.SaveChangesAsync();
            }
        }
    }
}

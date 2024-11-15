using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DO_AN.Models;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.Repositories
{
    public class EFCtddRepository : ICtddRepository
    {
        private readonly DoAnContext _context;

        public EFCtddRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ctdd>> GetAllAsync()
        {
            return await _context.Ctdds
                .Include(c => c.MadtqNavigation)
                .Include(c => c.MaksNavigation)
                .Include(c => c.MaltNavigation)
                .ToListAsync();
        }

        public async Task<Ctdd> GetByIdAsync(int malt, int maks, int madtq)
        {
            return await _context.Ctdds
                .Include(c => c.MadtqNavigation)
                .Include(c => c.MaksNavigation)
                .Include(c => c.MaltNavigation)
                .FirstOrDefaultAsync(c => c.Malt == malt && c.Maks == maks && c.Madtq == madtq);
        }

        public async Task AddAsync(Ctdd ctdd)
        {
            _context.Ctdds.Add(ctdd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ctdd ctdd)
        {
            _context.Ctdds.Update(ctdd);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int malt, int maks, int madtq)
        {
            var ctdd = await _context.Ctdds.FindAsync(malt, maks, madtq);
            if (ctdd != null)
            {
                _context.Ctdds.Remove(ctdd);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Ctdd>> GetallMaltAsync(int malt)
        {
            return await _context.Ctdds
                .Include(c => c.MadtqNavigation)
                .Include(c => c.MaksNavigation)
                .Include(c => c.MaltNavigation)
                .Where(c => c.Malt == malt)
                .ToListAsync();
        }
    }
}

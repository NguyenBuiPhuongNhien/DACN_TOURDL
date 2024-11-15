using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFCtptRepository : ICtptRepository
    {
        private readonly DoAnContext _context;

        public EFCtptRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ctpt>> GetAllAsync()
        {
            return await _context.Ctpts
                .Include(c => c.MaptNavigation)
                .Include(c => c.MatourNavigation)
                .ToListAsync();
        }

        public async Task<Ctpt> GetByIdAsync(int matour, int mapt)
        {
            return await _context.Ctpts
                .Include(c => c.MaptNavigation)
                .Include(c => c.MatourNavigation)
                .FirstOrDefaultAsync(c => c.Matour == matour && c.Mapt == mapt);
        }

        public async Task AddAsync(Ctpt ctpt)
        {
            _context.Ctpts.Add(ctpt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ctpt ctpt)
        {
            _context.Ctpts.Update(ctpt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int matour, int mapt)
        {
            var ctpt = await _context.Ctpts.FindAsync(matour, mapt);
            if (ctpt != null)
            {
                _context.Ctpts.Remove(ctpt);
                await _context.SaveChangesAsync();
            }
        }
    }
}

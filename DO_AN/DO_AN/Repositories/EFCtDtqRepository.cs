using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DO_AN.Models;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.Repositories
{
    public class EFCtDtqRepository : ICtDtqRepository
    {
        private readonly DoAnContext _context;
        private readonly ITourRepository _tourRepository;

        public EFCtDtqRepository(DoAnContext context, ITourRepository tourRepository)
        {
            _context = context;
            _tourRepository = tourRepository;
        }

        public async Task<IEnumerable<CtDtq>> GetAllAsync()
        {
            return await _context.CtDtqs
                .Include(c => c.MadlNavigation)
                .Include(c => c.MadtqNavigation)
                .ToListAsync();
        }

        public async Task<CtDtq> GetByIdAsync(int madtq, int madl)
        {
            return await _context.CtDtqs
                .Include(c => c.MadlNavigation)
                .Include(c => c.MadtqNavigation)
                .FirstOrDefaultAsync(c => c.Madtq == madtq && c.Madl == madl);
        }

        public async Task AddAsync(CtDtq ctdtq)
        {
            _context.CtDtqs.Add(ctdtq);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CtDtq ctdtq)
        {
            _context.CtDtqs.Update(ctdtq);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int madtq, int madl)
        {
            var ctdtq = await _context.CtDtqs.FindAsync(madtq, madl);
            if (ctdtq != null)
            {
                _context.CtDtqs.Remove(ctdtq);
                await _context.SaveChangesAsync();
            }
        }

    }
}

using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFDanhlamtcRepository : IDanhlamtcRepository
    {
        private readonly DoAnContext _context;

        public EFDanhlamtcRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Danhlamtc>> GetAllAsync()
        {
            return await _context.Danhlamtcs
                .Include(dl => dl.CtDtqs)
                .Include(dl => dl.Hinhanhs)
                .ToListAsync();
        }

        public async Task<Danhlamtc> GetByIdAsync(int madl)
        {
            return await _context.Danhlamtcs
                .Include(dl => dl.CtDtqs)
                .Include(dl => dl.Hinhanhs)
                .FirstOrDefaultAsync(dl => dl.Madl == madl);
        }

        public async Task AddAsync(Danhlamtc danhlamtc)
        {
            _context.Danhlamtcs.Add(danhlamtc);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Danhlamtc danhlamtc)
        {
            _context.Danhlamtcs.Update(danhlamtc);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int madl)
        {
            var danhlamtc = await _context.Danhlamtcs.FindAsync(madl);
            if (danhlamtc != null)
            {
                _context.Danhlamtcs.Remove(danhlamtc);
                await _context.SaveChangesAsync();
            }
        }
    }
}

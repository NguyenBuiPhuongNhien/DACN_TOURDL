using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFLichtrinhRepository : ILichtrinhRepository
    {
        private readonly DoAnContext _context;

        public EFLichtrinhRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lichtrinh>> GetAllAsync()
        {
            return await _context.Lichtrinhs.ToListAsync();
        }

        public async Task<Lichtrinh> GetByIdAsync(int malt)
        {
            return await _context.Lichtrinhs
                .Include(lt => lt.Ctdds)
                .Include(lt => lt.Tours)
                .FirstOrDefaultAsync(lt => lt.Malt == malt);
        }

        public async Task AddAsync(Lichtrinh lichtrinh)
        {
            _context.Lichtrinhs.Add(lichtrinh);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lichtrinh lichtrinh)
        {
            _context.Lichtrinhs.Update(lichtrinh);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int malt)
        {
            var lichtrinh = await _context.Lichtrinhs.FindAsync(malt);
            if (lichtrinh != null)
            {
                _context.Lichtrinhs.Remove(lichtrinh);
                await _context.SaveChangesAsync();
            }
        }
    }
}

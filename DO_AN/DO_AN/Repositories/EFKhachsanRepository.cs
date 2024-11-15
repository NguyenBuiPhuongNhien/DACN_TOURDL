using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFKhachsanRepository : IKhachsanRepository
    {
        private readonly DoAnContext _context;

        public EFKhachsanRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Khachsan khachsan)
        {
            _context.Khachsans.Add(khachsan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int maks)
        {
            var khachsan = await _context.Khachsans.FindAsync(maks);
            if (khachsan != null)
            {
                _context.Khachsans.Remove(khachsan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Khachsan>> GetAllAsync()
        {
            return await _context.Khachsans.ToListAsync();
        }

        public async Task<Khachsan> GetByIdAsync(int maks)
        {
            return await _context.Khachsans
                .Include(k => k.Ctdds)
                .FirstOrDefaultAsync(k => k.Maks == maks);
        }

        public async Task UpdateAsync(Khachsan khachsan)
        {
            _context.Khachsans.Update(khachsan);
            await _context.SaveChangesAsync();
        }
    }
}

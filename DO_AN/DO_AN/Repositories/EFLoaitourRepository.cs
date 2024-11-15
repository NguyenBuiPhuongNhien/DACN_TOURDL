using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFLoaitourRepository : ILoaitourRepository
    {
        private readonly DoAnContext _context;

        public EFLoaitourRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Loaitour loaitour)
        {
            _context.Loaitours.Add(loaitour);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int maloai)
        {
            var loaitour = await _context.Loaitours.FindAsync(maloai);
            if (loaitour != null)
            {
                _context.Loaitours.Remove(loaitour);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Loaitour>> GetAllAsync()
        {
            return await _context.Loaitours.ToListAsync();
        }

        public async Task<Loaitour> GetByIdAsync(int maloai)
        {
            return await _context.Loaitours
                .Include(l => l.Tours)
                .FirstOrDefaultAsync(l => l.Maloai == maloai);
        }

        public async Task UpdateAsync(Loaitour loaitour)
        {
            _context.Loaitours.Update(loaitour);
            await _context.SaveChangesAsync();
        }
    }
}

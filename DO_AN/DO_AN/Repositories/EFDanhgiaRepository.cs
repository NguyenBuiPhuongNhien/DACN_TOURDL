using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DO_AN.Models;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.Repositories
{
    public class EFDanhgiaRepository : IDanhgiaRepository
    {
        private readonly DoAnContext _context;

        public EFDanhgiaRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Danhgia>> GetAllAsync()
        {
            return await _context.Danhgia
                .Include(d => d.MakhNavigation)
                .Include(d => d.MatourNavigation)
                .ToListAsync();
        }

        public async Task<Danhgia> GetByIdAsync(int makh, int matour)
        {
            return await _context.Danhgia
                .Include(d => d.MakhNavigation)
                .Include(d => d.MatourNavigation)
                .FirstOrDefaultAsync(d => d.Makh == makh && d.Matour == matour);
        }

        public async Task AddAsync(Danhgia danhgia)
        {
            _context.Danhgia.Add(danhgia);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Danhgia danhgia)
        {
            _context.Danhgia.Update(danhgia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int makh, int matour)
        {
            var danhgia = await _context.Danhgia.FindAsync(makh, matour);
            if (danhgia != null)
            {
                _context.Danhgia.Remove(danhgia);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFKhuyenmaiRepository : IKhuyenmaiRepository
    {
        private readonly DoAnContext _context;

        public EFKhuyenmaiRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Khuyenmai>> GetAllAsync()
        {
            return await _context.Khuyenmais.ToListAsync();
        }

        public async Task<Khuyenmai> GetByIdAsync(string makm)
        {
            return await _context.Khuyenmais
                .Include(km => km.Phieudattours)
                .FirstOrDefaultAsync(km => km.Makm == makm);
        }

        public async Task AddAsync(Khuyenmai khuyenmai)
        {
            _context.Khuyenmais.Add(khuyenmai);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Khuyenmai khuyenmai)
        {
            _context.Khuyenmais.Update(khuyenmai);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string makm)
        {
            var khuyenmai = await _context.Khuyenmais.FindAsync(makm);
            if (khuyenmai != null)
            {
                _context.Khuyenmais.Remove(khuyenmai);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFDiemthamquanRepository : IDiemthamquanRepository
    {
        private readonly DoAnContext _context;

        public EFDiemthamquanRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Diemthamquan diemthamquan)
        {
            _context.Diemthamquans.Add(diemthamquan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int madtq)
        {
            var diemthamquan = await _context.Diemthamquans.FindAsync(madtq);
            if (diemthamquan != null)
            {
                _context.Diemthamquans.Remove(diemthamquan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Diemthamquan>> GetAllAsync()
        {
            return await _context.Diemthamquans.ToListAsync();
        }

        public async Task<Diemthamquan> GetByIdAsync(int madtq)
        {
            return await _context.Diemthamquans
                .Include(d => d.CtDtqs)
                .Include(d => d.Ctdds)
                .FirstOrDefaultAsync(d => d.Madtq == madtq);
        }

        public async Task UpdateAsync(Diemthamquan diemthamquan)
        {
            _context.Diemthamquans.Update(diemthamquan);
            await _context.SaveChangesAsync();
        }
    }
}

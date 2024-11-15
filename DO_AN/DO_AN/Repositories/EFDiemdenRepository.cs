using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFDiemdenRepository : IDiemdenRepository
    {
        private readonly DoAnContext _context;

        public EFDiemdenRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Diemden>> GetAllAsync()
        {
            return await _context.Diemdens
                .Include(dd => dd.Diemthamquans)
                .Include(dd => dd.Khachsans)
                .ToListAsync();
        }

        public async Task<Diemden> GetByIdAsync(int madd)
        {
            return await _context.Diemdens
                .Include(dd => dd.Diemthamquans)
                .Include(dd => dd.Khachsans)
                .FirstOrDefaultAsync(dd => dd.Madd == madd);
        }

        public async Task AddAsync(Diemden diemden)
        {
            _context.Diemdens.Add(diemden);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Diemden diemden)
        {
            _context.Diemdens.Update(diemden);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int madd)
        {
            var diemden = await _context.Diemdens.FindAsync(madd);
            if (diemden != null)
            {
                _context.Diemdens.Remove(diemden);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFNhanvienRepository : INhanvienRepository
    {
        private readonly DoAnContext _context;

        public EFNhanvienRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nhanvien>> GetAllAsync()
        {
            return await _context.Nhanviens.ToListAsync();
        }

        public async Task<Nhanvien> GetByIdAsync(int manv)
        {
            return await _context.Nhanviens
                .Include(nv => nv.Huongdans)
                .FirstOrDefaultAsync(nv => nv.Manv == manv);
        }

        public async Task AddAsync(Nhanvien nhanvien)
        {
            _context.Nhanviens.Add(nhanvien);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Nhanvien nhanvien)
        {
            _context.Nhanviens.Update(nhanvien);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int manv)
        {
            var nhanvien = await _context.Nhanviens.FindAsync(manv);
            if (nhanvien != null)
            {
                _context.Nhanviens.Remove(nhanvien);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Nhanvien> GetBySdtAsync(string sdt)
        {
            return await _context.Nhanviens
                .Include(nv => nv.Huongdans).FirstOrDefaultAsync(DT => DT.Sdt == sdt);
        }
    }
}

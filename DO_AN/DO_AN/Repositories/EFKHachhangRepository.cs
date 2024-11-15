using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFKHachhangRepository : IKhachhangRepository
    {
        private readonly DoAnContext _context;

        public EFKHachhangRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Khachhang khachhang)
        {
            _context.Khachhangs.Add(khachhang);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int makh)
        {
            var khachhang = await _context.Khachhangs.FindAsync(makh);
            if (khachhang != null)
            {
                _context.Khachhangs.Remove(khachhang);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Khachhang>> GetAllAsync()
        {
            return await _context.Khachhangs.ToListAsync();
        }

        public async Task<Khachhang> GetByIdAsync(int makh)
        {
            return await _context.Khachhangs
                .Include(k => k.Danhgia)
                .Include(k => k.Phieudattours)
                .FirstOrDefaultAsync(k => k.Makh == makh);
        }

        public async Task UpdateAsync(Khachhang khachhang)
        {
            _context.Khachhangs.Update(khachhang);
            await _context.SaveChangesAsync();
        }
        public async Task<Khachhang> GetBySdtAsync(string sdt)
        {
            return await _context.Khachhangs
                .Include(k => k.Danhgia)
                .Include(k => k.Phieudattours)
                .FirstOrDefaultAsync(k => k.Sdt == sdt);
        }

    }
}

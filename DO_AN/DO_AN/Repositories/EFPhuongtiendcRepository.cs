    using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFPhuongtiendcRepository : IPhuongtiendcRepository
    {
        private readonly DoAnContext _context;

        public EFPhuongtiendcRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Phuongtiendc phuongtiendc)
        {
            _context.Phuongtiendcs.Add(phuongtiendc);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int mapt)
        {
            var phuongtiendc = await _context.Phuongtiendcs.FindAsync(mapt);
            if (phuongtiendc != null)
            {
                _context.Phuongtiendcs.Remove(phuongtiendc);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Phuongtiendc>> GetAllAsync()
        {
            return await _context.Phuongtiendcs.ToListAsync();
        }

        public async Task<Phuongtiendc> GetByIdAsync(int mapt)
        {
            return await _context.Phuongtiendcs
                .Include(p => p.Ctpts)
                .FirstOrDefaultAsync(p => p.Mapt == mapt);
        }

        public async Task UpdateAsync(Phuongtiendc phuongtiendc)
        {
            _context.Phuongtiendcs.Update(phuongtiendc);
            await _context.SaveChangesAsync();
        }
    }
}

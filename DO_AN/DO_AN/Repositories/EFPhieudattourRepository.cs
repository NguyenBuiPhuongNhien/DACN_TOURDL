using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DO_AN.Models;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.Repositories
{
    public class EFPhieudattourRepository : IPhieudattourRepository
    {
        private readonly DoAnContext _context;

        public EFPhieudattourRepository(DoAnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Phieudattour>> GetAllAsync()
        {
            return await _context.Phieudattours
                .Include(p => p.MakhNavigation)
                .Include(p => p.MakmNavigation)
                .Include(p => p.MatourNavigation)
                .ToListAsync();
        }

        public async Task<Phieudattour> GetByIdAsync(int mapdt)
        {
            return await _context.Phieudattours
                .Include(p => p.MakhNavigation)
                .Include(p => p.MakmNavigation)
                .Include(p => p.MatourNavigation)
                .FirstOrDefaultAsync(p => p.Mapdt == mapdt);
        }

        public async Task AddAsync(Phieudattour phieudattour)
        {
            _context.Phieudattours.Add(phieudattour);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Phieudattour phieudattour)
        {
            var existingEntity = await _context.Phieudattours.FindAsync(phieudattour.Mapdt);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Phieudattours.Update(phieudattour);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int mapdt)
        {
            var phieudattour = await _context.Phieudattours.FindAsync(mapdt);
            if (phieudattour != null)
            {
                _context.Phieudattours.Remove(phieudattour);
                await _context.SaveChangesAsync();
            }
        }

        

        public async Task UpdateSLTour(int matour)
        {

            var tour = await _context.Tours.FindAsync(matour);
            tour.Sochodadat = 0;
            foreach(var t in _context.Phieudattours)
            {
                if(t.Matour == matour)
                {
                    tour.Sochodadat += t.Song;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSLTourDaDat(int makh)
        {
            var khachhang = await _context.Khachhangs.FindAsync(makh);
            khachhang.Sotourdadat = 0;
            foreach(var t in _context.Phieudattours)
            {
                if(t.Makh == makh)
                {
                    khachhang.Sotourdadat += 1;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}

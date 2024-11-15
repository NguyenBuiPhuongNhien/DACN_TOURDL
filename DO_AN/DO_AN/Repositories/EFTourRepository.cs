using DO_AN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DO_AN.Repositories
{
    public class EFTourRepository : ITourRepository
    {
        private readonly DoAnContext _context;
        private readonly IDiemdenRepository _diemdenRepository;

        public EFTourRepository(DoAnContext context, IDiemdenRepository diemdenRepository)
        {
            _context = context;
            _diemdenRepository = diemdenRepository;
        }

        public async Task<IEnumerable<Tour>> GetAllAsync()
        {
            return await _context.Tours
                .Include(t => t.MadkhNavigation)
                .Include(t => t.MaloaiNavigation)
                .Include(t => t.MaltNavigation)
                .ToListAsync();
        }

        public async Task<Tour> GetByIdAsync(int matour)
        {
            return await _context.Tours
                .Include(t => t.Ctpts)
                .Include(t => t.Danhgia)
                .Include(t => t.Huongdans)
                .Include(t => t.Phieudattours)
                .Include(t => t.MadkhNavigation)
                .Include(t => t.MaloaiNavigation)
                .Include(t => t.MaltNavigation)
                .FirstOrDefaultAsync(t => t.Matour == matour);
        }

        public async Task AddAsync(Tour tour)
        {
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tour tour)
        {
            var existingTour = await _context.Tours.FindAsync(tour.Matour);
            if (existingTour != null)
            {
                _context.Entry(existingTour).State = EntityState.Detached; // Tách đối tượng cũ khỏi context
            }

            _context.Tours.Update(tour); // Thêm đối tượng mới vào context
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int matour)
        {
            var tour = await _context.Tours.FindAsync(matour);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Tour>> GetToursByDestinationAsync(int madd)
        {
            return await _context.Tours
                .Include(t => t.MaltNavigation) // Bao gồm thông tin lịch trình
                .ThenInclude(lt => lt.Ctdds) // Bao gồm thông tin chi tiết điểm đến trong lịch trình
                .Where(t => t.MaltNavigation.Ctdds.Any(ctdd => ctdd.MadtqNavigation.Madd == madd))
                .ToListAsync();
        }
        public async Task<IEnumerable<Tour>> SearchByNameAsync(string query)
        {
            return await _context.Tours
                .Include(t => t.Ctpts)
                .Include(t => t.Danhgia)
                .Include(t => t.Huongdans)
                .Include(t => t.Phieudattours)
                .Include(t => t.MadkhNavigation)
                .Include(t => t.MaloaiNavigation)
                .Include(t => t.MaltNavigation)
                .Where(t => t.Tentour.Contains(query))
                .ToListAsync();
        }



        public async Task<IEnumerable<Tour>> GetTourInDiemthamquan(int madtq)
        {
            var tours = await _context.Tours
                .Include(t => t.MaltNavigation)
                    .ThenInclude(lt => lt.Ctdds)
                .Where(t => t.MaltNavigation.Ctdds
                    .Any(ctdd => ctdd.Madtq == madtq))
                .ToListAsync();

            return tours;
        }

        public async Task<IEnumerable<Tour>> GetTourInDiemden(int madd)
        {
            var tours = await _context.Tours
       .Include(t => t.MaltNavigation)
           .ThenInclude(lt => lt.Ctdds)
               .ThenInclude(ctdd => ctdd.MadtqNavigation)
                   .ThenInclude(dtq => dtq.MaddNavigation)
       .Where(t => t.MaltNavigation.Ctdds
           .Any(ctdd => ctdd.MadtqNavigation.Madd == madd))
       .ToListAsync();
            return tours;
        }

        public async Task<IEnumerable<Tour>> GetTourInloaitour(int maloai)
        {
            var tours = await _context.Tours.Where(Maloai => Maloai.Maloai == maloai).ToListAsync();
            return tours;
        }
        public async Task<IEnumerable<Tour>> GetTourInDiemkhoihanh(int madkh)
        {
            var tours = await _context.Tours.Where(DKH => DKH.Madkh == madkh).ToListAsync();
            return tours;
        }
        public async Task<IEnumerable<Tour>> GetTourInDanhlam(int madl)
        {
            var tours = await _context.Tours
       .Include(t => t.MaltNavigation)
           .ThenInclude(lt => lt.Ctdds)
               .ThenInclude(ctdd => ctdd.MadtqNavigation).ThenInclude(ctdtq => ctdtq.CtDtqs).ThenInclude(dl => dl.MadlNavigation).Where(t=>t.MaltNavigation.Ctdds.Any(danhlam => danhlam.MadtqNavigation.CtDtqs.Any(c=>c.Madl == madl))).ToArrayAsync();       
            return tours;
        }
    }
}

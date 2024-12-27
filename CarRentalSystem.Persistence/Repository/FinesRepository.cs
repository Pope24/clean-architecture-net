using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Persistence.Repository
{
    public class FinesRepository : IFinesRepository
    {
        private readonly ApplicationDbContext _context;

        public FinesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public FinesRepository()
        {
            _context = new ApplicationDbContext();
        }
        public async Task<bool> AddFinesRange(List<FinesEntity> finesEntities)
        {
            await _context.Fines.AddRangeAsync(finesEntities);
            int colEffected = await _context.SaveChangesAsync();
            return colEffected > 0 ? true : false;
        }

        public async Task<List<FinesEntity>> GetAllFines()
        {
            var fines = await _context.Fines.ToListAsync();
            return fines;
        }

        public async Task<List<FinesEntity>> GetFinesByBookingId(Guid bookingId)
        {
            using (var _context = new ApplicationDbContext())
            {
                var finesOfBooking = await _context.Fines.Where(e => e.BookingId == bookingId).ToListAsync();
                return finesOfBooking;
            }
        }

        public async Task<bool> UpdateFinesRange(List<FinesEntity> finesEntities)
        {
            foreach (var fine in finesEntities)
            {
                _context.Entry(fine).State = EntityState.Modified;
                _context.Update(fine);
            }
            int colEffected = await _context.SaveChangesAsync();
            return colEffected > 0 ? true : false;
        }

        public async Task<bool> AddFines(FinesEntity finesEntity)
        {
            await _context.Fines.AddAsync(finesEntity);
            int colEffected = await _context.SaveChangesAsync();
            return colEffected > 0 ? true : false;
        }
    }
}

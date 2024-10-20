using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Persistence.Repository
{
    public class BookingRepository : IBookingRepository
    {
        public async Task<bool> AddAsync(BookingEntity entity)
        {
            using (var context = new ApplicationDbContext())
            {
                await context.Booking.AddAsync(entity);
                var res = await context.SaveChangesAsync();
                return res > 0 ? true : false;
            }
        }

        public async Task<IEnumerable<BookingEntity>> GetAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var bookings = await context.Booking
                    .ToListAsync();
                var check = await context.Booking.AnyAsync();
                await Console.Out.WriteLineAsync(check.ToString());
                return bookings;
            }
        }

        public async Task<BookingEntity> GetAsyncById(Guid id)
        {
            using (var context = new ApplicationDbContext())
            {
                var booking = await context.Booking.Where(b => b.Id == id).FirstOrDefaultAsync();
                return booking;
            }
        }
    }
}

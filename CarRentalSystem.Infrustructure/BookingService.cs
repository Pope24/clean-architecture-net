using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrustructure
{
    public class BookingService : IBookingService
    {
        private IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Task<IEnumerable<BookingEntity>> GetAsync()
        {
            return _bookingRepository.GetAsync();
        }

        public Task<BookingEntity> GetAsyncById(Guid id)
        {
            return _bookingRepository.GetAsyncById(id);
        }
    }
}

using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Service
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingEntity>> GetAsync();
        Task<BookingEntity> GetAsyncById(Guid id);
    }
}

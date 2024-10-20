using CarRentalSystem.Application.Bases;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
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
        Task<BaseResponse<BookingResponse>> AddBookingAsync(BookingRequest bookingRequest);
    }
}

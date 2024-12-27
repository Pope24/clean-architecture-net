using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrustructure
{
    public class AppraiseService : IAppraiseService
    {
        private readonly IBookingService _bookingService;

        public AppraiseService(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public AppraiseService()
        {
            _bookingService = new BookingService();
        }

        public async Task<BasePaging<BookingHistoryResponse>> GetAllRequestReturn(BaseFilteration filter)
        {
            var bookings = await _bookingService.GetAsync();

            var bookingResponseTasks = bookings
                .Where(b => b.RegisterReturnDate != null && b.BookingConfirmDate != null)
                .Select(b => _bookingService.ConvertBookingToBookingHistoryResponse(b));
            var bookingResponse = await Task.WhenAll(bookingResponseTasks);

            var paging = BasePaging<BookingHistoryResponse>.ToPagedList(bookingResponse.ToList(), filter.Page, 9);
            return paging;
        }

    }
}

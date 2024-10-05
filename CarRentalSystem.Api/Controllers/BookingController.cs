using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers
{
    [ApiController]
    public class BookingController: ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            this._bookingService = bookingService;
        }
        [HttpGet("bookings")]
        public async Task<IActionResult> GetAllAsync()
        {
            var bookings = await _bookingService.GetAsync();
            if (bookings.Any())
            {
                return Ok(bookings);
            }
            return NotFound();
        }
        [HttpGet("bookings/{bookingId:Guid}")]
        public async Task<IActionResult> GetAsyncById(Guid bookingId)
        {
            var booking = await _bookingService.GetAsyncById(bookingId);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
    }
}

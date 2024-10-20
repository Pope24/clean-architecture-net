using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IPaymentService _paymentService;

        public BookingController(IBookingService bookingService, IPaymentService paymentService)
        {
            this._bookingService = bookingService;
            this._paymentService = paymentService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var bookings = await _bookingService.GetAsync();
            if (bookings.Any())
            {
                return Ok(bookings);
            }
            return NotFound();
        }
        [HttpGet("{bookingId:Guid}")]
        public async Task<IActionResult> GetAsyncById(Guid bookingId)
        {
            var booking = await _bookingService.GetAsyncById(bookingId);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
        [HttpPost("add")]
        public async Task<BaseResponse<BookingResponse>> AddBookingAsync([FromBody] BookingRequest bookingRequest)
        {
            var bookingResponse = await _bookingService.AddBookingAsync(bookingRequest);
            var booking = bookingResponse.Data.BookingEntity;
            bookingResponse.Data.PaymentLink = _paymentService.CreatePaymentUrlByVNPay(
                new PaymentInforRequest(booking.Id, 10000000), HttpContext);
            return bookingResponse;
        }
    }
}

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
            var booking = await _bookingService.GetBookingHistoryAsyncById(bookingId);
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
                new PaymentInforRequest(booking.Id, bookingRequest.TotalAmount), HttpContext);
            return bookingResponse;
        }
        [HttpGet("booking-history/{userId:Guid}")]
        public async Task<BasePaging<BookingHistoryResponse>> GetBookingHistoryByUserId(Guid userId, [FromQuery] BaseFilteration filter)
        {
            var res = await _bookingService.GetBookingsHistoryByUserIdAsync(userId, filter);
            return res;
        }
        [HttpGet("create-payment-vnpay")]
        public async Task<BookingResponse> CreatePaymentVNPayForBookingAsync(Guid bookingId, decimal totalAmount)
        {
            var booking = await _bookingService.GetAsyncById(bookingId);
            var paymentLink = _paymentService.CreatePaymentUrlByVNPay(
                new PaymentInforRequest(bookingId, totalAmount), HttpContext);
            return new BookingResponse()
            {
                BookingEntity = booking,
                PaymentLink = paymentLink
            };
        }
        [HttpGet("register-return/{bookingId:Guid}")]
        public async Task<bool> RegisterReturnVehicleAsync(Guid bookingId)
        {
            var booking = await _bookingService.GetAsyncById(bookingId);
            booking.RegisterReturnDate = DateTime.Now.ToLocalTime();
            return await _bookingService.UpdateBookingAsync(booking);
        }
        [HttpGet("booking-approve")]
        public async Task<BasePaging<BookingHistoryResponse>> GetBookingNeedToApprove([FromQuery] BaseFilteration filter)
        {
            return await _bookingService.GetBookingNeedToApprove(filter);
        }
        [HttpGet("approve")]
        public async Task<bool> ApproveBooking(Guid bookingId)
        {
            return await _bookingService.ApproveBooking(bookingId);
        }
    }
}

using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Enum;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/fines")]
    public class FinesController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IPaymentService _paymentService;
        private readonly IFinesService _fineService;

        public FinesController(IBookingService bookingService, IPaymentService paymentService, IFinesService finesService)
        {
            this._bookingService = bookingService;
            this._paymentService = paymentService;
            this._fineService = finesService;
        }
        [HttpGet("payment-fines")]
        public async Task<BookingResponse> CreatePaymentVNPayForBookingAsync(Guid bookingId, decimal totalAmount)
        {
            var booking = await _bookingService.GetAsyncById(bookingId);
            var paymentLink = _paymentService.CreatePaymentForFines(
                new PaymentInforRequest(bookingId, totalAmount), HttpContext);
            return new BookingResponse()
            {
                BookingEntity = booking,
                PaymentLink = paymentLink
            };
        }
        [HttpGet("change-status")]
        public async Task<bool> ChangePaymentStatusFinesOfBooking(Guid bookingId, EPaymentStatus paymentStatus)
        {
            return await _fineService.ChangePaymentStatus(bookingId, paymentStatus);
        }
    }
}

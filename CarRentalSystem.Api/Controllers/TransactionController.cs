using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/transactions")]
    public class TransactionController: ControllerBase
    {
        private ITransactionService _transactionService;
        private IBookingService _bookingService;

        public TransactionController(ITransactionService transactionService, IBookingService bookingService)
        {
            _transactionService = transactionService;
            _bookingService = bookingService;
        }

        [HttpPost("add")]
        public async Task<BaseResponse<TransactionEntity>> SaveTransactionAsync([FromBody] TransactionRequest transactionRequest)
        {
            var res = await _transactionService.AddAsync(transactionRequest);
            await _bookingService.UpdateBookingAfterPaymentAsync(transactionRequest.BookingId);
            return res;
        }
    }
}

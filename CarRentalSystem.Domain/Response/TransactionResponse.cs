using CarRentalSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Response
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string? ErrorCode { get; set; }
        public string TransactionNo { get; set; }
        public decimal Amount { get; set; }
        public string PaymentFor { get; set; }
        public string PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public Guid? BookingId { get; set; }
        public List<string>? FinesIds { get; set; }
        public DateTime Created { get; set; }
    }
}

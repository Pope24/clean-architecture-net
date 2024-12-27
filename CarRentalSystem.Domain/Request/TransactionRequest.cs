using CarRentalSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Request
{
    public class TransactionRequest
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string ResponseCode { get; set; }
        public EPaymentFor PaymentFor { get; set; }
    }
}

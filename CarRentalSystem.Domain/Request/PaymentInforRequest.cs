using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Request
{
    public class PaymentInforRequest
    {
        public Guid BookingId { get; set; }
        public double Amount { get; set; }

        public PaymentInforRequest(Guid bookingId, double amount)
        {
            BookingId = bookingId;
            Amount = amount;
        }
    }
}

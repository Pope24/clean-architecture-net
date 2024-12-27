using CarRentalSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Request
{
    public class BookingRequest
    {
        public Guid UserId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid? CouponId { get; set; }
        public EBookingType BookingType { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string? ReturnAddress { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

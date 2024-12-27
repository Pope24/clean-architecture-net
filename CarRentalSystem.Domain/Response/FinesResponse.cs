using CarRentalSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Response
{
    public class FinesResponse
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public EFineReason FinesReason { get; set; }
        public DateTime FinesDate { get; set; }
        public EPaymentStatus PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now.ToLocalTime();
    }
}

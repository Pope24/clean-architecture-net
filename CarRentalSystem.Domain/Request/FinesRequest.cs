using CarRentalSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Request
{
    public class FinesRequest
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public EFineReason FinesReason { get; set; }
    }
}

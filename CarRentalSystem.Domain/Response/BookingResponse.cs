using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Response
{
    public class BookingResponse
    {
        public BookingEntity BookingEntity { get; set; }
        public string? PaymentLink { get; set; }
    }
}

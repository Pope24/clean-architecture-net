using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Request
{
    public class CheckAvailableRequest
    {
        public Guid VehicleId { get; set; }
        public DateTime DateOfBooking { get; set; }
        public DateTime DateOfReturn { get; set; }
    }
}

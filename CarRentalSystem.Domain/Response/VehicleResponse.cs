using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Enum;
using Crs.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Response
{
    public class VehicleResponse
    {
        public Guid Id { get; set; }
        public string VehicleCode { get; set; }
        public string VehicleName { get; set; }
        public int NumberOfSeats { get; set; }
        public string TransmissionSystem { get; set; }
        public string Fuel { get; set; }
        public int Consumption { get; set; }
        public List<string>? Amenities { get; set; } = new();
        public string Description { get; set; }
        public string CurrentAddress { get; set; }
        public decimal ADayRentalPrice { get; set; }
        public decimal? VATFee { get; set; }
        public decimal? ReservationFee { get; set; }
        public decimal? DepositFee { get; set; }
        public List<string> Images { get; set; } = new();
        public string Created { get; set; }
        public LegacyDataExtension DataExtension { get; set; }
    }
}

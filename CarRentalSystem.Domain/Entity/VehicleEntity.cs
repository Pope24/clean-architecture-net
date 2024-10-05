using CarRentalSystem.Domain.Enum;
using Crs.Domain.Entity;

namespace CarRentalSystem.Domain.Entity
{
    public class VehicleEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string VehicleCode { get; set; }
        public string VehicleName { get; set; }
        public VehicleEngineProperty VehicleEngineProperty { get; set; }
        public VehicleAmenityProperty VehicleAmenityProperty { get; set; }
        public string Description { get; set; }
        public string CurrentAddress { get; set; }
        public decimal ADayRentalPrice { get; set; }
        public AdditionalFeeProperty AdditionalFeeProperty { get; set; }
        public VehicleImageProperty VehicleImageProperty { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string TextSearch { get; set; }
        public LegacyDataExtension DataExtension { get; set; }
        public ICollection<BookingEntity> Bookings { get; } = new List<BookingEntity>();
    }

    public class VehicleEngineProperty
    {
        public Guid OwnerId { get; set; }
        public int NumberOfSeats { get; set; }
        public ETransmissionSystem TransmissionSystem { get; set; }
        public EFuel Fuel { get; set; }
        public int Consumption { get; set; }
    }

    public class VehicleAmenityProperty
    {
        public List<string>? Amenities { get; set; } = new();
    }

    public class AdditionalFeeProperty
    {
        public Guid OwnerId { get; set; }
        public decimal? VATFee { get; set; }
        public decimal? ReservationFee { get; set; }
        public decimal? DepositFee { get; set; }
    }

    public class VehicleImageProperty
    {
        public List<string> Images { get; set; } = new();
    }
}
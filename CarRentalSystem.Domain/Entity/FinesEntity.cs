using CarRentalSystem.Domain.Enum;
using Crs.Domain.Entity;

namespace CarRentalSystem.Domain.Entity
{
    public class FinesEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public EFineReason FinesReason { get; set; }
        public DateTime FinesDate { get; set; }
        public EPaymentStatus PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now.ToLocalTime();
        public string? TextSearch { get; set; }
        public LegacyDataExtension? DataExtension { get; set; }
    }
}

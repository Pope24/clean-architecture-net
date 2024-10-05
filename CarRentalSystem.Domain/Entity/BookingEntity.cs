using CarRentalSystem.Domain.Enum;
using Crs.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entity
{
    public class BookingEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BookingNumber { get; set; }
        public Guid? UserId { get; set; }
        public Guid? VehicleId { get; set; }
        public Guid? CouponId { get; set; }
        public UserEntity User {  get; set; }
        public VehicleEntity Vehicle { get; set; }
        public CouponEntity Coupon { get; set; }
        public EBookingType BookingType { get; set; }
        public DateTime BookingConfirmDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public EPaymentStatus? PaymentStatus { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string TextSearch { get; set; }
        public LegacyDataExtension DataExtension { get; set; }
    }
}

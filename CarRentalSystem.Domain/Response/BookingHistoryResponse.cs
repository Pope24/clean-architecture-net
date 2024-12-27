using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Response
{
    public class BookingHistoryResponse
    {
        public Guid Id { get; set; }
        public string BookingNumber { get; set; }
        public Guid UserId { get; set; }
        public Guid? VehicleId { get; set; }
        public Guid? CouponId { get; set; }
        public UserEntity? User { get; set; }
        public VehicleEntity? Vehicle { get; set; }
        public CouponEntity? Coupon { get; set; }
        public string BookingType { get; set; }
        public DateTime? BookingConfirmDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? RegisterReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? ReturnAddress { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public List<TransactionResponse>? TransactionHistory { get; set; }
        public List<FinesEntity>? Fines { get; set; }
    }
}

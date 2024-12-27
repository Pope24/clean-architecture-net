using CarRentalSystem.Domain.Enum;
using Crs.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entity
{
    public class TransactionEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public EPaymentStatus Status { get; set; }
        public string? ErrorCode { get; set; }
        public string TransactionNo { get; set; }
        public decimal Amount { get; set; }
        public EPaymentFor PaymentFor { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public Guid? BookingId { get; set; }
        public List<string>? FinesIds { get; set; }
        public DateTime Created { get; set; } = DateTime.Now.ToLocalTime();
        public string TextSearch { get; set; }
        public LegacyDataExtension DataExtension { get; set; }
    }
}

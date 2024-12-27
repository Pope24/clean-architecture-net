using CarRentalSystem.Domain.Enum;
using Crs.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entity
{
    public class CouponEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsActivate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public EDiscountType DiscountType { get; set; }
        public int UsageLimit { get; set; }
        public DateTime Created { get; set; } = DateTime.Now.ToLocalTime();
        public string TextSearch { get; set; }
        public LegacyDataExtension DataExtension { get; set; }
        public ICollection<BookingEntity> Bookings { get; } = new List<BookingEntity>();
    }
}

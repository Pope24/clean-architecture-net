using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Enum
{
    public enum EPaymentStatus
    {
        Pending = 100,
        Paid = 101,
        Unpaid = 102,
        Failed = 103,
        PartiallyPaid = 104,
        Canceled = 105
    }
    public enum EPaymentMethod
    {
        CASH = 0,
        VNPAY = 1,
        PAYPAL = 2,
    }
    public enum EPaymentFor
    {
        Booking = 100,
        Fines = 200,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Enum
{
    public enum EFineReason
    {
        LateReturn = 1,
        Cancellation = 3,
        MakingDamage = 5,
        Cleaning = 7,
        TrafficViolation = 9
    }
}

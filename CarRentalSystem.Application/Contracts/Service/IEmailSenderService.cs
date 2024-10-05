using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Service
{
    public interface IEmailSenderService
    {
        void SendEmail(UserEntity user, EContentEmail contentEmail);
    }
}

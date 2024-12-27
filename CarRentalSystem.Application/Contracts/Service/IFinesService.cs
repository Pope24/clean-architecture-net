using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Enum;
using CarRentalSystem.Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Service
{
    public interface IFinesService
    {
        Task<List<FinesEntity>> GetAllFines();
        Task<bool> AddFinesRange(List<FinesRequest> finesEntities);
        Task<bool> AddFines(FinesRequest finesEntity);
        Task<List<FinesEntity>> GetFinesByBookingId(Guid bookingId);
        FinesEntity ConvertRequestToEntity(FinesRequest finesRequest);
        Task<bool> ChangePaymentStatus(Guid bookingId, EPaymentStatus paymentStatus);
    }
}

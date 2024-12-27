using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Repository
{
    public interface IFinesRepository
    {
        Task<List<FinesEntity>> GetAllFines();
        Task<bool> AddFinesRange(List<FinesEntity> finesEntities);
        Task<bool> AddFines(FinesEntity finesEntity);
        Task<List<FinesEntity>> GetFinesByBookingId(Guid bookingId);
        Task<bool> UpdateFinesRange(List<FinesEntity> finesEntities);
    }
}

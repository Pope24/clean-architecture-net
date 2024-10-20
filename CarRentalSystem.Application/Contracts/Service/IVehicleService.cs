using CarRentalSystem.Application.Bases;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Service
{
    public interface IVehicleService
    {
        Task<BasePaging<VehicleResponse>> GetAsync(BaseFilteration filter);
        Task<VehicleResponse> GetAsyncById(Guid id);
        Task<bool> CheckAvailableVehicle(CheckAvailableRequest availableRequest);
    }
}

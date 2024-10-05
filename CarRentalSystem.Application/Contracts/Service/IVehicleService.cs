using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Service
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleEntity>> GetAsync();
        Task<VehicleEntity> GetAsyncById(Guid id);
    }
}

using CarRentalSystem.Application.Bases;
using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Repository
{
    public interface IVehicleRepository
    {
        Task<List<VehicleEntity>> GetAsync();
        Task<VehicleEntity> GetAsyncById(Guid id);
    }
}

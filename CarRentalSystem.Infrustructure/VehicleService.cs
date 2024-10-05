using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrustructure
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            this._vehicleRepository = vehicleRepository;
        }

        public Task<IEnumerable<VehicleEntity>> GetAsync()
        {
            return _vehicleRepository.GetAsync();
        }

        public Task<VehicleEntity> GetAsyncById(Guid id)
        {
            return _vehicleRepository.GetAsyncById(id);
        }
    }
}

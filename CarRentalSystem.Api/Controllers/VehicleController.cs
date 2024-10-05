using CarRentalSystem.Application.Contracts.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers
{
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet("vehicles")]
        public async Task<IActionResult> GetAllAsync() {
            var vehicles = await _vehicleRepository.GetAsync();
            if (vehicles.Count() == 0)
            {
                return NotFound();
            }
            return Ok(vehicles);
        }
        [HttpGet("vehicles/{vehicleId:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid vehicleId)
        {
            var vehicle = await _vehicleRepository.GetAsyncById(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }
    }
}

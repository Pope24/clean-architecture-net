using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync([FromQuery] BaseFilteration filter)
        {
            var vehicles = await _vehicleService.GetAsync(filter);
            if (vehicles.Items.Count() == 0)
            {
                return NotFound();
            }
            return Ok(vehicles);
        }
        [HttpGet("{vehicleId:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid vehicleId)
        {
            var vehicle = await _vehicleService.GetAsyncById(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }
        [HttpGet("check-available")]
        public async Task<bool> CheckAvailableVehicle([FromQuery] CheckAvailableRequest availableRequest)
        {
            return await _vehicleService.CheckAvailableVehicle(availableRequest);
        }
    }
}

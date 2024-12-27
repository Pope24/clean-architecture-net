using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Persistence.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public VehicleRepository() { 
            _context = new ApplicationDbContext();
        }
        public async Task<List<VehicleEntity>> GetAsync()
        {

            var vehicles = await _context.Vehicle
                .Include(v => v.AdditionalFeeProperty)
                .Include(v => v.VehicleAmenityProperty)
                .Include(v => v.VehicleImageProperty)
                .Include(v => v.VehicleEngineProperty)
                .Include(v => v.DataExtension)
                .ToListAsync();
            return vehicles;
        }

        public async Task<VehicleEntity> GetAsyncById(Guid id)
        {
            using (var context = new ApplicationDbContext())
            {
                var vehicle = await context.Vehicle.Where(s => s.Id == id).FirstOrDefaultAsync();
                return vehicle;
            }
        }
    }
}

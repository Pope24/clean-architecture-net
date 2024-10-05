using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Persistence.Repository
{
    public class VehicleRepository: IVehicleRepository
    {

        public async Task<IEnumerable<VehicleEntity>> GetAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var vehicles = await context.Vehicle
                    .Include(v => v.AdditionalFeeProperty)
                    .Include(v => v.VehicleAmenityProperty)
                    .Include(v => v.VehicleImageProperty)
                    .Include(v => v.VehicleEngineProperty)
                    .Include(v => v.DataExtension)
                    .ToListAsync();
                return vehicles;
            }
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

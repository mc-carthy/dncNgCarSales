using System.Threading.Tasks;
using dncNgCarSales.Models;
using Microsoft.EntityFrameworkCore;

namespace dncNgCarSales.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly SkeletonDbContext context;
        public VehicleRepository(SkeletonDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int id)
        {
            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }
    }
}
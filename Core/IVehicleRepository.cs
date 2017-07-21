using System.Threading.Tasks;
using dncNgCarSales.Core.Models;

namespace dncNgCarSales.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}
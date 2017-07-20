using System.Threading.Tasks;
using dncNgCarSales.Models;

namespace dncNgCarSales.Persistence
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id);
    }
}
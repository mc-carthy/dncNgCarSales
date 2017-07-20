using dncNgCarSales.Models;
using Microsoft.AspNetCore.Mvc;

namespace dncNgCarSales.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        [HttpPost()]
        public IActionResult CreateVehicle([FromBody] Vehicle vehicle)
        {
            return Ok(vehicle);
        }
    }
}
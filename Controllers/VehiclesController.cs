using System;
using System.Threading.Tasks;
using AutoMapper;
using dncNgCarSales.Controllers.Resources;
using dncNgCarSales.Models;
using dncNgCarSales.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace dncNgCarSales.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly SkeletonDbContext context;
        public VehiclesController(IMapper mapper, SkeletonDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }
    }
}
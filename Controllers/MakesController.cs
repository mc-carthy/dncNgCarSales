using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dncNgCarSales.Core.Models;
using dncNgCarSales.Persistence;
using dncNgCarSales.Controllers.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dncNgCarSales.Controllers
{
    public class MakesController : Controller
    {
        private readonly SkeletonDbContext context;
        private readonly IMapper mapper;

        public MakesController(SkeletonDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}
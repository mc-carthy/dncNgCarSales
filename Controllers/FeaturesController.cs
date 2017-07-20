using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dncNgCarSales.Controllers.Resources;
using dncNgCarSales.Models;
using dncNgCarSales.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dncNgCarSales.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly SkeletonDbContext context;
        private readonly IMapper mapper;

        public FeaturesController(SkeletonDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();
            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }
    }
}
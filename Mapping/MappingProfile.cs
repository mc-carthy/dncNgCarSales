using AutoMapper;
using dncNgCarSales.Controllers.Resources;
using dncNgCarSales.Models;

namespace dncNgCarSales.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
        }
    }
}
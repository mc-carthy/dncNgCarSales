using AutoMapper;
using dncNgCarSales.Controllers.Resources;
using dncNgCarSales.Models;
using System.Linq;

namespace dncNgCarSales.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain -> API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();

            // API Resource -> Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature { FeatureId = id})));
        }
    }
}
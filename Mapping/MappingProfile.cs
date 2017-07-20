using AutoMapper;
using dncNgCarSales.Controllers.Resources;
using dncNgCarSales.Models;
using System.Linq;
using System.Collections.Generic;

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
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(
                    v => new ContactResource 
                    { 
                        Name = v.ContactName,
                        Phone = v.ContactPhone,
                        Email = v.ContactEmail
                    }
                ))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(
                    v => v.Features.Select(vf => vf.FeatureId)
                ));

            // API Resource -> Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) => {
                    // Remove unselected features
                    var removedFeatures = new List<VehicleFeature>();
                    foreach (var f in v.Features)
                    {
                        if (!vr.Features.Contains(f.FeatureId))
                        {
                            removedFeatures.Add(f);
                        }
                    }
                    foreach (var f in removedFeatures)
                    {
                        v.Features.Remove(f);
                    }

                    // Add new features
                    foreach (var id in vr.Features)
                    {
                        if (!v.Features.Any(f => f.FeatureId == id))
                        {
                            v.Features.Add(new VehicleFeature { FeatureId = id });
                        }
                    }
                });
        }
    }
}
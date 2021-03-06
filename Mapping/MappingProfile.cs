using AutoMapper;
using dncNgCarSales.Controllers.Resources;
using dncNgCarSales.Core.Models;
using System.Linq;
using System.Collections.Generic;

namespace dncNgCarSales.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain -> API Resource
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
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
                
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(
                    v => new ContactResource 
                    { 
                        Name = v.ContactName,
                        Phone = v.ContactPhone,
                        Email = v.ContactEmail
                    }
                ))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(
                    v => v.Features.Select(vf => new Feature { Id = vf.Feature.Id, Name = vf.Feature.Name })
                ));    
                
            // API Resource -> Domain
            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) => {
                    // Remove unselected features
                    var removedFeatures = v.Features
                        .Where(f => !vr.Features
                        .Contains(f.FeatureId))
                        .ToList();

                    foreach (var f in removedFeatures)
                    {
                        v.Features.Remove(f);
                    }

                    // Add new features
                    var addedFeatures = vr.Features
                        .Where(id => !v.Features
                        .Any(f => f.FeatureId == id))
                        .Select(id => new VehicleFeature { FeatureId = id })
                        .ToList();   

                    foreach (var f in addedFeatures)
                    {
                        v.Features.Add(f);
                    }
                });
        }
    }
}
using AutoMapper;
using GC.Core.Entities;
using GC.Core.Querying;
using GC.Web.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace GC.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultDTO<>));

            CreateMap<Company, KeyValuePairDTO>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<Feature, KeyValuePairDTO>();
            CreateMap<IncludedFeature, IncludedFeatureFormDTO>()
                .ForMember(f => f.Features, opt => opt.MapFrom(v => v.Features.Select(f => new KeyValuePairDTO() { Id = f.Id, Name = f.Name })));
            CreateMap<Photo, PhotoDTO>();
            CreateMap<ProvidedService, ProvidedServiceListItemDTO>();
            CreateMap<ProvidedServiceQuery, ProvidedServiceQueryDTO>();
            CreateMap<ProvidedService, ProvidedServiceFormDTO>();
            CreateMap<ProvidedService, ProvidedServiceDetailsDTO>();

            CreateMap(typeof(QueryResultDTO<>), typeof(QueryResult<>));

            CreateMap<CompanyDTO, Company>();
            CreateMap<KeyValuePairDTO, Feature>();
            CreateMap<KeyValuePairDTO, Company>();
            CreateMap<IncludedFeatureFormDTO, IncludedFeature>();
            CreateMap<CompanyQueryDTO, CompanyQuery>();
            CreateMap<PhotoDTO, Photo>();
            CreateMap<ProvidedServiceQueryDTO, ProvidedServiceQuery>();

            CreateMap<ProvidedServiceDetailsDTO, ProvidedService>();


            CreateMap<ProvidedServiceFormDTO, ProvidedService>()
                .AfterMap((fromApi, fromDataBase) =>
                {
                    fromDataBase.IncludedFeatures.Where(c => !fromApi.IncludedFeatures.Select(s => s.Id).Contains(c.Id)).ToList()
                    .ForEach(toDelete => fromDataBase.IncludedFeatures.Remove(toDelete));

                    fromApi.IncludedFeatures.Where(c => !fromDataBase.IncludedFeatures.Select(s => s.Id).Contains(c.Id)).ToList()
                    .Select(feature => Mapper.Map<IncludedFeature>(feature)).ToList()
                    .ForEach(item => fromDataBase.IncludedFeatures.Add(item));

                });

            CreateMap<IncludedFeatureFormDTO, IncludedFeature>()
            .ForMember(f => f.Features, opt => opt.Ignore())
            .AfterMap((includedFeatureFormDTO, includedFeature) =>
            {
                
                includedFeature.Features
                 .Where(f => !includedFeatureFormDTO.Features.Select(c => c.Id).Contains(f.Id)).ToList()
                 .ForEach(removedFeature => includedFeature.Features.Remove(removedFeature));

                includedFeature.Features.ForEach(i =>
                {
                    var item = includedFeatureFormDTO.Features.Where(f => f.Id == i.Id ).First();
                    Mapper.Map<KeyValuePairDTO, Feature>(item, i);
                });

                includedFeatureFormDTO.Features
                .Where(item => !includedFeature.Features.Select(c => c.Id).Any(f => f == item.Id) && item.Id == 0)
                .Select(newFeature => new Feature() { Name = newFeature.Name, IncludedFeatureId = includedFeature.Id }).ToList()
                .ForEach(newFeature =>
                {
                    includedFeature.Features.Add(newFeature);
                });


            });

        }
    }
}

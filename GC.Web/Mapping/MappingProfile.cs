using AutoMapper;
using GC.Core.Entities;
using GC.Core.Querying;
using GC.Web.DTOs;

namespace GC.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultDTO<>));

            CreateMap<Company, KeyValuePairDTO>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<Photo, PhotoDTO>();
            CreateMap<ProvidedService, ProvidedServiceListItemDTO>();
            CreateMap<ProvidedServiceQuery, ProvidedServiceQueryDTO>();
            CreateMap<ProvidedService, ProvidedServiceFormDTO>();
            CreateMap<ProvidedService, ProvidedServiceDetailsDTO>();

            CreateMap(typeof(QueryResultDTO<>), typeof(QueryResult<>));

            CreateMap<CompanyDTO, Company>();
            CreateMap<KeyValuePairDTO, Company>();
            CreateMap<CompanyQueryDTO, CompanyQuery>();
            CreateMap<PhotoDTO, Photo>();
            CreateMap<ProvidedServiceQueryDTO, ProvidedServiceQuery>();
            CreateMap<ProvidedServiceFormDTO, ProvidedService>();
            CreateMap<ProvidedServiceDetailsDTO, ProvidedService>();

        }
    }
}

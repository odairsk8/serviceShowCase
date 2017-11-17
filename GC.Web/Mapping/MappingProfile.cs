using AutoMapper;
using GC.Core.Entities;
using GC.Core.Querying;
using GC.Web.DTOs;
using System.Collections.Generic;

namespace GC.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultDTO<>));

            CreateMap<Company, KeyValuePairDTO>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<CompanyQuery, CompanyQueryDTO>();

            CreateMap(typeof(QueryResultDTO<>), typeof(QueryResult<>));

            CreateMap<CompanyDTO, Company>();
            CreateMap<KeyValuePairDTO, Company>();
            CreateMap<CompanyQueryDTO, CompanyQuery>();
        }
    }
}

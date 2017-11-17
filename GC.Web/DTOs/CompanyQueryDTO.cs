using GC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GC.Web.DTOs
{
    public class CompanyQueryDTO : IQueryObjectDTO
    {
        public string Name { get; set; }
        public DateTime? FoundationStart { get; set; }
        public DateTime? FoundationEnd { get; set; }
        public List<string> FilterBy { get; set; }

        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }

        public CompanyQueryDTO()
        {
            this.FilterBy = new List<string>();
        }

     }
}

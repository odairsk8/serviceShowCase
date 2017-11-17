using GC.Core.Entities;
using GC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GC.Core.Querying
{
    public class CompanyQuery : IQueryObject<Company>
    {
        public string Name { get; set; }
        public DateTime? FoundationStart { get; set; }
        public DateTime? FoundationEnd { get; set; }

        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public List<string> FilterBy { get; set; }

        public Dictionary<string, Expression<Func<Company, object>>> OrderingMapping { get; set; }
        public Dictionary<string, Expression<Func<Company, bool>>> FilteringMapping { get; set; }

        public CompanyQuery()
        {
            this.FilterBy = new List<string>();
            this.CreateOrderMapping();
            this.CreateFilterMapping();
        }

        private void CreateOrderMapping()
        {
            this.OrderingMapping = new Dictionary<string, Expression<Func<Company, object>>>()
            {
                ["name"] = v => v.Name,
                ["foundation"] = v => v.Foundation
            };
        }

        private void CreateFilterMapping()
        {
            this.FilteringMapping = new Dictionary<string, Expression<Func<Company, bool>>>()
            {
                ["name"] = f => f.Name.ToLower().Contains(this.Name.ToLower().Trim())
            };
        }
    }
}

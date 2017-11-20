using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GC.Core.Entities;
using GC.Core.Interfaces;

namespace GC.Core.Querying
{
    public class ProvidedServiceQuery : IQueryObject<ProvidedService>
    {
        public int CompanyId { get; set; }

        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string, Expression<Func<ProvidedService, object>>> OrderingMapping { get; set; }

        public ProvidedServiceQuery()
        {
            this.CreateOrderMapping();
        }

        private void CreateOrderMapping()
        {
            this.OrderingMapping = new Dictionary<string, Expression<Func<ProvidedService, object>>>()
            {
                ["name"] = v => v.Name,
            };
        }

        public List<Expression<Func<ProvidedService, bool>>> GetConditions()
        {
            return new List<Expression<Func<ProvidedService, bool>>>() {
                this.FilterByCompanyIdClause()
            };
        }

        private Expression<Func<ProvidedService, bool>> FilterByCompanyIdClause()
        {
            return p => p.CompanyId == this.CompanyId;
        }

        
    }
}

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

        public Dictionary<string, Expression<Func<Company, object>>> OrderingMapping { get; set; }
       
        public CompanyQuery()
        {
            this.CreateOrderMapping();
        }

        private void CreateOrderMapping()
        {
            this.OrderingMapping = new Dictionary<string, Expression<Func<Company, object>>>()
            {
                ["name"] = v => v.Name,
                ["foundation"] = v => v.Foundation
            };
        }

        public List<Expression<Func<Company, bool>>> GetConditions()
        {
            var conditions = new List<Expression<Func<Company, bool>>>();

            if (!string.IsNullOrEmpty(this.Name))
                conditions.Add(this.ContainNameCondition());

            if (this.FoundationEnd != null || this.FoundationStart != null)
                conditions.Add(this.FilterByFoundationDateClause());

            return conditions;
        }

        private Expression<Func<Company, bool>> FilterByFoundationDateClause()
        {
            if (this.FoundationStart.HasValue && this.FoundationEnd.HasValue)
                return this.FoundationBetweenPeriodClause();
            if (this.FoundationStart.HasValue)
                return this.FoundationFromClause();
            return this.FoundationToClause();
        }

        private Expression<Func<Company, bool>> FoundationToClause()
        {
            var endDate = this.FoundationEnd.Value.AddDays(1);
            return company => (company.Foundation <= endDate);
        }

        private Expression<Func<Company, bool>> FoundationFromClause()
        {
            var startDate = this.FoundationStart.Value.AddDays(-1);
            return company => (company.Foundation >= startDate);
        }

        private Expression<Func<Company, bool>> FoundationBetweenPeriodClause()
        {
            var endDate = this.FoundationEnd.Value.AddDays(1);
            var startDate = this.FoundationStart.Value.AddDays(-1);
            return company => (company.Foundation >= startDate && company.Foundation <= endDate);
        }

        private Expression<Func<Company, bool>> ContainNameCondition() => f => f.Name.ToLower().Contains(this.Name.ToLower().Trim());
    }
}

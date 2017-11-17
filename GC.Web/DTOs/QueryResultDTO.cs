using System.Collections.Generic;

namespace GC.Web.DTOs
{
    public class QueryResultDTO<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}

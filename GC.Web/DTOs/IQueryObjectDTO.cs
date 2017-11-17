using System.Collections.Generic;

namespace GC.Web.DTOs
{
    public interface IQueryObjectDTO
    {
        bool IsSortAscending { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
        string SortBy { get; set; }
        List<string> FilterBy { get; set; }
    }
}

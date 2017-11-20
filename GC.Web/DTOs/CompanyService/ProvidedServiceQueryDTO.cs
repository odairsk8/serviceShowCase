namespace GC.Web.DTOs
{
    public class ProvidedServiceQueryDTO : IQueryObjectDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
    }
}

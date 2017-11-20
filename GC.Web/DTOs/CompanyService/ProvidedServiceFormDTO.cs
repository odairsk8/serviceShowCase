namespace GC.Web.DTOs
{
    public class ProvidedServiceFormDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string CoverTitle { get; set; }
        public string Description { get; set; }

        public int CompanyId { get; set; }
    }
}

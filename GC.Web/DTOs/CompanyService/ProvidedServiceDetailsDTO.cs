namespace GC.Web.DTOs
{
    public class ProvidedServiceDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoverTitle { get; set; }
        public string Description { get; set; }

        public PhotoDTO CoverImage { get; set; }

    }
}

namespace GC.Core.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int IncludedFeatureId { get; set; }
    }
}

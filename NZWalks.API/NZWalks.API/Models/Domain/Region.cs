namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Population { get; set; }

        // Navigation Property
        public IEnumerable<Walk> Walks { get; set; }
    }
}

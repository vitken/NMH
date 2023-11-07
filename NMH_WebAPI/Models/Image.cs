namespace NMH_WebAPI.Models
{
    public class Image
    {
        public long Id { get; set; } // Primary key
        public string Description { get; set; }
        public Author Author { get; set; }
    }
}

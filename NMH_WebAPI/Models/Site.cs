namespace NMH_WebAPI.Models
{
    public class Site
    {
        public long Id { get; set; } // Primary key
        public DateTimeOffset CreatedAt { get; set; }

        public IList<Article> Articles { get; set; }
    }
}

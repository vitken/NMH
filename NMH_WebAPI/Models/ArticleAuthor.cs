namespace NMH_WebAPI.Models
{
    public class ArticleAuthor
    {
        public long ArticleId { get; set; }
        public Article Article { get; set; }

        public long AuthorId { get; set; }
        public Author Author { get; set; }
    }
}

﻿using static System.Net.Mime.MediaTypeNames;

namespace NMH_WebAPI.Models
{
    public class Author
    {
        public long Id { get; set; } // Primary key
        public string Name { get; set; } // Unique index
        public virtual Image Image { get; set; } // One-To-One

        public IList<ArticleAuthor> ArticleAuthors { get; set; }
    }
}

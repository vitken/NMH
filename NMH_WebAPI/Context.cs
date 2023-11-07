using Microsoft.EntityFrameworkCore;
using NMH_WebAPI.Models;
using System;

namespace NMH_WebAPI
{
    public class Context : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<ArticleAuthor> ArticleAuthors { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgres;Database=nmh_db;Username=nmh_db_user;Password=Nmh123!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Constrains
            modelBuilder.Entity<Article>().HasKey(x => x.Id);
            modelBuilder.Entity<Article>().HasIndex(x => x.Title);
            modelBuilder.Entity<Author>().HasKey(x => x.Id);
            modelBuilder.Entity<Author>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Image>().HasKey(x => x.Id);
            modelBuilder.Entity<Site>().HasKey(x => x.Id);
            modelBuilder.Entity<ArticleAuthor>().HasKey(x => new { x.ArticleId, x.AuthorId});

            // One-to-one relationships
            modelBuilder.Entity<Author>()
                .HasOne<Image>(x => x.Image)
                .WithOne(x => x.Author)
                .HasForeignKey<Image>(x => x.Id);

            // One-to-many relationships
            modelBuilder.Entity<Article>()
                .HasOne<Site>(x => x.Site)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.Id);

            // Many-to-many relationships
            modelBuilder.Entity<ArticleAuthor>()
                .HasOne<Author>(x => x.Author)
                .WithMany(x => x.ArticleAuthors)
                .HasForeignKey(x => x.AuthorId);
            modelBuilder.Entity<ArticleAuthor>()
                .HasOne<Article>(x => x.Article)
                .WithMany(x => x.ArticleAuthors)
                .HasForeignKey(x => x.ArticleId);

        }
    }
}

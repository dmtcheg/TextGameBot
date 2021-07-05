using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace lingames.Models
{
    public class WordsContext : DbContext
    {
        public WordsContext()
        {
        }

        public WordsContext(DbContextOptions<WordsContext> options)
            : base(options)
        {
        }

        public DbSet<Nouns> Nouns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Filename =./wwwroot/Words.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nouns>(entity =>
            {
                entity.HasKey(e => e.Noun);

                entity.ToTable("nouns");

                entity.Property(e => e.Noun)
                    .HasColumnName("noun")
                    .HasColumnType("VARCHAR (50)")
                    .HasDefaultValueSql("\"\"");
            });
        }
    }
}

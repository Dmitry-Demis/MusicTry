using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicTry
{

    public class MusicContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Compilation> Compilations { get; set; }
        public MusicContext()
        {

           // Database.EnsureCreated();
           // ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Указываем использование SQLite
            if (!optionsBuilder.IsConfigured)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                optionsBuilder.UseSqlite("Data Source=C:\\TEMP\\music.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Установка связи между Album и Artist
            modelBuilder.Entity<Album>()
                .HasOne(album => album.Artist)
                .WithMany(artist => artist.Albums)
                .HasForeignKey("ArtistId")
                .IsRequired();

            // Установка связи между Album и Genre (необязательно)
            modelBuilder.Entity<Album>()
                .HasOne(album => album.Genre)
                .WithMany()
                .HasForeignKey("GenreId")
                .IsRequired(false); // Genre может быть пустым

            // Установка связи между Track и Album
            modelBuilder.Entity<Track>()
                .HasOne(track => track.Album)
                .WithMany(album => album.Tracks)
                .HasForeignKey("AlbumId")
                .IsRequired();
        }

    }

}

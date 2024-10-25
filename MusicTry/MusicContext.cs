using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicTry
{

    public class MusicContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Track> Tracks { get; set; } = null!;
        public DbSet<Compilation> Compilations { get; set; } = null!;

        public MusicContext()
        {

           // Database.EnsureCreated();
           // ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Указываем использование SQLite
            optionsBuilder.UseSqlite("Data Source=music.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка таблицы Artist
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(a => a.Id); // Установка первичного ключа
                entity.Property(a => a.Name).IsRequired(); // Название артиста обязательно
                entity.HasOne(a => a.Genre) // Настройка отношения 1:M с Genre
                      .WithMany(g => g.Artists)
                      .HasForeignKey("GenreId"); // Имя внешнего ключа в таблице Artist

                // Настройка отношения 1:M с Album
                entity.HasMany(a => a.Albums)
                      .WithOne(a => a.Artist)
                      .HasForeignKey("ArtistId"); // Имя внешнего ключа в таблице Album
            });

            // Настройка таблицы Genre
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.Id); // Установка первичного ключа
                entity.Property(g => g.Name).IsRequired(); // Название жанра обязательно
            });

            // Настройка таблицы Album
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(a => a.Id); // Установка первичного ключа
                entity.Property(a => a.Title).IsRequired(); // Название альбома обязательно
                entity.HasOne(a => a.Artist) // Настройка отношения 1:M с Artist
                      .WithMany(a => a.Albums)
                      .HasForeignKey("ArtistId"); // Имя внешнего ключа в таблице Album

                // Настройка отношения 1:M с Track
                entity.HasMany(a => a.Tracks)
                      .WithOne(t => t.Album)
                      .HasForeignKey("AlbumId"); // Имя внешнего ключа в таблице Track
            });

            // Настройка таблицы Track
            modelBuilder.Entity<Track>(entity =>
            {
                entity.HasKey(t => t.Id); // Установка первичного ключа
                entity.Property(t => t.Title).IsRequired(); // Название трека обязательно
                entity.Property(t => t.Duration).IsRequired(); // Длительность трека обязательна
            });

            // Настройка таблицы Compilation
            modelBuilder.Entity<Compilation>(entity =>
            {
                entity.HasKey(c => c.Id); // Установка первичного ключа
                entity.Property(c => c.Title).IsRequired(); // Название сборника обязательно

                // Настройка отношения M:N с Track
                entity.HasMany(c => c.Tracks)
                      .WithMany() // Без навигационного свойства в Track
                      .UsingEntity<Dictionary<string, object>>(
                          "CompilationTrack", // Имя промежуточной таблицы
                          j => j
                              .HasOne<Track>()
                              .WithMany()
                              .HasForeignKey("TrackId")
                              .OnDelete(DeleteBehavior.Cascade),
                          j => j
                              .HasOne<Compilation>()
                              .WithMany()
                              .HasForeignKey("CompilationId")
                              .OnDelete(DeleteBehavior.Cascade));
            });
        }
    }

    public static class DatabaseService
    {
        private static MusicContext? _context = new MusicContext();

        public static async Task SaveArtistAsync(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
        }

        public static async Task SaveAlbumAsync(Album album)
        {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
        }

        public static async Task SaveCompilationAsync(Compilation compilation)
        {
            _context.Compilations.Add(compilation);
            await _context.SaveChangesAsync();
        }

        // Если нужно, можно добавить методы для загрузки данных
        public static async Task<Artist?> GetArtistByIdAsync(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public static async Task<List<Album>> GetAlbumsByArtistIdAsync(int artistId)
        {
            return await _context.Albums.Where(album => album.Artist.Id == artistId).ToListAsync();
        }

        // Закрытие контекста, если это необходимо
        public static void Dispose()
        {
            _context.Dispose();
            _context = null;
        }
    }

}

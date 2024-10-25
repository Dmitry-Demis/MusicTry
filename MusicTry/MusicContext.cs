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

            Database.EnsureCreated();
            ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Указываем использование SQLite
            optionsBuilder.UseSqlite("Data Source=music.db");
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

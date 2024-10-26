using Microsoft.EntityFrameworkCore;

namespace MusicTry
{
    public static class DatabaseService
    {
        private static MusicContext _context = new MusicContext();

        public static async Task SaveArtistAsync(Artist artist)
        {
            // Проверка на существование артиста с тем же именем в нижнем регистре
            bool artistExists = await _context.Artists
                .AnyAsync(a => a.Name.ToLower() == artist.Name.ToLower());

            if (artistExists)
            {
                // Уведомление или логирование, если артист с таким именем уже существует
                throw new InvalidOperationException("Артист с таким именем уже существует.");
            }

            // Добавление и сохранение нового артиста
            _context.Add(artist);
            await _context.SaveChangesAsync();
        }

        public static async Task SaveAlbumAsync(Album album)
        {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
        }
        public static async Task<Artist> GetArtistByNameAsync(string name)
        {
            // Ensure the input name is not null
            if (string.IsNullOrEmpty(name))
            {
                return null; // or throw an exception if that's preferable
            }

            // Fetch all artists from the database
            var artists = await _context.Artists.ToListAsync();

            // Convert the input name to lower case for comparison
            var lowerCaseName = name.ToLower();

            // Loop through each artist to find a match
            foreach (var artist in artists)
            {
                if (artist.Name != null && artist.Name.ToLower() == lowerCaseName)
                {
                    return artist; // Return the matching artist
                }
            }

            return null; // Return null if no artist was found
        }



        public static async Task AddArtistAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
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
        public static async Task<List<Artist>> GetArtistsAsync()
        {
            return await _context.Artists.ToListAsync();
        }
        // Новый метод для получения всех жанров
        public static async Task<List<Genre>> GetGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
        public static async Task<List<Album>> GetAlbumsByArtistAsync(Artist artist)
        {
            return await _context.Albums
                                 .Where(album => album.Artist.Id == artist.Id)
                                 .Include(album => album.Genre)  // подгружаем жанр
                                 .ToListAsync();
        }

        public static async Task<List<Track>> GetTracksByAlbumAsync(Album album)
        {
            return await _context.Tracks
                                 .Where(track => track.Album.Id == album.Id)
                                 .Include(track => track.Album)   // подгружаем альбом, если нужен доступ к полям
                                 .ToListAsync();
        }
    }

}

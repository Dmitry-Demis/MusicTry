namespace MusicTry
{
    public class AlbumBuilder : IBuilder<Album>
    {
        private string _title = string.Empty; // Инициализация с пустой строкой
        private Artist _artist = null!; // Используем null-символ для указания, что это значение будет установлено
        private Genre _genre = null!; // Используем null-символ для указания, что это значение будет установлено
        private readonly List<Track> _tracks = []; // Используем List вместо []
        private string _releaseDate = string.Empty; // Инициализация минимальным значением

        public AlbumBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public AlbumBuilder SetArtist(Artist artist)
        {
            _artist = artist;
            return this;
        }

        public AlbumBuilder SetTrack(Track track)
        {
            _tracks.Add(track);
            return this;
        }
        public AlbumBuilder SetTracks(List<Track> tracks)
        {
            _tracks.AddRange(tracks);
            return this;
        }
        public AlbumBuilder SetGenre(Genre genre)
        {
            _genre = genre;
            return this;
        }

        public AlbumBuilder SetReleaseDate(string releaseDate)
        {
            _releaseDate = releaseDate;
            return this;
        }

        public Album Build()
        {
            if (string.IsNullOrWhiteSpace(_title))
                throw new InvalidOperationException("Title must be set.");
            if (_artist is null)
                throw new InvalidOperationException("Artist must be set.");

            return new Album
            {
                Title = _title,
                Artist = _artist,
                Tracks = _tracks,
                ReleaseDate = _releaseDate,
                Genre = _genre,
            };
        }
    }

}

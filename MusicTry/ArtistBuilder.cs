using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTry
{
    public interface IBuilder<T>
    {
        T Build();
    }

    public class ArtistBuilder : IBuilder<Artist>
    {
        private string _name = string.Empty; // Инициализация с пустой строкой
        private Genre _genre = null!; // Используем null-символ для указания, что это значение будет установлено
        private readonly List<Album> _albums = []; // Инициализация коллекции

        public ArtistBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public ArtistBuilder SetGenre(Genre genre)
        {
            _genre = genre;
            return this;
        }

        public ArtistBuilder AddAlbum(Album album)
        {
            _albums.Add(album);
            return this;
        }

        public Artist Build()
        {
            // Проверка на наличие обязательных полей
            if (string.IsNullOrWhiteSpace(_name))
                throw new InvalidOperationException("Name must be set.");
            if (_genre is null)
                throw new InvalidOperationException("Genre must be set.");

            return new Artist
            {
                Name = _name,
                Genre = _genre,
                Albums = _albums
            };
        }
    }
    public class AlbumBuilder : IBuilder<Album>
    {
        private string _title = string.Empty; // Инициализация с пустой строкой
        private Artist _artist = null!; // Используем null-символ для указания, что это значение будет установлено
        private readonly List<Track> _tracks = []; // Используем List вместо []
        private DateTime _releaseDate = DateTime.MinValue; // Инициализация минимальным значением

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

        public AlbumBuilder AddTrack(Track track)
        {
            _tracks.Add(track);
            return this;
        }

        public AlbumBuilder SetReleaseDate(DateTime releaseDate)
        {
            _releaseDate = releaseDate;
            return this;
        }

        public Album Build()
        {
            // Здесь можно добавить проверку на наличие обязательных полей
            if (string.IsNullOrWhiteSpace(_title))
                throw new InvalidOperationException("Title must be set.");
            if (_artist is null)
                throw new InvalidOperationException("Artist must be set.");

            return new Album
            {
                Title = _title,
                Artist = _artist,
                Tracks = _tracks,
                ReleaseDate = _releaseDate
            };
        }
    }

}

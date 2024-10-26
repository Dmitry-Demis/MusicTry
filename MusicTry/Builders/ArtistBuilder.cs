using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTry
{

    public class ArtistBuilder : IBuilder<Artist>
    {
        private string _name = string.Empty; // Инициализация с пустой строкой
        private readonly List<Album> _albums = []; // Инициализация коллекции

        public ArtistBuilder SetName(string name)
        {
            _name = name;
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
            //if (_genre is null)
            //    throw new InvalidOperationException("Genre must be set.");

            return new Artist
            {
                Name = _name,
                Albums = _albums
            };
        }
    }
}

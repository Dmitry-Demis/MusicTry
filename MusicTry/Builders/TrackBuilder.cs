namespace MusicTry
{
    public class TrackBuilder : IBuilder<Track>
    {
        private string _title = string.Empty; // Инициализация с пустой строкой
        private double _duration; // Инициализация длительности трека
        private Album _album = null!; // Альбом, к которому относится трек

        public TrackBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public TrackBuilder SetDuration(double minutes, double seconds)
        {
            // Преобразование времени в секунды
            _duration = (minutes * 60) + seconds;
            return this;
        }

        public TrackBuilder SetAlbum(Album album)
        {
            _album = album;
            return this;
        }

        public Track Build()
        {
            // Проверка на наличие обязательных полей
            if (string.IsNullOrWhiteSpace(_title))
                throw new InvalidOperationException("Title must be set.");
            if (_album is null)
                throw new InvalidOperationException("Album must be set.");

            return new Track
            {
                Title = _title,
                Duration = _duration,
                Album = _album
            };
        }
    }


}

namespace MusicTry
{
    public class TrackBuilder : IBuilder<Track>
    {
        private string _title = string.Empty;
        private double _duration;
        private Album _album = null!;

        public TrackBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public TrackBuilder SetDuration(double minutes, double seconds)
        {
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

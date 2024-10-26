namespace MusicTry
{
    public class CompilationBuilder : IBuilder<Compilation>
    {
        private string _title = string.Empty;
        private readonly List<Track> _tracks = new List<Track>();

        public CompilationBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public CompilationBuilder AddTrack(Track track)
        {
            _tracks.Add(track);
            return this;
        }
        public CompilationBuilder AddTracks(List<Track> tracks)
        {
            _tracks.AddRange(tracks);
            return this;
        }

        public Compilation Build()
        {
            if (string.IsNullOrWhiteSpace(_title))
                throw new InvalidOperationException("Title must be set.");

            return new Compilation
            {
                Title = _title,
                Tracks = _tracks
            };
        }
    }

}

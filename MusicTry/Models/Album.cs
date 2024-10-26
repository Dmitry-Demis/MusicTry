namespace MusicTry
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }

        public List<Track> Tracks { get; init; } = [];
        public string ReleaseDate { get; set; }
    }

}

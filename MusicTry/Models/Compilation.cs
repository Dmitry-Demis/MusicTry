namespace MusicTry
{
    public class Compilation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Track> Tracks { get; init; } = [];
    }

}

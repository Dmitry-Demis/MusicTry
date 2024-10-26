namespace MusicTry
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Album Album { get; set; }
        public double Duration { get; set; }

        public ICollection<Compilation> Compilations { get; set; } = new List<Compilation>();
    }

}

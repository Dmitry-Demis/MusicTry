using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTry
{
    public class Artist
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Genre Genre { get; set; }
        public List<Album> Albums { get; init; } = [];
    }
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Artist> Artists { get; init; } = [];
    }

    public class Album
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required Artist Artist { get; set; }
        public List<Track> Tracks { get; init; } = [];
        public DateTime ReleaseDate { get; set; }
    }
    public class Track
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required Album Album { get; set; }
        public double Duration { get; set; }
    }

    public class Compilation
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public List<Track> Tracks { get; init; } = [];
    }

}

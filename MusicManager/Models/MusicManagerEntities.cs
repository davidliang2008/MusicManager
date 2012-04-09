using System.Data.Entity;

namespace MusicManager.Models
{
    public class MusicManagerEntities : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
    }
}
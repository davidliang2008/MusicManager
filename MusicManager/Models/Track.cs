using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicManager.Models
{
    public class Track
    {
        public int TrackId { get; set; }
        public int AlbumId { get; set; }
        public string TrackName { get; set; }
        public int Order { get; set; }
        public int Length { get; set; }
    }
}
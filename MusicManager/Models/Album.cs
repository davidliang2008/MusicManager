using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MusicManager.Models
{
    public class Album
    {
        [ScaffoldColumn(false)]
        public int AlbumId { get; set; }
        [Required(ErrorMessage="An Album Title is required")]
        [StringLength(60)]
        public string Title { get; set; }
        [Required(ErrorMessage="Name of artist is required")]
        [StringLength(40)]
        public string Artist { get; set; }
        [Required(ErrorMessage="Genre Name is required")]
        [StringLength(40)]
        public string Genre { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual List<Track> Tracks { get; set; }
    }
}
using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_Music.Music
{
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public int IdArtist { get; set; }
        public string Name { get; set; }
        public string Annee { get; set; }
        public string Discription { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}

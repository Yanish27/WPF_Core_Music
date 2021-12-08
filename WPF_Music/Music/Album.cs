using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_Music.Music
{
    public partial class Album
    {
        public Album()
        {
            Ratings = new HashSet<Rating>();
        }

        public int IdAlbums { get; set; }
        public string Titre { get; set; }
        public int ArtistIdArtist { get; set; }

        public virtual Artist ArtistIdArtistNavigation { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}

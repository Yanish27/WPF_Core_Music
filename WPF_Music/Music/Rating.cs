using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_Music.Music
{
    public partial class Rating
    {
        public int IdRatings { get; set; }
        public string Grade { get; set; }
        public int AlbumsIdAlbums { get; set; }

        public virtual Album AlbumsIdAlbumsNavigation { get; set; }
    }
}

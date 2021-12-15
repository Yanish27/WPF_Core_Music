using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Music.Music;
using Microsoft.EntityFrameworkCore;
namespace WPF_Music.DAO
{

    public class DAO_Album
    {

        private musicContext Context;
        public string AddAlbum(Album album)
        {
            using (Context = new musicContext())
            {
                Context.Albums.Add(album);
                Context.SaveChanges();
            }
            return "L'artiste à bien été ajouté.";
        }

    }
}

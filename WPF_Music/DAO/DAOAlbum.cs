using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Music.Music;
using Microsoft.EntityFrameworkCore;
namespace WPF_Music.DAO
{
    /// <summary>
    /// DAO Album : Data Access Object pour accéder aux Albums
    /// </summary>
    public class DAO_Album
    {

        private musicContext Context;

        /// <summary>
        /// Permet d'ajouter un Album
        /// </summary>
        /// <param name="album">Un objet de type Album</param>
        /// <returns>String L'artiste à bien été ajouté.</returns>
        public string AddAlbum(Album album)
        {
            using (Context = new musicContext())
            {
                Context.Albums.Add(album);
                Context.SaveChanges();
            }
            return "L'album à bien été ajouté.";
        }

        public IEnumerable<Album> GetAlbum()
        {
            using (Context = new musicContext())
            {
                var AllArtists = Context.Albums.ToList();
                return AllArtists;
            }
        }

        public List<Album> getAlbumByName(string name)
        {
            using (Context = new musicContext())
            {
                var alb = Context.Albums.Where(a => a.Titre == name).ToList();
                // On fait .ToList parce qu'il peut y avoir plusieurs artiste qui ont le même nom
                return alb;
            }
        }

        public int CountAlbum()
        {
            int nb_album = 0;
            using (Context = new musicContext())
            {
               foreach (var alb in Context.Albums.ToList())
                {
                    nb_album = nb_album + 1;
                }
                return nb_album;
            }
        }


        /// <summary>
        /// Permet de supprimer un album
        /// </summary>
        /// <param name="ID">ID de l'album</param>
        /// <returns></returns>

        public string DeleteAlbum(int ID)
        {
            using (Context = new musicContext())
            {
                var itemToRemove = Context.Albums.SingleOrDefault(x => x.IdAlbums == ID);   //On supprime par la clé primaire 
                if (itemToRemove != null)
                {
                    Context.Albums.Remove(itemToRemove);
                    Context.SaveChanges();
                }



            }
            return "L'album à bien été supprimé.";
        }


    }
}

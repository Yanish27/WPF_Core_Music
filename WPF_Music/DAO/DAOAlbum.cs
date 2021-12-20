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

        // On initialise le conext
        private musicContext Context;

        /// <summary>
        /// Permet d'ajouter un Album
        /// </summary>
        /// <param name="album">Un objet de type Album</param>
        /// <returns>String L'artiste à bien été ajouté.</returns>
        public string AddAlbum(Album album)
        {
            // On initialise le context
            using (Context = new musicContext())
            {
                // On ajoute l'album
                Context.Albums.Add(album);
                // On sauvegarde
                Context.SaveChanges();
            }
            return "L'album à bien été ajouté.";
        }

        public IEnumerable<Album> GetAlbum()
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les albums de la BDD dans une liste
                // liste car il y aplusieurs albums
                var AllArtists = Context.Albums.ToList();
                // On retourne les albums
                return AllArtists;
            }
        }

        public List<Album> getAlbumByName(string name)
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les albums de la BDD dans une liste
                // On fait .ToList parce qu'il peut y avoir plusieurs albums qui ont le même nom
                var alb = Context.Albums.Where(a => a.Titre == name).ToList();
                // On retourne les albums
                return alb;
            }
        }

        public int CountAlbum()
        {
            // Nombre d'albums = 0
            int nb_album = 0;
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les albums de la BDD dans une liste
               foreach (var alb in Context.Albums.ToList())
                {
                    // On incrémente le nombre d'albums
                    nb_album = nb_album + 1;
                }
                // On retourne le nombre d'albums   
                return nb_album;
            }
        }

        
        public string DeleteAlbumByArtistId(int ID)
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les albums de la BDD dans une liste
                foreach (var m in Context.Albums.Where(x => x.ArtistIdArtist == ID))
                {
                    // On supprime l'album
                    Context.Albums.Remove(m);
                }
                // On sauvegarde
                Context.SaveChanges();
            }
            // On retourne le message
            return "L'artiste à bien été supprimé.";
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

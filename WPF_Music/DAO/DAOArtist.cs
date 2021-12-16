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
    class DAO_Artist
    {

        // On initialise le conext
        private musicContext Context;

        /// <summary>
        /// GetArtist() ne prénds pas d'arguments
        /// </summary>
        /// <returns>renvoie une liste de Artist</returns>
        public IEnumerable<Artist> GetArtists()
        {
            using (Context = new musicContext())
            {
                var AllArtists = Context.Artists.ToList();
                return AllArtists;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Artist GetArtistByID(int id)
        {
            using (Context = new musicContext())
            {
                Artist artist = Context.Artists.Single(a => a.IdArtist == id);
                return artist;
            }
        }

        /// <summary>
        /// Permet d'obtenir les informations d'un artiste par son nom
        /// </summary>
        /// <param name="name">Nom de l'artiste</param>
        /// <returns>List de Artist</returns>
        public List<Artist> GetArtistByName(string name)
        {
            using (Context = new musicContext())
            {
                var artist = Context.Artists.Where(a => a.Name == name).ToList(); 
                // On fait .ToList parce qu'il peut y avoir plusieurs artiste qui ont le même nom
                return artist;
            }
        }


        /// <summary>
        /// Connaitre tous les artistes qui commencent par ....
        /// </summary>
        /// <param name="name">Debut du nom de l'artiste</param>
        /// <returns>IEnumerable de Artist</returns>
        public IEnumerable<Artist> GetArtistsStartsByName(string name)
        {
            using (Context = new musicContext())
            {
                var ListArtist = Context.Artists.Where(a => a.Name.StartsWith(name)).ToList();
                return ListArtist;
            }
        }

        /// <summary>
        /// Permet de connaitre les artistes d'un album
        /// </summary>
        /// <returns>List de Artist</returns>
        public List<Artist> GetArtistsAlbums()
        {
            using (Context = new musicContext())
            {
                var artistesAlbums = Context.Artists.Include(art => art.Albums).ToList();  
                // On fait une jointure avec la table Album grâce au include
                return artistesAlbums;
            }
        }

        /// <summary>
        /// Permet d'avoir les artistes, album et note
        /// </summary>
        /// <returns>List de Artist</returns>
        public List<Artist> GetArtistAlbumsRating()
        {
            using (Context = new musicContext())
            {
                var artistAlbums = Context.Artists.Include(art => art.Albums).ThenInclude(alb => alb.Ratings).ToList();  //Double jointure 3 tables 
                return artistAlbums;
            }
        }


        /// <summary>
        /// Permet d'ajouter un artist
        /// </summary>
        /// <param name="artist">Prend un objet de type Artist</param>
        /// <returns>String : L'artiste à bien été ajouté.</returns>
        public string AddArtist(Artist artist)
        {
            using (Context = new musicContext())
            {
                Context.Artists.Add(artist);
                Context.SaveChanges();
            }
            return "L'artiste à bien été ajouté.";
        }




        /// <summary>
        /// Permet de mettre à jour un artist
        /// </summary>
        /// <param name="artist">Prend un objet de type Artist</param>
        /// <returns>String : L'artiste à bien été mis à jour.</returns>
        public string UpdateArtist(Artist artist)  //on modifie une valeur existante
        {
            using (Context = new musicContext())
            {
                Context.Entry(artist).State = EntityState.Modified;
                Context.SaveChanges();
            }
            return "L'artiste à bien été mis à jour.";
        }


        /// <summary>
        /// Permet de supprimer un artist
        /// </summary>
        /// <param name="artist">Prend un objet de type Artist</param>
        /// <returns>String : L'artiste à bien été supprimé.</returns>

        public string DeleteArtist(int ID)
        {
            using (Context = new musicContext())
            {
                var itemToRemove = Context.Artists.SingleOrDefault(x => x.IdArtist == ID);   //On supprime par la clé primaire 
                if (itemToRemove != null)
                {
                    Context.Artists.Remove(itemToRemove);
                    Context.SaveChanges();
                }



            }
            return "L'artiste à bien été supprimé.";
        }


    }
}

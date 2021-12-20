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
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les albums de la BDD dans une liste
                var AllArtists = Context.Artists.ToList();
                // On retourne les albums
                return AllArtists;
            }
        }

        
        public int CountArtist()
        {
            // nb de artistes = 0
            int nb_artist = 0;
            // On initialise le conext 
            using (Context = new musicContext())
            {
                //  On récupère les albums de la BDD dans une liste
                foreach (var art in Context.Artists.ToList())
                {
                    // On incrémente le nombre d'artiste
                    nb_artist = nb_artist + 1;
                }
                // On retourne le nombre d'artiste 
                return nb_artist;
            }
        }

        /// <summary>
        /// Sert à avoir un artiste en fonction de son ID
        /// </summary>
        /// <param name="id">ID de l'artiste</param>
        /// <returns>Un objet de type artiste</returns>
        public Artist GetArtistByID(int id)
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère l'artiste de la BDD pour un ID donné
                Artist artist = Context.Artists.Single(a => a.IdArtist == id);
                // On retourne l'artiste
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
            //  On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les artistes de la BDD dans une liste
                var artist = Context.Artists.Where(a => a.Name == name).ToList(); 
                // On fait .ToList parce qu'il peut y avoir plusieurs artiste qui ont le même nom
                return artist;
            }
        }

        /// <summary>
        /// Permet de connaitre les artistes d'un album
        /// </summary>
        /// <returns>List de Artist</returns>
        public List<Artist> GetArtistsAlbums()
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les albums de la BDD dans une liste
                var artistesAlbums = Context.Artists.Include(art => art.Albums).ToList();  
                // On fait une jointure avec la table Album grâce au include
                return artistesAlbums;
            }
        }

        /// <summary>
        /// Permet d'ajouter un artist
        /// </summary>
        /// <param name="artist">Prend un objet de type Artist</param>
        /// <returns>String : L'artiste à bien été ajouté.</returns>
        public string AddArtist(Artist artist)
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On ajoute l'artiste à la BDD
                Context.Artists.Add(artist);
                // On sauvegarde les changements
                Context.SaveChanges();
            }
            // On retourne le message
            return "L'artiste à bien été ajouté.";
        }

        /// <summary>
        /// Permet de mettre à jour un artist
        /// </summary>
        /// <param name="artist">Prend un objet de type Artist</param>
        /// <returns>String : L'artiste à bien été mis à jour.</returns>
        public string UpdateArtist(Artist artist)  //on modifie une valeur existante
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère l'artiste de la BDD pour un ID donné
                Context.Entry(artist).State = EntityState.Modified;
                // On sauvegarde les changements
                Context.SaveChanges();
            }
            // On retourne le message
            return "L'artiste à bien été mis à jour.";
        }

        /// <summary>
        /// Permet de supprimer un artist
        /// </summary>
        /// <param name="artist">Prend un objet de type Artist</param>
        /// <returns>String : L'artiste à bien été supprimé.</returns>
        public string DeleteArtist(int ID)
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                //  On récupère l'artiste de la BDD pour un ID donné
                var itemToRemove = Context.Artists.SingleOrDefault(x => x.IdArtist == ID);   //On supprime par la clé primaire 
                // Si un artiste a été trouve
                if (itemToRemove != null)
                {
                    // On le supprime
                    Context.Artists.Remove(itemToRemove);
                    // On sauvegarde les changements
                    Context.SaveChanges();
                }
            }
            // On retourne le message
            return "L'artiste à bien été supprimé.";
        }


    }
}

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
    /// DAO Raiting : Data Access Object pour accéder aux Raitings
    /// </summary>
    class DAORaiting
    {
        // On initialise le conext
        private musicContext Context;
        
        public string GetRaitingByID(int id)
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les albums de la BDD dans une liste
                var raiting = Context.Ratings.SingleOrDefault(a => a.AlbumsIdAlbums == id);
                // Si il y a au moins 1 raiting
                if (raiting != null)
                {
                    // On retourne le raiting
                    return raiting.Grade;
                } else
                {
                    // Sinon on retourne un message d'erreur
                    return "error";
                }
            }
        }

        public void DeleteRating(int id){
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les albums de la BDD dans une liste
                var raiting = Context.Ratings.SingleOrDefault(a => a.AlbumsIdAlbums == id);
                // Si il y a au moins 1 raiting
                if (raiting != null)
                {
                    // On supprime le raiting
                    Context.Ratings.Remove(raiting);
                    // On sauvegarde les changements
                    Context.SaveChanges();
                }
            }
        }

        public string AddRaiting(Rating raiting)
        {
            // On initialise le conext
            using (Context = new musicContext())
            {
                // On ajoute le raiting
                Context.Ratings.Add(raiting);
                //  On sauvegarde
                Context.SaveChanges();
            }
            // On retourne un message de succès
            return "Le raiting à bien été ajouté.";
        }
        /// <summary>
        /// Retourne le nombre d'album pour cette note
        /// </summary>
        public int CountAlbumByRaiting(string grade)
        {
            int nb_album = 0;
            //  On initialise le conext
            using (Context = new musicContext())
            {
                // On récupère les albums de la BDD dans une liste
                foreach (var alb in Context.Ratings.ToList())
                {
                    // On aurait pu faire un foreach
                    if (alb.Grade == grade)
                    {
                        // On incrémente le nombre d'albums
                        nb_album = nb_album + 1;
                    }
                }
                // On retourne le nombre d'albums
                return nb_album;
            }
        }
    }

}

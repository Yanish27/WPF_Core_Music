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
            using (Context = new musicContext())
            {
                var raiting = Context.Ratings.SingleOrDefault(a => a.AlbumsIdAlbums == id);
                if (raiting != null)
                {
                    return raiting.Grade;
                } else
                {
                    return "error";
                }
            }
        }

        public void DeleteRating(int id){
            using (Context = new musicContext())
            {
                var raiting = Context.Ratings.SingleOrDefault(a => a.AlbumsIdAlbums == id);
                if (raiting != null)
                {
                    Context.Ratings.Remove(raiting);
                    Context.SaveChanges();
                }
            }
        }

        public string AddRaiting(Rating raiting)
        {
            using (Context = new musicContext())
            {
                Context.Ratings.Add(raiting);
                Context.SaveChanges();
            }
            return "Le raiting à bien été ajouté.";
        }
        /// <summary>
        /// Retourne le nombre d'album pour cette note
        /// </summary>
        public int CountAlbumByRaiting(string grade)
        {
            int nb_album = 0;
            using (Context = new musicContext())
            {
                foreach (var alb in Context.Ratings.ToList())
                {
                    if (alb.Grade == grade)
                    {
                        nb_album = nb_album + 1;
                    }
                }
                return nb_album;
            }
        }
    }

}

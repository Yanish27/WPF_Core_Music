using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Music.Music;
using Microsoft.EntityFrameworkCore;
namespace WPF_Music.DAO
{
    class DAO_Artist
    {
        private musicContext Context;
        public IEnumerable<Artist> GetArtists()
        {
            using (Context = new musicContext())
            {
                var AllArtists = Context.Artists.ToList();
                return AllArtists;
            }
        }


        public Artist GetArtistByID(int id)
        {
            using (Context = new musicContext())
            {
                Artist artist = Context.Artists.Single(a => a.IdArtist == id);
                return artist;
            }
        }

        public List<Artist> GetArtistByName(string name)
        {
            using (Context = new musicContext())
            {
                var artist = Context.Artists.Where(a => a.Name == "Venki").ToList(); 
                //On fait .ToList parce qu'il peut y avoir plusieurs venki. 
                return artist;
            }
        }

        public IEnumerable<Artist> GetArtistsStartsByName(string name)
        {
            using (Context = new musicContext())
            {
                var ListArtist = Context.Artists.Where(a => a.Name.StartsWith(name)).ToList();
                return ListArtist;
            }
        }


        public List<Artist> GetArtistsAlbums()
        {
            using (Context = new musicContext())
            {
                var artistesAlbums = Context.Artists.Include(art => art.Albums).ToList();  
                //On fait une jointure avec la table Album grâce au Include
                return artistesAlbums;
            }
        }



        public List<Artist> GetArtistAlbumsRating()
        {
            using (Context = new musicContext())
            {
                var artistAlbums = Context.Artists.Include(art => art.Albums).ThenInclude(alb => alb.Ratings).ToList();  //Double jointure 3 tables 
                return artistAlbums;
            }
        }



        public string AddArtist(Artist artist)
        {
            using (Context = new musicContext())
            {
                Context.Artists.Add(artist);
                Context.SaveChanges();
            }
            return "L'artiste à bien été ajouté.";
        }





        public string UpdateArtist(Artist artist)  //on modifie une valeur existante
        {
            using (Context = new musicContext())
            {
                Context.Entry(artist).State = EntityState.Modified;
                Context.SaveChanges();
            }
            return "L'artiste à bien été mis à jour.";
        }




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
            return "L'artiste à bien été supprimé";
        }


    }
}

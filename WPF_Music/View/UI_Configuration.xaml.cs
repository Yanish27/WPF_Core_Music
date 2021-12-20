using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Music.DAO;
using WPF_Music.Music;

namespace WPF_Music.View
{
    /// <summary>
    /// Logique d'interaction pour UI_Configuration.xaml
    /// </summary>
    public partial class UI_Configuration : UserControl
    {

        DAO_Artist daoart;
        DAO_Album daoalbum;

        int art_sel;
        int alb_sel;
            public UI_Configuration()
            {
            InitializeComponent();

            daoart = new DAO_Artist();
            daoalbum = new DAO_Album();

            foreach (Artist artist in  daoart.GetArtists())
            {
                CB_Artist.Items.Add(artist.Name);

            }
            foreach (Album album in daoalbum.GetAlbum())
            {
                CB_Album.Items.Add(album.Titre);
            }

        }

        private void btnAjoutAlbum_Click(object sender, RoutedEventArgs e)
        {
            if (TB_Album.Text != "")
            {
                Album album = new Album();
                album.ArtistIdArtist = art_sel;
                album.Titre = TB_Album.Text;
                daoalbum.AddAlbum(album);
                MessageBox.Show("L'album à bien été ajouté.");
                TB_Album.Text = "";
            } else
            {
                MessageBox.Show("Merci de saisir un nom d'album");
            }

        }

        private void CB_Artist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Création d'une variable artist de type List de Artist
            List <Artist> artiste = new List<Artist>();

            // On met dans la variable artiste la liste de Artist
            artiste = daoart.GetArtistByName(CB_Artist.SelectedItem.ToString());

            // On prend volontairement le premier résultat qui nous est retourné.
            // art_sel est une variable globale. Artiste Selectionné.
            art_sel = artiste[0].IdArtist;
            // DEBUG : MessageBox.Show("ID DE LARTISTE : " + artiste[0].IdArtist);
        }

        private void CB_Album_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Création d'une variable artist de alb List de Album
            List<Album> alb = new List<Album>();
           
            // On met dans la variable alb la liste de Album
            alb = daoalbum.getAlbumByName(CB_Album.SelectedItem.ToString());

            // On prend volontairement le premier résultat qui nous est retourné.
            // alb_sel est une variable globale. Artiste Selectionné.
            alb_sel = alb[0].IdAlbums;
            
            // DEBUG : MessageBox.Show("ID DE L'ALBUM : " + alb_sel);
        }

        private void btnSupprAlbum_Click(object sender, RoutedEventArgs e)
        {
            var res = daoalbum.DeleteAlbum(alb_sel);
            MessageBox.Show(res);
        }
    }
}

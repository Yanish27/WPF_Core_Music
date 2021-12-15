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
            public UI_Configuration()
        {
            InitializeComponent();

            daoart = new DAO_Artist();
 
            foreach(Artist artist in  daoart.GetArtists())
            {
                CB_Artist.Items.Add(artist.Name);

            }


        }

        private void btnAjoutAlbum_Click(object sender, RoutedEventArgs e)
        {

            Album album = new Album();
            album.ArtistIdArtist = 2;
            album.Titre = "La danse ";

            daoalbum.AddAlbum(album);

        }

        private void CB_Artist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            List <Artist> artiste = new List<Artist>();


            artiste = daoart.GetArtistByName(CB_Artist.SelectedItem.ToString());

            MessageBox.Show("ID DE LARTISTE : " + artiste[0].IdArtist);


            

        }
    }
}

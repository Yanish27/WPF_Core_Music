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

namespace WPF_Music.View
{
    /// <summary>
    /// Logique d'interaction pour UI_Home.xaml
    /// </summary>
    public partial class UI_Home : UserControl
    {
        public UI_Home()
        {
            InitializeComponent();
            DAO_Album DaoAlb;
            DaoAlb = new DAO_Album();

            DAO_Artist DaoArt;
            DaoArt = new DAO_Artist();

            lbl_nb_art.Content = "Nous avons actuellement un catalogue de " + DaoArt.CountArtist().ToString() + " artistes pour un total de "
                + DaoAlb.CountAlbum().ToString() + " albums.";


        }
    }
}

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
    /// Logique d'interaction pour UI_Rating.xaml
    /// </summary>
    public partial class UI_Rating : UserControl
    {
        int alb_sel;
        DAO_Album daoalbum;
        DAORaiting daoraiting;
        public UI_Rating()
        {
            InitializeComponent();
            daoalbum = new DAO_Album();
            daoraiting = new DAORaiting();
            foreach (Album album in daoalbum.GetAlbum())
            {
                CB_Album.Items.Add(album.Titre);
            }
            CB_note.Items.Add("A");
            CB_note.Items.Add("B");
            CB_note.Items.Add("C");
            CB_note.Items.Add("D");

            NB_Alb_By_Rat.Content =
                "Grade A :" + daoraiting.CountAlbumByRaiting("A") + " albums " + " | " +
                "Grade B :" + daoraiting.CountAlbumByRaiting("B") + " albums ";
            NB_Alb_By_Rat2.Content =
                "Grade C : " + daoraiting.CountAlbumByRaiting("C") + " albums " + " | " +
                "Grade D : " + daoraiting.CountAlbumByRaiting("D") + " albums";  
            
        }

        private void btnNoteAlbum_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Album.SelectedItem != null && CB_note.SelectedItem != null)
            {
                daoraiting.DeleteRating(alb_sel);
                Rating raiting = new Rating();
                raiting.AlbumsIdAlbums = alb_sel;
                raiting.Grade = CB_note.SelectedItem.ToString();
                daoraiting.AddRaiting(raiting);
                MessageBox.Show("La note à bien été ajouté.");
            }
            else
            {
                MessageBox.Show("Merci de saisir un nom d'album et une note");
            }
        }

        private void CB_Album_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CB_note.SelectedIndex = -1;
            List<Album> alb = new List<Album>();

            // On met dans la variable alb la liste de Album
            alb = daoalbum.getAlbumByName(CB_Album.SelectedItem.ToString());

            // On prend volontairement le premier résultat qui nous est retourné.
            // alb_sel est une variable globale. Artiste Selectionné.
            alb_sel = alb[0].IdAlbums;

            // DEBUG : MessageBox.Show("ID DE L'ALBUM : " + alb_sel);

            var raiting = daoraiting.GetRaitingByID(alb_sel);
            if(raiting != "error")
            {
               if(raiting == "A"){
                    CB_note.SelectedItem = "A";
                }
                else if (raiting == "B")
                {
                    CB_note.SelectedItem = "B";
                }
                else if (raiting == "C")
                {
                    CB_note.SelectedItem = "C";
                }
                else if (raiting == "D")
                {
                    CB_note.SelectedItem = "D";
                }  else {
                    CB_note.SelectedIndex = -1;
                }
            } else {
                CB_note.SelectedIndex = -1;
            }
        }
    }
}

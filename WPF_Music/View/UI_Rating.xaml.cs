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
        // Initialisation des dao
        DAO_Album daoalbum;
        DAO_Artist daoartist;
        DAORaiting daoraiting;
        public UI_Rating()
        {
            InitializeComponent();
            // creation d'un objet de type DAO_Album
            daoalbum = new DAO_Album();
            // creation d'un objet de type DAO_Artist
            daoraiting = new DAORaiting();
            // On parcours la liste des albums
            foreach (Album album in daoalbum.GetAlbum())
            {
                // On ajoute un item a la combo box
                CB_Album.Items.Add(album.Titre);
            }
            // On ajoute les grades a la combo box
            CB_note.Items.Add("A");
            CB_note.Items.Add("B");
            CB_note.Items.Add("C");
            CB_note.Items.Add("D");
            // On affiche le nombre d'albums pour chaque grade
            NB_Alb_By_Rat.Content =
                "Grade A :" + daoraiting.CountAlbumByRaiting("A") + " albums " + " | " +
                "Grade B :" + daoraiting.CountAlbumByRaiting("B") + " albums ";
            NB_Alb_By_Rat2.Content =
                "Grade C : " + daoraiting.CountAlbumByRaiting("C") + " albums " + " | " +
                "Grade D : " + daoraiting.CountAlbumByRaiting("D") + " albums";

        }

        private void btnNoteAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Si un album est selectionné
            // Si une note est selectionnée
            if (CB_Album.SelectedItem != null && CB_note.SelectedItem != null)
            {
                // On supprime la note de l'album
                daoraiting.DeleteRating(alb_sel);
                // Creation d'un objet de type Rating
                Rating raiting = new Rating();
                // On recupere l'id de l'album
                //On attribut l'ID a l'album 
                raiting.AlbumsIdAlbums = alb_sel;
                // On attribut la note a l'album
                raiting.Grade = CB_note.SelectedItem.ToString();
                // On ajoute la note a la BDD
                daoraiting.AddRaiting(raiting);
                // Message de confirmation
                MessageBox.Show("La note à bien été ajouté.");
            }
            else
            {
                // Message d'erreur
                MessageBox.Show("Merci de saisir un nom d'album et une note");
            }
        }

        private void CB_Album_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On deselectionne la note
            CB_note.SelectedIndex = -1;
            // Création d'une liste d'albums
            List<Album> alb = new List<Album>();

            // On met dans la variable alb la liste de Album
            alb = daoalbum.getAlbumByName(CB_Album.SelectedItem.ToString());

            // On prend volontairement le premier résultat qui nous est retourné.
            // alb_sel est une variable globale. Artiste Selectionné.
            alb_sel = alb[0].IdAlbums;

            // DEBUG : MessageBox.Show("ID DE L'ALBUM : " + alb_sel);

            // On recupere la note de l'album
            var raiting = daoraiting.GetRaitingByID(alb_sel);
            if(raiting != "error")
            {
                //  Si la note est égale a A
               if(raiting == "A"){
                   // On selectionne la note A dans la combo box
                    CB_note.SelectedItem = "A";
                }
                // Si la note est égale a B
                else if (raiting == "B")
                {
                    // On selectionne la note B dans la combo box
                    CB_note.SelectedItem = "B";
                }
                // Si la note est égale a C
                else if (raiting == "C")
                {
                    // On selectionne la note C dans la combo box
                    CB_note.SelectedItem = "C";
                }
                // Si la note est égale a D
                else if (raiting == "D")
                {
                    // On selectionne la note D dans la combo box
                    CB_note.SelectedItem = "D";
                }
                else {
                    // On ne selectionne rien dans la combo box
                    CB_note.SelectedIndex = -1;
                }
            } else {
                // On ne selectionne rien dans la combo box
                CB_note.SelectedIndex = -1;
            }
        }
    }
}

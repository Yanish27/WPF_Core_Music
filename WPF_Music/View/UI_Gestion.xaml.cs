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
    /// Logique d'interaction pour UI_Gestion.xaml
    /// </summary>
    public partial class UI_Gestion : UserControl
    {
        int idArtiste;
         
        // Création d'une instance de la classe DAO_Artist
        DAO_Artist daoart;
        public UI_Gestion()
        {
            InitializeComponent();
            // Création d'un objet de type DAO_Artist
            daoart = new DAO_Artist();
            // On récupère la liste des artistes
            // On ajoute la liste des artistes à la datagrid
            DG_Artist.ItemsSource = daoart.GetArtists();
            
            // On active // desactive des bouttons
            btnModifier.IsEnabled = false;
            btnAjouter.IsEnabled = true;
            btnSuppr.IsEnabled = false;
            ExitSelect.IsEnabled = false;
        }

       

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        { 
            // DG_Artist.SelectedIndex < DG_Artist.Items.Count-1 Car sinon on peut selectionner une ligne VIDE et cela créer des erreurs
            if (DG_Artist.SelectedIndex != -1 && DG_Artist.SelectedIndex <= DG_Artist.Items.Count-1)
            {
                // Si le nom n'est pas vide
                if (TextB_nom.Text != "")
                {
                    // Si la description n'est pas vide
                    if (TextB_description.Text != "")
                    {
                        // Si l'année n'est pas vide
                        if (TextB_annee.Text != "")
                        {
                            // Creation d'un objet ArtistModif de type "Artist" via le constrcuteur Artist() dans la DAO
                            Artist ArtistModif = new Artist();
                
                            // On définit les caractéristiques de l'objet
                            ArtistModif.IdArtist = idArtiste;
                            ArtistModif.Name = TextB_nom.Text;
                            ArtistModif.Discription = TextB_description.Text;
                            ArtistModif.Annee = TextB_annee.Text;

                            // On execute la méthode UpdateArtist dans DAO Artist
                            daoart.UpdateArtist(ArtistModif);

                            // On réinitialise le DataGrid
                            DG_Artist.ItemsSource = null;

                            // On recharge le DataGrid
                            DG_Artist.ItemsSource = daoart.GetArtists();
                        }
                        else
                        {
                            // On affiche un message d'erreur
                            MessageBox.Show("L'année doit contenir uniquement des chiffres.", "Echec de la modification");
                        }
                    }
                    else
                    {
                        // On affiche un message d'erreur
                        MessageBox.Show("Merci de saisir une année", "Echec de la modification");
                    }
                }
                else
                {
                    // On affiche un message d'erreur
                    MessageBox.Show("Merci de saisir une description", "Echec de la modification");
                }
            }
            else
            {
                // On affiche un message d'erreur   
                MessageBox.Show("Merci de saisir un nom", "Echec de la modification");
            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Si le nom n'est pas vide
            if (TextB_nom.Text != "")
            {
                // Si la description n'est pas vide
                if (TextB_description.Text != "")
                {
                    // Si l'année n'est pas vide
                    if (TextB_annee.Text != "")
                    {
                        // Verification de l'année
                        bool verif_annee = Int32.TryParse(TextB_annee.Text, out int annee);

                        // Si l'année est bien un nombre
                        if(verif_annee == true)
                        {

                        // Creation d'un objet artiste de type "Artist" via le constrcuteur Artist() dans la DAO
                        Artist artiste = new Artist();
                    
                        // On définit les caractéristiques de l'objet
                        artiste.Name = TextB_nom.Text;
                        artiste.Discription = TextB_description.Text;

                        // On convertit l'année en string car c'est stocké comme ça dans la bdd
                        artiste.Annee = annee.ToString();
                        
                        // On execute la méthode AddArtist dans DAO Artist
                        // On recupere un message de retour
                        string Message = new DAO_Artist().AddArtist(artiste);
                        // On affiche le message de retour
                        MessageBox.Show(Message, "Ajout d'un artiste");

                        // On réinitialise le DataGrid
                        DG_Artist.ItemsSource = null;
                        
                        // On recharge le DataGrid
                        DG_Artist.ItemsSource = daoart.GetArtists();

                        // On vide les champs
                        resetTextBox();
                        }
                        else
                        {
                            // On affiche un message d'erreur
                            MessageBox.Show("L'année doit contenir uniquement des chiffres.", "Echec de l'ajout");
                        }
                    } else {
                        // On affiche un message d'erreur
                        MessageBox.Show("Merci de saisir une année", "Echec de l'ajout");
                    }
                }
                else
                {
                    // On affiche un message d'erreur
                    MessageBox.Show("Merci de saisir une description", "Echec de l'ajout");
                }
            }
            else
            {
                // On affiche un message d'erreur
                MessageBox.Show("Merci de saisir un nom", "Echec de l'ajout");
            }
        }

        private void DG_Artist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // DG_Artist.SelectedIndex < DG_Artist.Items.Count-1 Car sinon on peut selectionner une ligne VIDE et cela créer des erreurs
            if (DG_Artist.SelectedIndex != -1 && DG_Artist.SelectedIndex < DG_Artist.Items.Count-1)
            {
                // Création d'un objet Artist via le constrcuteur Artist() dans la DAO
                Artist artiste = DG_Artist.SelectedValue as Artist;
                btnAjouter.IsEnabled = false;
                btnModifier.IsEnabled = true;
                btnSuppr.IsEnabled = true;
                ExitSelect.IsEnabled = true;
                
                // On recupere l'id de l'artiste
                idArtiste = artiste.IdArtist;

                // On recupere le nom de l'artiste
                TextB_nom.Text = artiste.Name;

                // On recupere la description de l'artiste
                TextB_description.Text = artiste.Discription;

                // On recupere l'année de l'artiste
                TextB_annee.Text = artiste.Annee;

            }
        }

        private void ExitSelect_Click(object sender, RoutedEventArgs e)
        {
            ExitSelect_FN();
        }
       
        public void ExitSelect_FN()
        { 
            // On déselectionne la dataGrid
            DG_Artist.SelectedIndex = -1;
            resetTextBox();

            btnModifier.IsEnabled = false;
            btnAjouter.IsEnabled = true;
            btnSuppr.IsEnabled = false;
            ExitSelect.IsEnabled = false;
        }
        public void resetTextBox()
        {
            // On vide les champs
            TextB_nom.Text = TextB_description.Text = TextB_annee.Text = "";
        }

        /// <summary>
        /// Lorsque l'utilisateur clique sur le button "supprimer"
        /// </summary
        private void btnSuppr_Click(object sender, RoutedEventArgs e)
        {

            if (DG_Artist.SelectedIndex != -1)
            {

                MessageBox.Show("Erreur : L'artiste ne peut être supprimé.");
                /*
                 * #### La supression, des artistes ne fonctionne pas SI l'artiste possède au moins 1 album
                 * On ne peut pas supprimer une clé primaire d'une table qui est clé étrangère d'une autre table.
                 * 
                 * Solutions : 
                 * 1] Ajouter "ON DELETE CASCADE" dans la requête SQL. J'ai cherché, je n'ai pas trouvé comment ajouter cela
                 * 
                 * 2] Faire une fonction pour supprimer les albums par IDArtist : J'ai tenté quelque chose dans la fonction DeleteAlbumByArtistId 
                 * Dans DAO_Album
                 * 
                 * Je n'ai pas eu le temps de voir +, mais l'idée est là 
                  Artist artiste = DG_Artist.SelectedValue as Artist;
                  string y = new DAO_Album().DeleteAlbumByArtistId(artiste.IdArtist);
                  string r = new DAO_Artist().DeleteArtist(artiste.IdArtist);

                  MessageBox.Show(r, "Suppression d'un artiste");
                  DG_Artist.ItemsSource = null;
                  DG_Artist.ItemsSource = daoart.GetArtists();
                */
            }
        }

        private void TextB_description_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /// <summary>
        /// Pour que l'utilisateur ne puisse pas ajouter de ligne manuellement
        /// Il doit passer par le fomulaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_Artist_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            // On empeche l'utilisateur de rajouter de ligne manuellement
            MessageBox.Show("Merci de saisir les informations du nouvel artiste via le formulaire au dessus.", "Echec");

            // On empeche l'ajout de ligne
            DG_Artist.ItemsSource = daoart.GetArtists();

            // On deselectionne la ligne
            DG_Artist.SelectedIndex = -1;
            
            ExitSelect_FN();

        }
    }
}

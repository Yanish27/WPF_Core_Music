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

        DAO_Artist daoart;
        public UI_Gestion()
        {
            InitializeComponent();
            daoart = new DAO_Artist();
            DG_Artist.ItemsSource = daoart.GetArtists();
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
                if (TextB_nom.Text != "")
                {
                    if (TextB_description.Text != "")
                    {
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
                            MessageBox.Show("L'année doit contenir uniquement des chiffres.", "Echec de la modification");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Merci de saisir une année", "Echec de la modification");
                    }
                }
                else
                {
                    MessageBox.Show("Merci de saisir une description", "Echec de la modification");
                }
            }
            else
            {
                MessageBox.Show("Merci de saisir un nom", "Echec de la modification");
            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (TextB_nom.Text != "")
            {
                if (TextB_description.Text != "")
                {
                    if (TextB_annee.Text != "")
                    {
                        bool verif_annee = Int32.TryParse(TextB_annee.Text, out int annee);
                        if(verif_annee == true)
                        {

                        Artist artiste = new Artist();
                        artiste.Name = TextB_nom.Text;
                        artiste.Discription = TextB_description.Text;
                        artiste.Annee = annee.ToString();

                        string Message = new DAO_Artist().AddArtist(artiste);
                        MessageBox.Show(Message, "Ajout d'un artiste");

                        DG_Artist.ItemsSource = null;
                        DG_Artist.ItemsSource = daoart.GetArtists();
                        resetTextBox();
                        }
                        else
                        {
                            MessageBox.Show("L'année doit contenir uniquement des chiffres.", "Echec de l'ajout");
                        }
                    } else {
                        MessageBox.Show("Merci de saisir une année", "Echec de l'ajout");
                    }
                }
                else
                {
                    MessageBox.Show("Merci de saisir une description", "Echec de l'ajout");
                }
            }
            else
            {
                MessageBox.Show("Merci de saisir un nom", "Echec de l'ajout");
            }
        }

        private void DG_Artist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // DG_Artist.SelectedIndex < DG_Artist.Items.Count-1 Car sinon on peut selectionner une ligne VIDE et cela créer des erreurs
            if (DG_Artist.SelectedIndex != -1 && DG_Artist.SelectedIndex < DG_Artist.Items.Count-1)
            {
                Artist artiste = DG_Artist.SelectedValue as Artist;
                btnAjouter.IsEnabled = false;
                btnModifier.IsEnabled = true;
                btnSuppr.IsEnabled = true;
                ExitSelect.IsEnabled = true;

                idArtiste = artiste.IdArtist;
                TextB_nom.Text = artiste.Name;
                TextB_description.Text = artiste.Discription;
                TextB_annee.Text = artiste.Annee;

            }
        }

        private void ExitSelect_Click(object sender, RoutedEventArgs e)
        {
            ExitSelect_FN();
        }
       
        public void ExitSelect_FN()
        {
            DG_Artist.SelectedIndex = -1;
            resetTextBox();

            btnModifier.IsEnabled = false;
            btnAjouter.IsEnabled = true;
            btnSuppr.IsEnabled = false;
            ExitSelect.IsEnabled = false;
        }
        public void resetTextBox()
        {

            TextB_nom.Text = TextB_description.Text = TextB_annee.Text = "";
        }

        private void btnSuppr_Click(object sender, RoutedEventArgs e)
        {
            if (DG_Artist.SelectedIndex != -1)
            {
                Artist artiste = DG_Artist.SelectedValue as Artist;
                string r = new DAO_Artist().DeleteArtist(artiste.IdArtist);
                MessageBox.Show(r, "Suppression d'un artiste");
                DG_Artist.ItemsSource = null;
                DG_Artist.ItemsSource = daoart.GetArtists();
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
            MessageBox.Show("Merci de saisir les informations du nouvel artiste via le formulaire au dessus.", "Echec");

            DG_Artist.ItemsSource = daoart.GetArtists();
            DG_Artist.SelectedIndex = -1;

            ExitSelect_FN();

        }
    }
}

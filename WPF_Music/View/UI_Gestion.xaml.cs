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
            int idArtiste;
            if (DG_Artist.SelectedIndex != -1)
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
    }
}

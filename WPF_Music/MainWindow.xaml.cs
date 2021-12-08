using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using WPF_Music.View;

namespace WPF_Music
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        DAO_Artist daoart;
        public MainWindow()
        {
            InitializeComponent();
            // Création d'un objet appelée daoart depuis le constructeur DAO_Artist();
            daoart = new DAO_Artist();
           
        }

        private void BTN_Home_Click(object sender, RoutedEventArgs e)
        {
            // Si clic sur BTN_Home, remplacement de partiecentrale par la vue UI_Home() dans le dossier VUE : Modèle MVC
            PartieCentrale.Content = new View.UI_Home();
        }

        private void BTN_page_Gestion_Click(object sender, RoutedEventArgs e)
        {

            PartieCentrale.Content = new View.UI_Gestion();
        }

        private void BTN_page_Configuration_Click(object sender, RoutedEventArgs e)
        {

            PartieCentrale.Content = new View.UI_Configuration();
        }

        private void BTN_page_Info_Click(object sender, RoutedEventArgs e)
        {
            PartieCentrale.Content = new View.UI_Info();
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult mbr;
            mbr = MessageBox.Show("Êtes vous sur de vouloir quitter ?", "WPF Music", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}

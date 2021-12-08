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
        }
    }
}

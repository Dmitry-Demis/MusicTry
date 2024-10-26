using MusicTry.Windows;
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

namespace MusicTry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            // Открыть окно создания
            var createWindow = new CreateWindow();
            createWindow.ShowDialog();
        }

        private void OnArtistButtonClick(object sender, RoutedEventArgs e)
        {
            var artistWindow = new ArtistWindow();
            artistWindow.ShowDialog();
        }
    }
}
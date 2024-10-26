using System.Windows;

namespace MusicTry.Windows
{
    public partial class CreateWindow : Window
    {
        public CreateWindow()
        {
            InitializeComponent();
        }

        private void CreateArtistButton_Click(object sender, RoutedEventArgs e)
        {
          
            var createArtistWindow = new CreateArtist();
            createArtistWindow.ShowDialog();
        }


        private void CreateAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            var createArtistWindow = new CreateAlbum();
            createArtistWindow.ShowDialog(); 
        }

        private void CreatePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            var create = new CreatePlaylist();
            create.ShowDialog(); 
        }
    }
}

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
            // Получаем значения из полей
            string name = "Martin Garrix";

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Genre selectedGenre = new() { Name = "ds" };
            // Создаём и сохраняем артиста
            var artist = new ArtistBuilder()
                .SetName(name)
                .SetGenre(selectedGenre)
                .Build();
            var album = new AlbumBuilder()
                .SetTitle("Gello")
                .SetArtist(artist)


                .Build();

            await DatabaseService.SaveArtistAsync(artist);


            MessageBox.Show("Артист успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
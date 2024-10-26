using System.Windows;
using System.Threading.Tasks;

namespace MusicTry.Windows
{
    public partial class CreateArtist : Window
    {
        public CreateArtist()
        {
            InitializeComponent();
        }

        private async void OnCreateArtistClick(object sender, RoutedEventArgs e)
        {
            var artistName = ArtistNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(artistName))
            {
                MessageBox.Show("Имя артиста не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверяем, существует ли артист в БД
            var existingArtist = await DatabaseService.GetArtistByNameAsync(artistName);
            if (existingArtist != null)
            {
                MessageBox.Show($"Артист \"{artistName}\" с таким именем уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создание нового артиста с помощью ArtistBuilder
            var artistBuilder = new ArtistBuilder();
            var newArtist = artistBuilder
                .SetName(artistName)
                .Build();

            // Вставляем артиста в БД
            await DatabaseService.SaveArtistAsync(newArtist);
            MessageBox.Show($"Артист \"{artistName}\" успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Закрываем окно
            Close();
        }
    }
}

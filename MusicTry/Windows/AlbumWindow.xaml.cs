using System.Windows;
using System.Windows.Controls;

namespace MusicTry.Windows
{
    public partial class AlbumWindow : Window
    {
        private readonly Artist _artist;

        public AlbumWindow(Artist artist)
        {
            InitializeComponent();
            _artist = artist;
            Title = $"Альбомы исполнителя: {_artist.Name}";
            LoadAlbums();
        }

        private async void LoadAlbums()
        {
            try
            {
                // Получаем альбомы исполнителя из БД
                var albums = await DatabaseService.GetAlbumsByArtistAsync(_artist);
                AlbumListBox.ItemsSource = albums;

                // Устанавливаем выбор первого альбома, если он есть
                if (albums.Count > 0)
                {
                    AlbumListBox.SelectedIndex = 0; // Выбор первого альбома
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок (например, показать сообщение об ошибке)
                MessageBox.Show($"Ошибка загрузки альбомов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AlbumListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что выбран альбом
            if (AlbumListBox.SelectedItem is Album selectedAlbum)
            {
                try
                {
                    // Получаем список треков выбранного альбома
                    var tracks = await DatabaseService.GetTracksByAlbumAsync(selectedAlbum);

                    // Нумерация треков (проверка для избежания ошибок)
                    for (int i = 0; i < tracks.Count; i++)
                    {
                        tracks[i].Id = i + 1; // Устанавливаем номер трека
                    }

                    TrackDataGrid.ItemsSource = tracks; // Обновляем таблицу треков
                }
                catch (Exception ex)
                {
                    // Обработка ошибок (например, показать сообщение об ошибке)
                    MessageBox.Show($"Ошибка загрузки треков: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                TrackDataGrid.ItemsSource = null; // Сбрасываем таблицу, если ничего не выбрано
            }
        }
    }
}

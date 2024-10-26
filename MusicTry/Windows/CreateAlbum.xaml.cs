using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MusicTry.Windows
{
    public partial class CreateAlbum : Window
    {

        public CreateAlbum()
        {
            InitializeComponent();
            LoadArtists();
            LoadGenres();
        }

        private void LoadArtists()
        {
            var artists = DatabaseService.GetArtistsAsync().Result;
            ArtistComboBox.ItemsSource = artists;
            ArtistComboBox.DisplayMemberPath = "Name";
        }

        private void LoadGenres()
        {
            var genres = DatabaseService.GetGenresAsync().Result;
            GenreComboBox.ItemsSource = genres;
            GenreComboBox.DisplayMemberPath = "Name";
        }

        private Album _album;
        private void AddTrackButton_Click(object sender, RoutedEventArgs e)
        {
            if (TrackTitleTextBox.Text == string.Empty)
            {
                MessageBox.Show("Пожалуйста, введите название трека.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Валидация минут и секунд
            if (!double.TryParse(TrackMinutesTextBox.Text, out double minutes) || minutes < 0 || minutes > 59)
            {
                MessageBox.Show("Пожалуйста, введите действительное время трека в минутах (0-59).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(TrackSecondsTextBox.Text, out double seconds) || seconds < 0 || seconds > 59)
            {
                MessageBox.Show("Пожалуйста, введите действительное время трека в секундах (0-59).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Получение названия альбома из текстового поля
            string albumTitle = AlbumTitleTextBox.Text;

            // Проверка на наличие названия альбома
            if (string.IsNullOrWhiteSpace(albumTitle))
            {
                MessageBox.Show("Пожалуйста, введите название альбома.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ArtistComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, введите артиста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (GenreComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, введите жанр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создание альбома с использованием AlbumBuilder
            // Создание альбома с использованием AlbumBuilder
            _album = new AlbumBuilder()
                .SetTitle(albumTitle)
                .SetArtist((Artist)ArtistComboBox.SelectedItem)
                .SetGenre((Genre)GenreComboBox.SelectedItem)
                .SetReleaseDate(ReleaseDatePicker.SelectedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd")) // Преобразование даты в строку
                .Build();


            // Создание трека с использованием TrackBuilder
            var track = new TrackBuilder()
                .SetTitle(TrackTitleTextBox.Text)
                .SetDuration(minutes, seconds)
                .SetAlbum(_album) // Установка альбома, созданного с использованием AlbumBuilder
                .Build();

            // Проверка на наличие трека с тем же названием в DataGrid
            if (TracksDataGrid.Items.OfType<Track>().Any(t => t.Title.Equals(track.Title, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Трек с таким названием уже существует в списке.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            TracksDataGrid.Items.Add(track);
        }

        private void CreateAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            var tracks = TracksDataGrid.Items.OfType<Track>().ToList();
            _album.Tracks.AddRange(tracks);
            if (!tracks.Any())
            {
                MessageBox.Show("Пожалуйста, добавьте хотя бы один трек к альбому.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Сохранение альбома и треков в базе данных
            var success = DatabaseService.SaveAlbumAsync(_album);
            MessageBox.Show($"Альбом \"{_album.Title}\" успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

    }
}

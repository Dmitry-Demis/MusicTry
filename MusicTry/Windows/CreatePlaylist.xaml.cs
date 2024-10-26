using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicTry.Windows
{
    public class TrackSelected
    {
        public Track Track { get; set; }
        public bool IsSelected { get; set; } = false;

        public TrackSelected(Track track)
        {
            Track = track;
        }
    }

    public partial class CreatePlaylist : Window
    {
        private List<TrackSelected> _trackSelectedList = new List<TrackSelected>();

        public CreatePlaylist()
        {
            InitializeComponent();
            LoadTracks();
        }

        private async void LoadTracks()
        {
            try
            {
                var tracks = await DatabaseService.GetTracksAsync();
                _trackSelectedList = tracks.Select(t => new TrackSelected(t)).ToList();
                TracksDataGrid.ItemsSource = _trackSelectedList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке треков: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchTrackTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTrackTextBox.Text.ToLower();

            // Фильтрация с сохранением состояния IsSelected
            var filteredTracks = _trackSelectedList
                .Where(ts => ts.Track.Title.ToLower().Contains(searchText) ||
                             ts.Track.Album.Title.ToLower().Contains(searchText))
                .ToList();

            // Восстановление выбранных треков в фильтрованном списке
            foreach (var track in _trackSelectedList.Where(t => t.IsSelected))
            {
                var filteredTrack = filteredTracks.FirstOrDefault(ts => ts.Track.Id == track.Track.Id);
                if (filteredTrack != null)
                {
                    filteredTrack.IsSelected = true;
                }
            }

            TracksDataGrid.ItemsSource = filteredTracks;
        }


        private async void CreatePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            string playlistName = PlaylistNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(playlistName))
            {
                MessageBox.Show("Введите название плейлиста.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedTracks = _trackSelectedList
                .Where(ts => ts.IsSelected)
                .Select(ts => ts.Track)
                .ToList();

            if (selectedTracks.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один трек для создания плейлиста.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var compilation = new CompilationBuilder()
                .SetTitle(playlistName)
                .AddTracks(selectedTracks) // Предполагается, что этот метод будет реализован в CompilationBuilder
                .Build();

            // Сохранение плейлиста в базу данных
            try
            {
                await DatabaseService.SaveCompilationAsync(compilation);
                MessageBox.Show($"Плейлист '{playlistName}' создан с {selectedTracks.Count} треками.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении плейлиста: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SearchTrackTextBox_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TrackSelected trackSelected)
            {
                // Переключение состояния IsSelected
                trackSelected.IsSelected = checkBox.IsChecked ?? false;

                // Обновление источника данных для DataGrid, если нужно
                TracksDataGrid.Items.Refresh();
            }
        }

    }
}

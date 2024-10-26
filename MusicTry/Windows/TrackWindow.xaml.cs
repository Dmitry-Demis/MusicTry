using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MusicTry.Windows
{
    public partial class TrackWindow : Window
    {
        private List<Track> _allTracks;

        // Конструктор, принимающий плейлист
        public TrackWindow(Compilation playlist)
        {
            InitializeComponent();
            LoadTracks(playlist); // Загружаем треки из плейлиста
        }

        private void LoadTracks(Compilation playlist)
        {
            _allTracks = playlist.Tracks; // Получаем треки из переданного плейлиста
            TrackDataGrid.ItemsSource = _allTracks; // Устанавливаем источник данных для DataGrid
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            var filteredTracks = _allTracks
                .Where(track => track.Title.ToLower().Contains(searchText) ||
                                (track.Album?.Artist?.Name?.ToLower().Contains(searchText) ?? false) ||
                                (track.Album?.Title?.ToLower().Contains(searchText) ?? false) ||
                                track.Duration.ToString().Contains(searchText) ||
                                (track.Album?.Genre?.Name?.ToLower().Contains(searchText) ?? false))
                .ToList();

            TrackDataGrid.ItemsSource = filteredTracks;
        }

    }
}

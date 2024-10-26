using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MusicTry.Windows
{
    public partial class AlbumWindow : Window
    {
        private readonly List<Artist> _artists; // Хранит список артистов
        private readonly Artist _selectedArtist; // Хранит выбранного артиста

        public AlbumWindow(Artist artist)
        {
            InitializeComponent();
            _selectedArtist = artist;
            Title = $"Альбомы исполнителя: {_selectedArtist.Name}";
            LoadAlbumsForArtist();
        }

        public AlbumWindow(List<Artist> artists)
        {
            InitializeComponent();
            _artists = artists;
            Title = "Альбомы всех исполнителей";
            LoadAlbumsForAllArtists();
        }

        private async void LoadAlbumsForArtist()
        {
            try
            {
                var albums = await DatabaseService.GetAlbumsByArtistAsync(_selectedArtist);
                AlbumListBox.ItemsSource = albums;

                if (albums.Count > 0)
                {
                    AlbumListBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки альбомов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadAlbumsForAllArtists()
        {
            try
            {
                var albums = new List<Album>();
                foreach (var artist in _artists)
                {
                    var artistAlbums = await DatabaseService.GetAlbumsByArtistAsync(artist);
                    albums.AddRange(artistAlbums);
                }

                AlbumListBox.ItemsSource = albums;

                if (albums.Count > 0)
                {
                    AlbumListBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки альбомов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AlbumListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AlbumListBox.SelectedItem is Album selectedAlbum)
            {
                try
                {
                    var tracks = await DatabaseService.GetTracksByAlbumAsync(selectedAlbum);
                    for (int i = 0; i < tracks.Count; i++)
                    {
                        tracks[i].Id = i + 1;
                    }

                    TrackDataGrid.ItemsSource = tracks;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки треков: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                TrackDataGrid.ItemsSource = null;
            }
        }
    }
}

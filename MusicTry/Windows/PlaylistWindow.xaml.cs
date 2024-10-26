using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;

namespace MusicTry.Windows
{
    public partial class PlaylistWindow : Window
    {
        private static readonly Random Random = new Random();
        private List<Compilation> _allPlaylists; // Список всех плейлистов

        public PlaylistWindow()
        {
            InitializeComponent();
            LoadPlaylists();
        }

        private async void LoadPlaylists()
        {
            _allPlaylists = await DatabaseService.GetPlaylistsAsync(); // Предполагается, что есть метод для получения плейлистов

            UpdatePlaylistDisplay(_allPlaylists); // Отображаем все плейлисты сразу
        }

        private void PlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Compilation playlist)
            {
                var trackWindow = new TrackWindow(playlist);
                trackWindow.ShowDialog();
            }
        }

        private Color GetRandomColour()
        {
            // Генерация случайного цвета
            return Color.FromRgb(
                (byte)Random.Next(100, 256),
                (byte)Random.Next(100, 256),
                (byte)Random.Next(100, 256)
            );
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            // Фильтруем плейлисты по имени
            var filteredPlaylists = _allPlaylists
                .Where(playlist => playlist.Title.ToLower().Contains(searchText))
                .ToList();

            UpdatePlaylistDisplay(filteredPlaylists);
        }

        private void UpdatePlaylistDisplay(List<Compilation> playlists)
        {
            PlaylistWrapPanel.Children.Clear(); // Очищаем текущий список

            foreach (var playlist in playlists)
            {
                // Создаем кнопку для каждого плейлиста
                var playlistButton = new Button
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(GetRandomColour()),
                    Content = new TextBlock
                    {
                        Text = playlist.Title,
                        Foreground = Brushes.White,
                        FontSize = 14,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextWrapping = TextWrapping.Wrap,
                        TextAlignment = TextAlignment.Center
                    },
                    Tag = playlist // Сохраняем объект плейлиста в свойстве Tag, чтобы можно было к нему обратиться при нажатии
                };

                // Привязываем обработчик события клика
                playlistButton.Click += PlaylistButton_Click;

                PlaylistWrapPanel.Children.Add(playlistButton);
            }
        }
    }
}

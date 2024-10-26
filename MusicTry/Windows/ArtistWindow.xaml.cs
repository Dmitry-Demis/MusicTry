using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;

namespace MusicTry.Windows
{
    public partial class ArtistWindow : Window
    {
        private static readonly Random Random = new Random();
        private List<Artist> _allArtists;

        public ArtistWindow()
        {
            InitializeComponent();
            LoadArtists();
        }

        private async void LoadArtists()
        {
            _allArtists = await DatabaseService.GetArtistsAsync();

            UpdateArtistDisplay(_allArtists); // Отображаем всех артистов сразу
        }

        private void ArtistButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Artist artist)
            {
                // Открытие окна альбомов для выбранного артиста
                var albumWindow = new AlbumWindow(artist);
                albumWindow.ShowDialog();
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

            // Фильтруем артистов по имени
            var filteredArtists = _allArtists
                .Where(artist => artist.Name.ToLower().Contains(searchText))
                .ToList();

            UpdateArtistDisplay(filteredArtists);
        }

        private void UpdateArtistDisplay(List<Artist> artists)
        {
            ArtistWrapPanel.Children.Clear(); // Очищаем текущий список

            foreach (var artist in artists)
            {
                // Создаем кнопку для каждого артиста
                var artistButton = new Button
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(GetRandomColour()),
                    Content = new TextBlock
                    {
                        Text = artist.Name,
                        Foreground = Brushes.White,
                        FontSize = 14,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextWrapping = TextWrapping.Wrap,
                        TextAlignment = TextAlignment.Center,
                    },
                    Tag = artist // Сохраняем объект артиста в свойстве Tag, чтобы можно было к нему обратиться при нажатии
                };

                // Привязываем обработчик события клика
                artistButton.Click += ArtistButton_Click;

                ArtistWrapPanel.Children.Add(artistButton);
            }
        }
    }
}

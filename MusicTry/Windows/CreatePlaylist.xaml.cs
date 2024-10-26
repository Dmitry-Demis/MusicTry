using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicTry.Windows
{
    public class TrackSelected(Track track)
    {
        public Track Track { get; set; } = track;
        public bool IsSelected { get; set; } = false;
    }
    public partial class CreatePlaylist : Window
    {
        private List<TrackSelected> _trackSelectedList = new List<TrackSelected>(); // List to store tracks with selection state

        public CreatePlaylist()
        {
            InitializeComponent();

        }

        private void SearchTrackTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CreatePlaylistButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTrackTextBox_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }

}

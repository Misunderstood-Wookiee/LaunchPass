using System.ComponentModel;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;

namespace RetroPass
{
    public class PlaylistItem :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public Playlist Playlist { get; set; }
        public Game game;

        private BitmapImage image;
        public BitmapImage BitmapImage
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyyChanged("bitmapImage");
            }
        }
    }
}
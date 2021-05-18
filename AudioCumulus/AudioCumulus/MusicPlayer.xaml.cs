using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.Playlists;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AudioCumulus
{    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MusicPlayer : Page
    {
        private ObservableCollection<musicLibrary> MusicList = new ObservableCollection<musicLibrary>();
        public MusicPlayer()
        {
            this.InitializeComponent();
        }


        private async void musicPlayer_Click(object sender, RoutedEventArgs e)
        {
            await SetLocalMedia();
        }
    

        async private System.Threading.Tasks.Task SetLocalMedia()
        {
            var pickFile = new Windows.Storage.Pickers.FileOpenPicker();

            pickFile.FileTypeFilter.Add(".mp3");

            pickFile.SuggestedStartLocation = PickerLocationId.MusicLibrary;

            var file = await pickFile.PickSingleFileAsync();

            if (file != null)
            {
                musicPlayer.Source = MediaSource.CreateFromStorageFile(file);

                musicPlayer.MediaPlayer.Play();
            }
        }
    }
}

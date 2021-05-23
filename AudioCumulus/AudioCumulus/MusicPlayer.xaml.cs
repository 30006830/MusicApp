using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.FileProperties;
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
using Windows.UI.Core;

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
    
        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            musicPlayer.Source = null;
        }    

        async private System.Threading.Tasks.Task SetLocalMedia()
        {
            var pickFile = new FileOpenPicker();

            pickFile.FileTypeFilter.Add(".mp3");

            pickFile.SuggestedStartLocation = PickerLocationId.MusicLibrary;

            StorageFile file = await pickFile.PickSingleFileAsync();

            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                musicPlayer.SetSource(stream, file.ContentType);

                musicPlayer.Play();
            }
        }       
        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFolder musicLib = KnownFolders.MusicLibrary;
            var files = await musicLib.GetFilesAsync();

            foreach (var file in files)
            {
                var musicProperties = await file.Properties.GetMusicPropertiesAsync();
                var musicName = musicProperties.Title;
                var musicDur = musicProperties.Duration;

                if (musicName == "")
                {
                    musicName = "Name of File missing";
                }

                var artist = musicProperties.Artist;
                if (artist == "")
                {
                    artist = "Artist not found";
                }

                var album = musicProperties.Album;
                if (album == "")
                {
                    album = "Album not found";
                }

                MusicList.Add(new musicLibrary
                {
                    fileName = musicName,
                    Artist = artist,
                    Album = album,
                    Duration = musicDur,
                    MusicPath = file.Path
                });
            }
        }       

        private async void myList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var file = e.ClickedItem as StorageFile;
            if(file != null)
            {
                var stream = await file.OpenReadAsync();
                musicPlayer.SetSource(stream, file.ContentType);
            }            
        }
    }
}

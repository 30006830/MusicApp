using System;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
                    musicName = "Title of song not available";
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
            try
            {
                var clickedItem = e.ClickedItem as musicLibrary;
                if(clickedItem != null)
                {
                    var file = await KnownFolders.MusicLibrary.GetFileAsync(clickedItem.MusicPath);
                    if(file != null)
                    {
                        var stream = await file.OpenReadAsync();
                        musicPlayer.SetSource(stream, file.ContentType);
                        musicPlayer.Play();
                    }   
                }
            }
            catch (Exception)
            {
                string message = "If this error message appears then the function that has been attempted is not available, please use the open file button on the right of the application.";

                MessageDialog messageDialog = new MessageDialog(message, "Error");
                messageDialog.ShowAsync();
            }                                 
        }
    }
}

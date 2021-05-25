using System;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.Media.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AudioCumulus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MusicPlayer : Page
    {
        private ObservableCollection<musicLibrary> MusicList = new ObservableCollection<musicLibrary>();
        private MediaPlaybackList _mediaPlaybackList;
        private string file;

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
            Windows.Storage.FileProperties.MusicProperties musicProperties = await file.Properties.GetMusicPropertiesAsync();
        }       

        private async void myList_ItemClick(object sender, ItemClickEventArgs e)
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
    }
}

using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.Media.Core;
using Windows.Media.Playback;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AudioCumulus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoPlayer : Page
    {
        public VideoPlayer()
        {
            this.InitializeComponent();
        }
        
        private async void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await SetLocalMedia();
        }

        async private System.Threading.Tasks.Task SetLocalMedia()
        {
            var pickFile = new FileOpenPicker();

            pickFile.FileTypeFilter.Add(".mp4");
            pickFile.FileTypeFilter.Add(".wmv");
            pickFile.FileTypeFilter.Add(".mkv");            

            pickFile.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            StorageFile file = await pickFile.PickSingleFileAsync();

            if(file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                mediaElement.SetSource(stream, file.ContentType);

                mediaElement.Play();
            }

            
        }
    }
} 

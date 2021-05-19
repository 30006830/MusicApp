using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AudioCumulus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Container.Navigate(typeof(Home));
        }        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SplitViewMenu.IsPaneOpen == false)
            {
                SplitViewMenu.IsPaneOpen = true;
            }
            else if (SplitViewMenu.IsPaneOpen == true)
            {
                SplitViewMenu.IsPaneOpen = false;
            }
        }
        private void HomeIcon_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(typeof(Home));
        }

        private void Home_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Container.Navigate(typeof(Home));
        }

        private void MusicPlayerIcon_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(typeof(MusicPlayer));
            
        }

        private void MusicPlayer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Container.Navigate(typeof(MusicPlayer));
        }

        private void VideoPlayerIcon_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(typeof(VideoPlayer));
        }

        private void VideoPlayer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Container.Navigate(typeof(VideoPlayer));
        }
    }
}

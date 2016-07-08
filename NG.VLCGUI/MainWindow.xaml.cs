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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vlc.DotNet.Wpf;
using Vlc.DotNet.Forms;
using Vlc.DotNet.Core;
//using Vlc.DotNet.Core.Medias;
using Vlc.DotNet.Core.Interops;
using System.Reflection;
using System.IO;
using YoutubeExtractor;

namespace NG.VLCGUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myControl.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;

            myControl.MediaPlayer.EndReached += OnVlcEndReached;
            myControl.MediaPlayer.EndInit();

        }

        private void OnVlcEndReached(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {
            //throw new NotImplementedException();
            MessageBox.Show("Song beendet");
        }

        private void OnVlcControlNeedsLibDirectory(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return;
            if (AssemblyName.GetAssemblyName(currentAssembly.Location).ProcessorArchitecture == ProcessorArchitecture.X86)
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, @"C:\Program Files (x86)\VideoLAN\VLC"));
            else
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, @"C:\Program Files (x86)\VideoLAN\VLC"));
        }

        

        private void button_PlayClick(object sender, RoutedEventArgs e)
        {
            // Direktlink
            // myControl.MediaPlayer.Play(new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi"));

            string link = "https://www.youtube.com/watch?v=0sB3Fjw3Uvc";
            //string link = "https://www.youtube.com/watch?v=0sB3Fjw3Uvc";

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
            VideoInfo video = videoInfos.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
            myControl.MediaPlayer.Play(new Uri(video.DownloadUrl));

        }

        private void button_StopClick(object sender, RoutedEventArgs e)
        {
            myControl.MediaPlayer.Stop();
        }
    }
}

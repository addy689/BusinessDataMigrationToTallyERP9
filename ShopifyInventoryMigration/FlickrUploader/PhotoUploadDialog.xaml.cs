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
using FlickrNet;

namespace FlickrUploader
{
    /// <summary>
    /// Interaction logic for ImageUploadDialog.xaml
    /// </summary>
    public partial class PhotoUploadDialog : Window
    {
        private IProgress<string> progress;
        public PhotoUploadDialog()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var photoPath = @"C:\Users\addy6\Pictures\red_mens_cool_leather_autumn_outwear_jackets_vests_5.jpg";

            //create progress class that will update the UI (on the main UI thread, i.e., Dispatcher.Invoke thread) 
            //through a callback that we provide as argument
            progress = new Progress<string>( value => progressDisplayCtl.Content = value);

            uploadBtn.IsEnabled = false;
            string photoId = await UploadPhotoAsync(photoPath);

            uploadBtn.IsEnabled = true;

            string photoUrl = GetPublicPhotoUrl(photoId);
            photoUrlCtl.Text = photoUrl;
        }

        private async Task<string> UploadPhotoAsync(string photoPath)
        {
            var f = FlickrManager.GetAuthInstance();

            f.OnUploadProgress += new EventHandler<UploadProgressEventArgs>(FlickrOnUploadProgress);

            string fileName = photoPath;
            string title = "Sample";
            string desc = "";
            string tags = null;
            bool isPublic = true;
            bool isFamily = false;
            bool isFriend = false;

            string photoId = await Task<string>.Run( () => f.UploadPicture(fileName, title, desc, tags, isPublic, isFamily, isFriend) );

            return photoId;
        }

        public void FlickrOnUploadProgress(object sender, UploadProgressEventArgs e)
        {
            progress.Report(e.ProcessPercentage + "%");
        }

        private string GetPublicPhotoUrl(string photoId)
        {
            var f = FlickrManager.GetAuthInstance();

            PhotoInfo photoInfo = f.PhotosGetInfo(photoId);

            string farmId = photoInfo.Farm;
            string serverId = photoInfo.Server;
            string secret = photoInfo.Secret;

            return $"https://farm{farmId}.staticflickr.com/{serverId}/{photoId}_{secret}.jpg";
        }

    }
}

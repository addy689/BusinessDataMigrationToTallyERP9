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
using FlickrUploader;

namespace ShopifyInventoryMigration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClickBtnUploadAuthCtl(object sender, RoutedEventArgs e)
        {
            //this.container.Children.Clear();
            //this.container.Children.Add();
            
            var authDialog = new AuthenticationDialog();
            authDialog.ShowDialog();
        }

        private void OnClickBtnUploadImageCtl(object sender, RoutedEventArgs e)
        {
            if (!FlickrManager.IsAccessTokenPresent())
            {
                MessageBox.Show("You must authenticate before you can upload a photo.");
                return;
            }
                
            var photoUploadDialog = new PhotoUploadDialog();
            photoUploadDialog.ShowDialog();
        }
    }
}

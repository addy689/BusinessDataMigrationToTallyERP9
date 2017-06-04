using System.Windows;
using FlickrNet;

namespace FlickrUploader
{
    /// <summary>
    /// Interaction logic for AuthorizationDialog.xaml
    /// </summary>
    public partial class AuthenticationDialog : Window
    {
        private OAuthRequestToken requestToken;
        public AuthenticationDialog()
        {
            InitializeComponent();
        }

        private void OnClickLinkAuthenticate(object sender, RoutedEventArgs e)
        {
            Flickr f = FlickrManager.GetInstance();

            //Obtain request token from Flickr OAuth server
            requestToken = f.OAuthGetRequestToken("oob");

            //Use request token to obtain a url to direct the user to where they can login securely
            string url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Write);

            //launch url in browser
            System.Diagnostics.Process.Start(url);

            //Enable Step2
            step2Ctl.IsEnabled = true;
        }

        private void OnClickLinkComplete(object sender, RoutedEventArgs e)
        {
            string verifierCode = verifierCodeCtl.Text;

            if (string.IsNullOrEmpty(verifierCode))
            {
                MessageBox.Show("Please paste the 'Verification Code' into the textbox above!");
                return;
            }

            Flickr f = FlickrManager.GetInstance();

            try
            {
                var accessToken = f.OAuthGetAccessToken(this.requestToken, verifierCode);
                FlickrManager.OAuthToken = accessToken;

                resultCtl.Text = $"Successfully authenticated as {accessToken.FullName.ToUpper()}!\nPlease close this dialog";


            }
            catch (FlickrApiException ex)
            {
                MessageBox.Show("Failed to get access token! Error Message: " + ex.Message);
            }
        }

    }
}

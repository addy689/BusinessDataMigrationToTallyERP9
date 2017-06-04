using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;

namespace FlickrUploader
{
    public class FlickrManager
    {
        private const string apiKey = "f02c6fe252e08336c4ef781b60a782f5";
        private const string secret = "444135d7f65efabb";

        public static OAuthAccessToken OAuthToken
        { get; set; }

        public static Flickr GetInstance()
        {
            return new Flickr(apiKey, secret);
        }

        public static Flickr GetAuthInstance()
        {
            var f = new Flickr(apiKey, secret);
            f.OAuthAccessToken = OAuthToken.Token;
            f.OAuthAccessTokenSecret = OAuthToken.TokenSecret;

            return f;
        }

        public static bool IsAccessTokenPresent()
        {
            if (OAuthToken == null || OAuthToken.Token == null)
                return false;

            return true;
        }
    }
    
    
}

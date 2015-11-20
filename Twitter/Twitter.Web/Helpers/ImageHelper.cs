using System;
using System.Web;

namespace Twitter.Web.Helpers
{
    public class ImageHelper
    {
        public static IHtmlString ShowImage(byte[] image, string id)
        {
            var base64 = Convert.ToBase64String(image);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

            string div = String.Format("<div id='{0}' style='background-image: url({1})'></div>", id, imgSrc);
            return new HtmlString(div);
        }
    }
}
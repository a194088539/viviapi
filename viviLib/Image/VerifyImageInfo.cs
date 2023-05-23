using System.Drawing;
using System.Drawing.Imaging;

namespace viviLib.Image
{
    public class VerifyImageInfo
    {
        private string contentType = "image/pjpeg";
        private ImageFormat imageFormat = ImageFormat.Jpeg;
        private Bitmap image;

        public Bitmap Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
            }
        }

        public string ContentType
        {
            get
            {
                return this.contentType;
            }
            set
            {
                this.contentType = value;
            }
        }

        public ImageFormat ImageFormat
        {
            get
            {
                return this.imageFormat;
            }
            set
            {
                this.imageFormat = value;
            }
        }
    }
}

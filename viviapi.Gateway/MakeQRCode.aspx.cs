namespace viviapi.gateway
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using ThoughtWorks.QRCode.Codec;

    public class MakeQRCode : Page
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Request.QueryString["data"]))
            {

                string content = base.Request.QueryString["data"].Replace("qfand", "&");
                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                encoder.QRCodeVersion = 0;
                encoder.QRCodeScale = 4;
                Bitmap bitmap = encoder.Encode(content, Encoding.Default);
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Gif);
                base.Response.BinaryWrite(stream.GetBuffer());
                base.Response.End();
            }
        }
    }
}


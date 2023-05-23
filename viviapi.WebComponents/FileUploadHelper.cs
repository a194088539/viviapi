using System.IO;
using System.Web.UI.WebControls;

namespace viviapi.WebComponents
{
    public class FileUploadHelper
    {
        public static bool IsAllowedExtension(FileUpload hifile)
        {
            if (!hifile.HasFile)
                return false;
            FileStream fileStream = new FileStream(hifile.PostedFile.FileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader((Stream)fileStream);
            string str = "";
            try
            {
                str = binaryReader.ReadByte().ToString();
                byte num = binaryReader.ReadByte();
                str += num.ToString();
            }
            catch
            {
            }
            binaryReader.Close();
            fileStream.Close();
            return str == "255216" || str == "7173";
        }
    }
}

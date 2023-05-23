using System.IO;
using System.Net;
using System.Text;

namespace viviapi.BLL.Order.Notify
{
    public class RequestState
    {
        private const int BUFFER_SIZE = 1024;
        public StringBuilder requestData;
        public string url;
        public byte[] BufferRead;
        public HttpWebRequest request;
        public HttpWebResponse response;
        public Stream streamResponse;
        public int orderclass;
        public object order;

        public RequestState()
        {
            this.BufferRead = new byte[1024];
            this.requestData = new StringBuilder("");
            this.request = (HttpWebRequest)null;
            this.streamResponse = (Stream)null;
        }
    }
}

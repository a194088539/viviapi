using System;

namespace viviapi.BLL.Tools
{
    [Serializable]
    public class IdcardInfo
    {
        public string code { get; set; }

        public string location { get; set; }

        public string birthday { get; set; }

        public string gender { get; set; }

        public string fullname { get; set; }
    }
}

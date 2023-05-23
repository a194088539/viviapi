using System;

namespace viviapi.WebUI.LongBao.merchant.Ajax
{
    [Serializable]
    public class GetUserInfoResult
    {
        public int result { get; set; }

        public string username { get; set; }

        public string name { get; set; }

        public string msg { get; set; }
    }
}

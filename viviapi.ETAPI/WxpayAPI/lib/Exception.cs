using System;

namespace viviapi.ETAPI.WxPayAPI
{
    public class WxPayException : Exception
    {
        public WxPayException(string msg)
          : base(msg)
        {
        }
    }
}

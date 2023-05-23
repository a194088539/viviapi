using System;

namespace viviapi.Model.Channel
{
    [Serializable]
    public enum ChannelClassEnum
    {
        在线支付 = 1,
        充值卡 = 2,
        声讯 = 4,
        短信 = 8,
        代付款 = 16,
        手机网银 = 6,
        手机微信 = 10,
        手机支付宝 = 9,
    }
}

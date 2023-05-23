using System;
using System.Security.Cryptography;
using System.Text;

namespace viviapi.Model.Order
{
    [Serializable]
    public class OrderCardInfo : OrderBase
    {
        private Decimal _faceValue = new Decimal(0);
        private byte _cardversion = (byte)1;

        public int cardType { get; set; }

        public string cardNo { get; set; }

        public string cardPwd { get; set; }

        public string resultcode { get; set; }

        public int cardnum { get; set; }

        public int ismulticard { get; set; }

        public string ovalue { get; set; }

        public string Desc { get; set; }

        public byte method { get; set; }

        public Decimal faceValue
        {
            get
            {
                return this._faceValue;
            }
            set
            {
                this._faceValue = value;
            }
        }

        public byte cardversion
        {
            get
            {
                return this._cardversion;
            }
            set
            {
                this._cardversion = value;
            }
        }

        public byte withhold_type { get; set; }

        public Decimal withholdAmt { get; set; }

        public byte makeup { get; set; }

        public string userViewMsg { get; set; }

        public string returnopstate
        {
            get
            {
                if (string.IsNullOrEmpty(this.opstate))
                    return "opstate=-1";
                StringBuilder stringBuilder = new StringBuilder();
                string opstate = this.opstate;
                char[] chArray1 = new char[1]
                {
          '|'
                };
                foreach (string str in opstate.Split(chArray1))
                {
                    char[] chArray2 = new char[1]
                    {
            ':'
                    };
                    string[] strArray = str.Split(chArray2);
                    if (strArray.Length == 2)
                    {
                        if (stringBuilder.Length == 0)
                            stringBuilder.AppendFormat("opstate={0}", (object)strArray[1]);
                        else
                            stringBuilder.AppendFormat(",opstate={0}", (object)strArray[1]);
                    }
                }
                return stringBuilder.ToString();
            }
        }

        public string returnovalue
        {
            get
            {
                if (string.IsNullOrEmpty(this.opstate))
                    return string.Empty;
                if (this.ovalue.EndsWith(","))
                    return this.ovalue.Substring(0, this.ovalue.Length - 1);
                return this.ovalue;
            }
        }

        public OrderCardInfo()
        {
            this.makeup = (byte)0;
        }

        public OrderCardInfo(string serverId, string userId, string chanel)
        {
            this.orderid = DateTime.Now.ToString("yyMMddHHmmssff") + "0" + new Random(this.GetRandomSeed(serverId + userId + chanel)).Next(1000).ToString("0000");
        }

        private int GetRandomSeed(string factor)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(factor);
            new RNGCryptoServiceProvider().GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}

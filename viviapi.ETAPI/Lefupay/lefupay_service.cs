using System.Collections.Generic;
using System.Text;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.ETAPI.Lefupay
{
    public class lefupay_service
    {
        private int suppId = 1012;
        private SupplierInfo _suppInfo = (SupplierInfo)null;
        private string gateway = "";
        private string mysign = "";
        private Dictionary<string, string> sPara = new Dictionary<string, string>();

        private SupplierInfo suppInfo
        {
            get
            {
                if (this._suppInfo == null)
                    this._suppInfo = SupplierFactory.GetCacheModel(this.suppId);
                return this._suppInfo;
            }
        }

        public lefupay_service()
        {
        }

        public lefupay_service(string partner, string inputCharset, string notifyURL, string redirectURL, string signType, string apiCode, string versionCode, string buyer, string paymentType, string outOrderId, string amount, string buyerContactType, string buyerContact, string key, string interfaceCode, string retryFalg, string submitTime, string timeout, string clientIP, string MyCurrency)
        {
            this.gateway = "https://pay.lefu8.com/gateway/trade.htm?";
            key = key.Trim();
            this.sPara = lefupay_function.Para_filter(new SortedDictionary<string, string>()
      {
        {
          "partner",
          partner
        },
        {
          "inputCharset",
          inputCharset
        },
        {
          "notifyURL",
          notifyURL
        },
        {
          "redirectURL",
          redirectURL
        },
        {
          "signType",
          signType
        },
        {
          "apiCode",
          apiCode
        },
        {
          "versionCode",
          versionCode
        },
        {
          "outOrderId",
          outOrderId
        },
        {
          "buyer",
          buyer
        },
        {
          "paymentType",
          paymentType
        },
        {
          "amount",
          amount
        },
        {
          "buyerContactType",
          buyerContactType
        },
        {
          "buyerContact",
          buyerContact
        },
        {
          "interfaceCode",
          interfaceCode
        },
        {
          "retryFalg",
          retryFalg
        },
        {
          "submitTime",
          submitTime
        },
        {
          "timeout",
          timeout
        },
        {
          "clientIP",
          clientIP
        },
        {
          "currency",
          MyCurrency
        }
      });
            this.mysign = lefupay_function.Build_mysign(this.sPara, key, signType, inputCharset).ToUpper();
        }

        public string Build_Form()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<form id=\"lefupaysubmit\" name=\"lefupaysubmit\" action=\"" + this.gateway + "\" method=\"get\">");
            foreach (KeyValuePair<string, string> keyValuePair in this.sPara)
                stringBuilder.Append("<input type=\"hidden\" name=\"" + keyValuePair.Key + "\" value=\"" + keyValuePair.Value + "\"/>");
            stringBuilder.Append("<input type=\"hidden\" name=\"sign\" value=\"" + this.mysign + "\"/>");
            stringBuilder.Append("<input type=\"submit\" value=\"乐富支付确认付款\"></form>");
            stringBuilder.Append("<script>document.forms['lefupaysubmit'].submit();</script>");
            return stringBuilder.ToString();
        }
    }
}

using System.Collections.Generic;
using System.Text;

namespace TestEasy
{
    public class Easypay_service
    {
        private string gateway = "";
        private string _key = "";
        private string _input_charset = "";
        private string _sign_type = "";
        private string mysign = "";
        private Dictionary<string, string> sPara = new Dictionary<string, string>();

        public Easypay_service()
        {
        }

        public Easypay_service(string partner, string seller_email, string return_url, string notify_url, string out_trade_no, string subject, string body, string total_fee, string paymethod, string defaultbank, string buyer_email, string key, string input_charset, string sign_type)
        {
            this.gateway = "http://cashier.easyebank.com/portal?";
            this._key = key.Trim();
            this._input_charset = input_charset.ToLower();
            this._sign_type = sign_type.ToUpper();
            this.sPara = Easypay_function.Para_filter(new SortedDictionary<string, string>()
      {
        {
          "service",
          "create_direct_pay_by_user"
        },
        {
          "payment_type",
          "1"
        },
        {
          "partner",
          partner
        },
        {
          "seller_email",
          seller_email
        },
        {
          "return_url",
          return_url
        },
        {
          "notify_url",
          notify_url
        },
        {
          "_input_charset",
          this._input_charset
        },
        {
          "out_trade_no",
          out_trade_no
        },
        {
          "subject",
          subject
        },
        {
          "body",
          body
        },
        {
          "total_fee",
          total_fee
        },
        {
          "paymethod",
          paymethod
        },
        {
          "defaultbank",
          defaultbank
        },
        {
          "buyer_email",
          buyer_email
        }
      });
            this.mysign = Easypay_function.Build_mysign(this.sPara, this._key, this._sign_type, this._input_charset);
        }

        public string Easypay_query(string partner, string out_trade_no, string key, string input_charset, string sign_type)
        {
            this._key = key.Trim();
            this._input_charset = input_charset.ToLower();
            this._sign_type = sign_type.ToUpper();
            this.sPara = Easypay_function.Para_filter(new SortedDictionary<string, string>()
      {
        {
          "partner",
          partner
        },
        {
          "_input_charset",
          this._input_charset
        },
        {
          "out_trade_no",
          out_trade_no
        }
      });
            this.mysign = Easypay_function.Build_mysign(this.sPara, this._key, this._sign_type, this._input_charset);
            return this.mysign;
        }

        public string Easypay_refund(string partner, string orig_out_trade_no, string out_trade_no, string subject, string amount, string key, string input_charset, string sign_type)
        {
            this._key = key.Trim();
            this._input_charset = input_charset.ToLower();
            this._sign_type = sign_type.ToUpper();
            this.sPara = Easypay_function.Para_filter(new SortedDictionary<string, string>()
      {
        {
          "partner",
          partner
        },
        {
          "_input_charset",
          this._input_charset
        },
        {
          "out_trade_no",
          out_trade_no
        },
        {
          "orig_out_trade_no",
          orig_out_trade_no
        },
        {
          "amount",
          amount
        },
        {
          "subject",
          subject
        }
      });
            this.mysign = Easypay_function.Build_mysign(this.sPara, this._key, this._sign_type, this._input_charset);
            return this.mysign;
        }

        public string Build_Form()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<form id=\"easypaysubmit\" name=\"easypaysubmit\" action=\"" + this.gateway + "_input_charset=" + this._input_charset + "\" method=\"get\">");
            foreach (KeyValuePair<string, string> keyValuePair in this.sPara)
                stringBuilder.Append("<input type=\"hidden\" name=\"" + keyValuePair.Key + "\" value=\"" + keyValuePair.Value + "\"/>");
            stringBuilder.Append("<input type=\"hidden\" name=\"sign\" value=\"" + this.mysign + "\"/>");
            stringBuilder.Append("<input type=\"hidden\" name=\"sign_type\" value=\"" + this._sign_type + "\"/>");
            stringBuilder.Append("<input type=\"submit\" value=\"易生支付确认付款\"></form>");
            stringBuilder.Append("<script>document.forms['easypaysubmit'].submit();</script>");
            return stringBuilder.ToString();
        }
    }
}

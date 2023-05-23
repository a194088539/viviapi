using System;
using System.Text;
using viviapi.BLL.User;
using viviLib.Text;
using viviLib.Web;

namespace viviapi.BLL.Withdraw
{
    public class notifyHelper
    {
        private viviapi.Model.Withdraw.settledAgent _model = (viviapi.Model.Withdraw.settledAgent)null;

        public viviapi.Model.Withdraw.settledAgent model
        {
            get
            {
                return this._model;
            }
            set
            {
                this._model = value;
            }
        }

        public void DoNotify()
        {
            if (this.model == null || !PageValidate.IsUrl(this.model.return_url) || this.model.is_cancel)
                return;
            viviapi.Model.Withdraw.settledAgentNotify model = new viviapi.Model.Withdraw.settledAgentNotify();
            model.userid = this.model.userid;
            model.trade_no = this.model.trade_no;
            model.out_trade_no = this.model.out_trade_no;
            string notifyId = model.notify_id;
            string service = this.model.service;
            string inputCharset = this.model.input_charset;
            string str1 = this.model.userid.ToString();
            string signType = this.model.sign_type;
            string str2 = model.addTime.ToString("yyyyMMddHHmmss");
            string outTradeNo = this.model.out_trade_no;
            string str3 = "0.00";
            int num1 = 1;
            if (this.model.is_cancel)
            {
                num1 = 1;
            }
            else
            {
                if (this.model.audit_status == 1)
                    num1 = 1;
                else if (this.model.audit_status == 2)
                    num1 = 0;
                else if (this.model.audit_status == 3)
                    num1 = 2;
                if (this.model.audit_status == 2)
                {
                    if (this.model.payment_status == 2)
                    {
                        num1 = 3;
                        str3 = this.model.amount.ToString("f2");
                    }
                    if (this.model.payment_status == 3)
                        num1 = 4;
                }
            }
            string str4 = num1.ToString();
            string str5 = string.Empty;
            string str6 = CommonHelper.BuildParamString(CommonHelper.BubbleSort(new string[10]
            {
        "service=" + service,
        "input_charset=" + inputCharset,
        "partner=" + str1,
        "sign_type=" + signType,
        "notify_id=" + notifyId,
        "notify_time=" + str2,
        "out_trade_no=" + outTradeNo,
        "trade_status=" + str4,
        "error_message=" + str5,
        "amount_str=" + str3
            }));
            string userApiKey = UserFactory.GetUserApiKey(this.model.userid);
            string str7 = CommonHelper.md5(inputCharset, str6 + userApiKey).ToLower();
            string returnUrl = this.model.return_url;
            string postData = string.Format("service={0}", (object)service) + string.Format("&input_charset={0}", (object)inputCharset) + string.Format("&partner={0}", (object)str1) + string.Format("&sign_type={0}", (object)signType) + string.Format("&notify_id={0}", (object)notifyId) + string.Format("&notify_time={0}", (object)str2) + string.Format("&out_trade_no={0}", (object)outTradeNo) + string.Format("&trade_status={0}", (object)str4) + string.Format("&error_message={0}", (object)str5) + string.Format("&amount_str={0}", (object)str3) + string.Format("&sign={0}", (object)str7);
            try
            {
                string @string = WebClientHelper.GetString(returnUrl, postData, "get", Encoding.GetEncoding(inputCharset), 10000);
                model.resTime = new DateTime?(DateTime.Now);
                int num2 = !(@string == notifyId) ? 0 : 2;
                model.notifyurl = returnUrl;
                model.resText = @string;
                model.notifystatus = num2;
            }
            catch (Exception ex)
            {
                model.notifyurl = returnUrl;
                model.resText = "";
                model.notifystatus = 0;
                model.remark = ex.Message;
                model.resTime = new DateTime?(DateTime.Now);
            }
            new settledAgentNotify().Add(model);
        }
    }
}

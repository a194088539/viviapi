using System;
using System.Web;
using viviapi.BLL;
using viviapi.BLL.Settled;
using viviapi.BLL.User;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib.Security;
using viviLib.Text;

namespace viviapi.WebComponents.Web
{
    public class AgentPageBase : PageBase
    {
        public UsersAmtInfo _currentUserAmt = (UsersAmtInfo)null;

        public UserInfo currentUser
        {
            get
            {
                return UserFactory.CurrentMember;
            }
        }

        public UsersAmtInfo currentUserAmt
        {
            get
            {
                if (this._currentUserAmt == null && this.UserId > 0)
                    this._currentUserAmt = UsersAmt.GetModel(this.UserId);
                return this._currentUserAmt;
            }
        }

        public Decimal balance
        {
            get
            {
                Decimal num = new Decimal(0);
                if (this.currentUserAmt != null && this.currentUserAmt.balance.HasValue)
                    num = this.currentUserAmt.balance.Value;
                return num;
            }
        }

        public Decimal enableAmt
        {
            get
            {
                Decimal num = new Decimal(0);
                if (this.currentUserAmt != null)
                    num = this.currentUserAmt.enableAmt;
                return num;
            }
        }

        public Decimal unpayment
        {
            get
            {
                Decimal num = new Decimal(0);
                if (this.currentUserAmt != null && this.currentUserAmt.unpayment.HasValue)
                    num = this.currentUserAmt.unpayment.Value;
                return num;
            }
        }

        public Decimal Freeze
        {
            get
            {
                Decimal num = new Decimal(0);
                if (this.currentUserAmt != null && this.currentUserAmt.Freeze.HasValue)
                    num = this.currentUserAmt.Freeze.Value;
                return num;
            }
        }

        public Decimal TodayIncome
        {
            get
            {
                return Trade.GetUserIncome(this.UserId, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")), DateTime.Now.AddDays(1.0));
            }
        }

        public Decimal YesterdayIncome
        {
            get
            {
                return Trade.GetUserIncome(this.UserId, DateTime.Now.AddDays(-1.0), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            }
        }

        public int GetUserNum
        {
            get
            {
                return PromotionUserFactory.GetUserNum(this.UserId);
            }
        }

        public int SettlesMode
        {
            get
            {
                return this.currentUser.Settles;
            }
        }

        public Decimal WeekIncome
        {
            get
            {
                DateTime dateTime = DateTime.Now;
                dateTime = dateTime.AddDays(-7.0);
                return Trade.GetUserIncome(this.UserId, Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd 00:00:00")), DateTime.Now.AddDays(1.0));
            }
        }

        public int UserId
        {
            get
            {
                return this.currentUser.ID;
            }
        }

        public bool IsLogin
        {
            get
            {
                return this.currentUser != null;
            }
        }

        public string UserViewEmail
        {
            get
            {
                return Strings.Mark(this.currentUser.Email, '@');
            }
        }

        public string UserViewTel
        {
            get
            {
                return Strings.Mark(this.currentUser.Tel);
            }
        }

        public string UserViewBankAccout
        {
            get
            {
                return Strings.ReplaceString(this.currentUser.Account, 4, "*");
            }
        }

        public string UserViewIdCard
        {
            get
            {
                return Strings.Mark(this.currentUser.IdCard);
            }
        }

        public string UserFullName
        {
            get
            {
                return UserFactory.CurrentMember.full_name;
            }
        }

        public UsersUpdateLog newUpdateLog(int userid, string f, string n, string o)
        {
            return new UsersUpdateLog()
            {
                userid = userid,
                Addtime = DateTime.Now,
                field = f,
                newvalue = n,
                oldValue = o
            };
        }

        public string getUserTitle(string subPageTitle)
        {
            return string.Format("{1}- {0}", (object)this.SiteName, (object)subPageTitle);
        }

        public static string GetViewStatusName(object status)
        {
            if (status == DBNull.Value)
                return string.Empty;
            if (Convert.ToInt32(status) == 8)
                return "失败";
            return Enum.GetName(typeof(OrderStatusEnum), status);
        }

        public static string GetViewSuccessAmt(object status, object amt)
        {
            if (status == DBNull.Value || amt == DBNull.Value || Convert.ToInt32(status) != 2)
                return "0";
            return Decimal.Round(Convert.ToDecimal(amt), 2).ToString();
        }

        public void checkLogin()
        {
            if (!this.IsLogin)
            {
                HttpContext.Current.Response.Write(string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert({0});\r\ntop.location.href=\"{1}\";\r\n//--></SCRIPT>", (object)AntiXss.JavaScriptEncode("对不起！你的登录信息已失效，请重新登录"), (object)"/agent/login.aspx"));
                HttpContext.Current.Response.End();
            }
            else
                UserAccessTime.Add(new UserAccessTimeInfo()
                {
                    userid = this.UserId,
                    lastAccesstime = DateTime.Now
                });
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.checkLogin();
        }
    }
}

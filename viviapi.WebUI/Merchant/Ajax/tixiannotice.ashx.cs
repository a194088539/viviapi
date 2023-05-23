namespace viviapi.WebUI.merchant.ajax
{
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviLib.Data;

    public class tixiannotice : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        private UserInfo _currentUser = null;
        public UsersAmtInfo _currentUserAmt = null;
        private DataSet GetData()
        {
            List<SearchParam> searchParams = new List<SearchParam> {
                new SearchParam("status", 1)
            };
            return SettledFactory.PageSearch(searchParams, 20, 1, string.Empty);
        }

        public void ProcessRequest(HttpContext context)
        {
            //string s = "";
            //string str2 = ((this.balance - this.unpayment) - this.Freeze).ToString("f2");
            //if ((str2 == "") || (str2 == null))
            //{
            //    s = "{\"result\":\"no\",\"ico\":\"error\"}";
            //}
            //else
            //{
            //    s = "{\"result\":\"ok\",\"amt\":\"" + str2 + "\"}";
            //}

            DataSet data = this.GetData();
            string count = data.Tables[0].Rows[0][0].ToString();

            string s = "{\"result\":\"ok\",\"count\":\"" + count + "\"}";
            context.Response.ContentType = "application/json";
            context.Response.Write(s);
        }

        public decimal balance
        {
            get
            {
                decimal num = 0M;
                if ((this.currentUserAmt != null) && this.currentUserAmt.balance.HasValue)
                {
                    num = this.currentUserAmt.balance.Value;
                }
                return num;
            }
        }

        public UserInfo currentUser
        {
            get
            {
                if (this._currentUser == null)
                {
                    this._currentUser = UserFactory.CurrentMember;
                }
                return this._currentUser;
            }
        }

        public UsersAmtInfo currentUserAmt
        {
            get
            {
                if ((this._currentUserAmt == null) && (this.UserId > 0))
                {
                    this._currentUserAmt = UsersAmt.GetModel(this.UserId);
                }
                return this._currentUserAmt;
            }
        }

        public decimal Freeze
        {
            get
            {
                decimal num = 0M;
                if ((this.currentUserAmt != null) && this.currentUserAmt.Freeze.HasValue)
                {
                    num = this.currentUserAmt.Freeze.Value;
                }
                return num;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public decimal unpayment
        {
            get
            {
                decimal num = 0M;
                if ((this.currentUserAmt != null) && this.currentUserAmt.unpayment.HasValue)
                {
                    num = this.currentUserAmt.unpayment.Value;
                }
                return num;
            }
        }

        public int UserId
        {
            get
            {
                if (this.currentUser == null)
                {
                    return 0;
                }
                return this.currentUser.ID;
            }
        }
    }
}


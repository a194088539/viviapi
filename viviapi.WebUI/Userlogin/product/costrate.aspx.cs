namespace viviapi.WebUI.Userlogin.product
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Channel;
    using viviapi.BLL.Payment;
    using viviapi.Model.Channel;
    using viviapi.WebComponents.Web;

    public class costrate : UserPageBase
    {
        protected HtmlForm form1;
        protected Repeater rpt_paymode;

        protected void b_search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            DataTable cacheList = ChannelType.GetCacheList();
            if (!cacheList.Columns.Contains("payrate"))
            {
                cacheList.Columns.Add("payrate", typeof(double));
            }
            if (!cacheList.Columns.Contains("plmodestatus"))
            {
                cacheList.Columns.Add("plmodestatus", typeof(string));
            }
            if (!cacheList.Columns.Contains("usermodestatus"))
            {
                cacheList.Columns.Add("usermodestatus", typeof(string));
            }
            foreach (DataRow row in cacheList.Rows)
            {
                int typeId = int.Parse(row["typeId"].ToString());
                bool flag = true;
                bool flag2 = false;
                ChannelTypeUserInfo cacheModel = ChannelTypeUsers.GetCacheModel(base.UserId, typeId);
                if ((cacheModel != null) && cacheModel.userIsOpen.HasValue)
                {
                    flag = cacheModel.userIsOpen.Value;
                }
                switch (ChannelType.GetCacheModel(typeId).isOpen)
                {
                    case OpenEnum.AllClose:
                        flag2 = false;
                        break;

                    case OpenEnum.AllOpen:
                        flag2 = true;
                        break;

                    case OpenEnum.Close:
                        flag2 = false;
                        if ((cacheModel != null) && cacheModel.sysIsOpen.HasValue)
                        {
                            flag2 = cacheModel.sysIsOpen.Value;
                        }
                        break;

                    case OpenEnum.Open:
                        flag2 = true;
                        if (((cacheModel != null) && cacheModel.sysIsOpen.HasValue) && cacheModel.sysIsOpen.HasValue)
                        {
                            flag2 = cacheModel.sysIsOpen.Value;
                        }
                        break;
                }
                row["payrate"] = 100M * PayRateFactory.GetUserPayRate(base.UserId, Convert.ToInt32(row["typeId"]));
                if (flag)
                {
                    row["usermodestatus"] = "right";
                }
                else
                {
                    row["usermodestatus"] = "wrong";
                }
                if (flag2)
                {
                    row["plmodestatus"] = "right";
                }
                else
                {
                    row["plmodestatus"] = "wrong";
                }
            }
            this.rpt_paymode.DataSource = cacheList;
            this.rpt_paymode.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.LoadData();
            }
        }
    }
}


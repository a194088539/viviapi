namespace viviapi.WebUI.Managements
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.Model;
    using viviapi.WebComponents.Web;

    public class CahceManage : ManagePageBase
    {
        protected Button btnBigClass;
        protected Button btnClear;
        protected Button btnDelAll;
        protected Label CacheCountLabel;
        protected CheckBoxList cbl_cacheTypeList;
        protected HtmlForm form1;
        protected GridView gv_cache;
        protected Label Label1;
        private string[] suppList = new string[] { "51", "70", "80", "100", "101", "102", "300", "600", "800", "990", "900" };

        protected void btnBigClass_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in this.cbl_cacheTypeList.Items)
            {
                if (item.Selected)
                {
                    if (((((item.Value == "CHANNELS") || (item.Value == "CHANNEL_TYPE")) || ((item.Value == "NEWS") || (item.Value == "Question"))) || (item.Value == "SUPPPAYRATE")) || (item.Value == "SYSCONFIG"))
                    {
                        WebCache.GetCacheService().RemoveObject(item.Value);
                    }
                    else
                    {
                        string str;
                        if (((item.Value == "CHANNEL_TYPE_USER_") || (item.Value == "USER_")) || (item.Value == "USERHOST_"))
                        {
                            List<int> users = UserFactory.GetUsers("[status] = 2");
                            foreach (int num in users)
                            {
                                str = string.Format(item.Value + "{0}", num);
                                WebCache.GetCacheService().RemoveObject(str);
                            }
                        }
                        else if (item.Value == "SUPPLIER_")
                        {
                            foreach (string str2 in this.suppList)
                            {
                                str = string.Format(item.Value + "{0}", str2);
                                WebCache.GetCacheService().RemoveObject(str);
                            }
                        }
                        else if (item.Value == "WEBINFO_")
                        {
                            string objId = string.Format("WEBINFOCONFIG_{0}", WebInfoFactory.CurrentWebInfo);
                            WebCache.GetCacheService().RemoveObject(objId);
                        }
                    }
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in this.gv_cache.Rows)
            {
                CheckBox box = row.FindControl("item") as CheckBox;
                if ((box != null) && box.Checked)
                {
                    string objId = this.gv_cache.DataKeys[row.RowIndex].Value.ToString();
                    WebCache.GetCacheService().RemoveObject(objId);
                }
            }
            this.LoadCaches();
        }

        protected void btnDelAll_Click(object sender, EventArgs e)
        {
            IDictionaryEnumerator enumerator = base.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                base.Cache.Remove(enumerator.Key.ToString());
            }
            this.CacheCountLabel.Text = base.Cache.Count.ToString();
        }

        private void LoadCaches()
        {
            DataTable table = new DataTable();
            table.Columns.Add("cacheType", typeof(string));
            table.Columns.Add("cacheTypeName", typeof(string));
            table.Columns.Add("cacheKey", typeof(string));
            string objId = ChannelType.CHANNELTYPE_CACHEKEY;
            DataRow row = table.NewRow();
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
            {
                row["cacheType"] = "ChannelType";
                row["cacheTypeName"] = "支付通道类别";
                row["cacheKey"] = objId;
                table.Rows.Add(row);
            }
            List<int> users = UserFactory.GetUsers("[status] = 2");
            foreach (int num in users)
            {
                objId = string.Format(ChannelTypeUsers.ChannelTypeUsers_CACHEKEY, num);
                if (WebCache.GetCacheService().RetrieveObject(objId) != null)
                {
                    row = table.NewRow();
                    row["cacheType"] = "Channel_type_user";
                    row["cacheTypeName"] = "支付通道类别用户设置";
                    row["cacheKey"] = objId;
                    table.Rows.Add(row);
                }
            }
            objId = viviapi.BLL.Channel.Channel.CHANEL_CACHEKEY;
            row = table.NewRow();
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
            {
                row["cacheType"] = "Channel";
                row["cacheTypeName"] = "支付通道";
                row["cacheKey"] = objId;
                table.Rows.Add(row);
            }
            DataTable list = WebInfoFactory.GetList(string.Empty);
            foreach (DataRow row2 in list.Rows)
            {
                objId = string.Format(WebInfoFactory.WEBINFO_DOMAIN_CACHEKEY, row2["domain"]);
                if (WebCache.GetCacheService().RetrieveObject(objId) != null)
                {
                    row = table.NewRow();
                    row["cacheType"] = "webinfo_domain_";
                    row["cacheTypeName"] = "网站设置信息";
                    row["cacheKey"] = objId;
                    table.Rows.Add(row);
                }
            }
            objId = string.Format(WebInfoFactory.WEBINFO_DOMAIN_CACHEKEY, HttpContext.Current.Request.Url.Host);
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
            {
                row = table.NewRow();
                row["cacheType"] = "webinfo_domain_";
                row["cacheTypeName"] = "网站设置信息";
                row["cacheKey"] = objId;
                table.Rows.Add(row);
            }
            foreach (int num in users)
            {
                objId = string.Format(UserFactory.USER_CACHE_KEY, num);
                if (WebCache.GetCacheService().RetrieveObject(objId) != null)
                {
                    row = table.NewRow();
                    row["cacheType"] = "users";
                    row["cacheTypeName"] = "用户信息缓存";
                    row["cacheKey"] = objId;
                    table.Rows.Add(row);
                }
            }
            objId = NewsFactory.NEWS_CACHE_KEY;
            row = table.NewRow();
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
            {
                row["cacheType"] = "News";
                row["cacheTypeName"] = "新闻通知缓存";
                row["cacheKey"] = objId;
                table.Rows.Add(row);
            }
            foreach (string str2 in this.suppList)
            {
                objId = string.Format(SupplierFactory.CACHE_KEY, str2);
                row = table.NewRow();
                if (WebCache.GetCacheService().RetrieveObject(objId) != null)
                {
                    row["cacheType"] = "SUPPLIER";
                    row["cacheTypeName"] = "接口商";
                    row["cacheKey"] = objId;
                    table.Rows.Add(row);
                }
            }
            objId = SupplierPayRateFactory.CACHE_KEY;
            row = table.NewRow();
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
            {
                row["cacheType"] = "SUPPPAYRATE";
                row["cacheTypeName"] = "接口商费率";
                row["cacheKey"] = objId;
                table.Rows.Add(row);
            }
            objId = SysConfig.SYSCONFIG_CACHEKEY;
            row = table.NewRow();
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
            {
                row["cacheType"] = "SysConfig";
                row["cacheTypeName"] = "配置信息";
                row["cacheKey"] = objId;
                table.Rows.Add(row);
            }
            objId = "Question";
            row = table.NewRow();
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
            {
                row["cacheType"] = "Question";
                row["cacheTypeName"] = "问题列表";
                row["cacheKey"] = objId;
                table.Rows.Add(row);
            }
            this.gv_cache.DataSource = table;
            this.gv_cache.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.CacheCountLabel.Text = base.Cache.Count.ToString();
                this.LoadCaches();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.System))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}


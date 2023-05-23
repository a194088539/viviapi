namespace viviapi.WebUI.Merchant.WS
{
    using Newtonsoft.Json;
    using System.Data;
    using System.Text;
    using System.Web;
    using viviapi.BLL.Channel;
    using viviapi.Model.Channel;
    using viviLib.Web;

    public class GetCardTypes_new : IHttpHandler
    {
        public string GetFaceValues(DataTable dt, int otypeid)
        {
            StringBuilder builder = new StringBuilder();
            DataRow[] rowArray = dt.Select("typeid=" + otypeid.ToString(), "faceValue");
            if (rowArray != null)
            {
                foreach (DataRow row in rowArray)
                {
                    builder.AppendFormat("{0}|", row["faceValue"]);
                }
            }
            return builder.ToString();
        }

        public void ProcessRequest(HttpContext context)
        {
            string s = string.Empty;
            try
            {
                string formString = WebBase.GetFormString("userid", string.Empty);
                if (!string.IsNullOrEmpty(formString))
                {
                    int result = 0;
                    if (int.TryParse(formString, out result))
                    {
                        DataTable table = new DataTable();
                        table.Columns.Add("typeid", typeof(int));
                        table.Columns.Add("typename", typeof(string));
                        table.Columns.Add("facevalues", typeof(string));
                        DataTable cacheList = ChannelType.GetCacheList();
                        DataTable dt = viviapi.BLL.Channel.Channel.GetCardChanels(result, 0, 0, 1);
                        foreach (DataRow row in cacheList.Rows)
                        {
                            int typeId = int.Parse(row["typeId"].ToString());
                            if (((typeId >= 0x67) && (typeId != 90)) && (typeId != 0x72))
                            {
                                bool flag = true;
                                bool flag2 = false;
                                ChannelTypeUserInfo cacheModel = ChannelTypeUsers.GetCacheModel(result, typeId);
                                if ((cacheModel != null) && cacheModel.userIsOpen.HasValue)
                                {
                                    flag = cacheModel.userIsOpen.Value;
                                    if (!flag)
                                    {
                                        continue;
                                    }
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
                                if (flag && flag2)
                                {
                                    DataRow row2 = table.NewRow();
                                    row2["typeid"] = typeId;
                                    row2["typename"] = row["modetypename"];
                                    row2["facevalues"] = this.GetFaceValues(dt, typeId);
                                    table.Rows.Add(row2);
                                }
                            }
                        }
                        s = "success" + JsonConvert.SerializeObject(table, Formatting.Indented);
                    }
                }
            }
            catch
            {
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


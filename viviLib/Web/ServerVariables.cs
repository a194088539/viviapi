namespace viviLib.Web
{
    public sealed class ServerVariables
    {
        public static string TrueIP
        {
            get
            {
                if (WebBase.Request != null)
                {
                    if (WebBase.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    {
                        if (WebBase.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                            return WebBase.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        return string.Empty;
                    }
                    if (WebBase.Request.ServerVariables["REMOTE_ADDR"] != null)
                        return WebBase.Request.ServerVariables["REMOTE_ADDR"];
                }
                return string.Empty;
            }
        }

        private ServerVariables()
        {
        }
    }
}

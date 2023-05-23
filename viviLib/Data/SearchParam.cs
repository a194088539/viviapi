namespace viviLib.Data
{
    public class SearchParam
    {
        private string _cmpOperator;
        private string _paramKey;
        private object _paramValue;

        public SearchParam()
        {
            this._paramKey = string.Empty;
            this._cmpOperator = "=";
            this._paramValue = null;
        }

        public SearchParam(string paramKey, object paramValue)
        {
            this._paramKey = string.Empty;
            this._cmpOperator = "=";
            this._paramValue = null;
            this.ParamKey = paramKey;
            this.ParamValue = paramValue;
        }

        public SearchParam(string paramKey, string cmpOperator, object paramValue)
        {
            this._paramKey = string.Empty;
            this._cmpOperator = "=";
            this._paramValue = null;
            this.ParamKey = paramKey;
            this.CmpOperator = cmpOperator;
            this.ParamValue = paramValue;
        }

        public string CmpOperator
        {
            get
            {
                return this._cmpOperator;
            }
            set
            {
                this._cmpOperator = value;
            }
        }

        public string ParamKey
        {
            get
            {
                return this._paramKey;
            }
            set
            {
                this._paramKey = value;
            }
        }

        public object ParamValue
        {
            get
            {
                return this._paramValue;
            }
            set
            {
                this._paramValue = value;
            }
        }
    }
}


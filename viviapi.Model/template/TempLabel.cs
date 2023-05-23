namespace viviapi.Model
{
    public class TempLabel
    {
        private string _content;
        private int _id;
        private string _info;
        private int _sort;
        private string _source;
        private string _templateId;
        private string _title;

        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Info
        {
            get
            {
                return this._info;
            }
            set
            {
                this._info = value;
            }
        }

        public int Sort
        {
            get
            {
                return this._sort;
            }
            set
            {
                this._sort = value;
            }
        }

        public string Source
        {
            get
            {
                return this._source;
            }
            set
            {
                this._source = value;
            }
        }

        public string TemplateId
        {
            get
            {
                return this._templateId;
            }
            set
            {
                this._templateId = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public TempLabel()
        {
        }

        public TempLabel(int _id, string _title, string _info, string _content, string _templateId, int _sort, string _source)
        {
            this._id = _id;
            this._title = _title;
            this._info = _info;
            this._content = _content;
            this._templateId = _templateId;
            this._sort = _sort;
            this._source = _source;
        }
    }
}

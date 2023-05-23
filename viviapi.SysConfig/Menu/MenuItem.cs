using System;
using System.Collections.Generic;

namespace viviapi.SysConfig.Menu
{
    [Serializable]
    public class MenuItem
    {
        private string _text = string.Empty;
        private string _link = string.Empty;
        private int _systemId = 0;
        private bool _onlyForAdmin = false;
        private int[] _requirePowers = new int[0];
        private List<MenuItem> _items = new List<MenuItem>();

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        public string Link
        {
            get
            {
                return this._link;
            }
            set
            {
                this._link = value;
            }
        }

        public int SystemId
        {
            get
            {
                return this._systemId;
            }
            set
            {
                this._systemId = value;
            }
        }

        public bool OnlyForAdmin
        {
            get
            {
                return this._onlyForAdmin;
            }
            set
            {
                this._onlyForAdmin = value;
            }
        }

        public int[] RequirePowers
        {
            get
            {
                return this._requirePowers;
            }
            set
            {
                this._requirePowers = value;
            }
        }

        public List<MenuItem> Items
        {
            get
            {
                return this._items;
            }
        }
    }
}

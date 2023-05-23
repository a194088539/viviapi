using System;
using System.Collections.Generic;

namespace viviapi.SysConfig.Menu
{
    [Serializable]
    public class SystemInstance
    {
        private int _systemId = 0;
        private int _parentId = 0;
        private SystemType _systemType = SystemType.Custom;
        private string _name = string.Empty;
        private bool _onlyForAdmin = false;
        private bool _release = true;
        private List<SystemInstance> _items = new List<SystemInstance>();

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

        public int ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        public SystemType SystemType
        {
            get
            {
                return this._systemType;
            }
            set
            {
                this._systemType = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
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

        public bool Release
        {
            get
            {
                return this._release;
            }
            set
            {
                this._release = value;
            }
        }

        public List<SystemInstance> Items
        {
            get
            {
                return this._items;
            }
        }
    }
}

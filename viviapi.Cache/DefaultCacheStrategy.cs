namespace viviapi.Cache
{
    using System;
    using System.Web;
    using System.Web.Caching;

    public class DefaultCacheStrategy : ICacheStrategy
    {
        protected int _timeOut = 0xe10;
        private static readonly DefaultCacheStrategy instance = new DefaultCacheStrategy();
        private static object syncObj = new object();
        protected static volatile Cache webCache = HttpRuntime.Cache;

        public virtual void AddObject(string objId, object o)
        {
            if (((objId != null) && (objId.Length != 0)) && (o != null))
            {
                CacheItemRemovedCallback onRemoveCallback = new CacheItemRemovedCallback(this.onRemove);
                if ((this.TimeOut == 0x1c20) || (this.TimeOut == 0))
                {
                    webCache.Insert(objId, o, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, onRemoveCallback);
                }
                else
                {
                    webCache.Insert(objId, o, null, DateTime.Now.AddSeconds((double)this.TimeOut), Cache.NoSlidingExpiration, CacheItemPriority.High, onRemoveCallback);
                }
            }
        }

        public virtual void AddObject(int hashCode, string objId, object o)
        {
            this.AddObject(objId, o);
        }

        public virtual void AddObject(string objId, object o, bool saved)
        {
            this.AddObject(objId, o);
        }

        public virtual void AddObject(string objId, object o, int expires)
        {
            CacheItemRemovedCallback onRemoveCallback = new CacheItemRemovedCallback(this.onRemove);
            webCache.Insert(objId, o, null, DateTime.Now.AddSeconds((double)expires), Cache.NoSlidingExpiration, CacheItemPriority.High, onRemoveCallback);
        }

        public virtual void AddObject(int hashCode, string objId, object o, bool saved)
        {
            this.AddObject(objId, o);
        }

        public virtual void AddObject(string objId, object o, int expires, bool saved)
        {
            this.AddObject(objId, o, expires);
        }

        public void onRemove(string key, object val, CacheItemRemovedReason reason)
        {
        }

        public virtual void RemoveObject(string objId)
        {
            if ((objId != null) && (objId.Length != 0))
            {
                webCache.Remove(objId);
            }
        }

        public virtual void RemoveObject(int hashCode, string objId)
        {
            this.RemoveObject(objId);
        }

        public virtual void RemoveObject(string objId, bool saved)
        {
            this.RemoveObject(objId);
        }

        public virtual object RetrieveObject(string objId)
        {
            if ((objId != null) && (objId.Length != 0))
            {
                return webCache.Get(objId);
            }
            return null;
        }

        public virtual object RetrieveObject(int hashCode, string objId)
        {
            if ((objId != null) && (objId.Length != 0))
            {
                return webCache.Get(objId);
            }
            return null;
        }

        public virtual object RetrieveObject(string objId, Type type, bool saved)
        {
            return this.RetrieveObject(objId);
        }

        public virtual object RetrieveObject(int hashCode, string objId, Type type, bool saved)
        {
            return this.RetrieveObject(objId);
        }

        public static Cache GetWebCacheObj
        {
            get
            {
                return webCache;
            }
        }

        public virtual int TimeOut
        {
            get
            {
                if (this._timeOut <= 0)
                {
                    return 0xe10;
                }
                return this._timeOut;
            }
            set
            {
                this._timeOut = (value > 0) ? value : 0xe10;
            }
        }
    }
}


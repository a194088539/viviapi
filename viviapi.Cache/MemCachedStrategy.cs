namespace viviapi.Cache
{
    using MemcachedLib;
    using System;
    using viviapi.SysConfig;
    using viviLib.ExceptionHandling;

    public class MemCachedStrategy : DefaultCacheStrategy
    {
        public override void AddObject(string objId, object o)
        {
            if (MemCachedConfig.LocalCacheTime > 0)
            {
                base.AddObject(objId, o, MemCachedConfig.LocalCacheTime);
            }
            else
            {
                base.AddObject(objId, o);
            }
            if (MemCachedConfig.MemCacheTime > 0)
            {
                MemCachedManager.CacheClient.Set(objId, o, DateTime.Now.AddSeconds((double)MemCachedConfig.MemCacheTime));
            }
            else
            {
                MemCachedManager.CacheClient.Set(objId, o);
            }
            this.RecordLog(objId, "set");
        }

        public virtual void AddObject(int hashCode, string objId, object o)
        {
            if (MemCachedConfig.LocalCacheTime > 0)
            {
                base.AddObject(objId, o, MemCachedConfig.LocalCacheTime);
            }
            else
            {
                base.AddObject(objId, o);
            }
            if (MemCachedConfig.MemCacheTime > 0)
            {
                MemCachedManager.CacheClient.Set(objId, o, DateTime.Now.AddSeconds((double)MemCachedConfig.MemCacheTime), hashCode);
            }
            else
            {
                MemCachedManager.CacheClient.Set(objId, o, hashCode);
            }
            this.RecordLog(objId, "set");
        }

        public override void AddObject(string objId, object o, bool saved)
        {
            this.AddObject(objId, o);
            if (saved)
            {
                this.SaveObject(objId, o);
            }
        }

        public override void AddObject(string objId, object o, int expried)
        {
            base.AddObject(objId, o);
            MemCachedManager.CacheClient.Set(objId, o, DateTime.Now.AddSeconds((double)expried));
            this.RecordLog(objId, "set");
        }

        public override void AddObject(int hashCode, string objId, object o, bool saved)
        {
            this.AddObject(hashCode, objId, o);
            if (saved)
            {
                this.SaveObject(objId, o);
            }
        }

        public override void AddObject(string objId, object o, int expried, bool saved)
        {
            this.AddObject(objId, o, expried);
            if (saved)
            {
                this.SaveObject(objId, o);
            }
        }

        private void RecordLog(string objId, string opName)
        {
            try
            {
                bool recordeLog = MemCachedConfig.RecordeLog;
            }
            catch
            {
            }
        }

        public override void RemoveObject(string objId)
        {
            try
            {
                base.RemoveObject(objId);
                MemCachedManager.CacheClient.Delete(objId);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        public override void RemoveObject(int hashCode, string objId)
        {
            base.RemoveObject(objId);
            MemCachedManager.CacheClient.Delete(objId, hashCode, DateTime.MaxValue);
        }

        public override void RemoveObject(string objId, bool saved)
        {
            this.RemoveObject(objId);
        }

        public override object RetrieveObject(string objId)
        {
            object obj2 = base.RetrieveObject(objId);
            if (obj2 == null)
            {
                obj2 = MemCachedManager.CacheClient.Get(objId);
                this.RecordLog(objId, "get");
            }
            return obj2;
        }

        public override object RetrieveObject(int hashCode, string objId)
        {
            object obj2 = base.RetrieveObject(objId);
            if (obj2 == null)
            {
                obj2 = MemCachedManager.CacheClient.Get(objId, hashCode);
                this.RecordLog(objId, "get");
            }
            return obj2;
        }

        public override object RetrieveObject(string objId, Type type, bool saved)
        {
            object obj2 = this.RetrieveObject(objId);
            if (obj2 == null)
            {
            }
            return obj2;
        }

        public override object RetrieveObject(int hashCode, string objId, Type type, bool saved)
        {
            object o = this.RetrieveObject(hashCode, objId);
            if (((o == null) && saved) && (o != null))
            {
                this.AddObject(hashCode, objId, o);
            }
            return o;
        }

        private void SaveObject(string objId, object o)
        {
        }

        public int LocalCacheTime
        {
            get
            {
                return MemCachedConfig.LocalCacheTime;
            }
        }

        public override int TimeOut
        {
            get
            {
                return MemCachedConfig.MemCacheTime;
            }
        }
    }
}


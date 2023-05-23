namespace viviapi.Cache
{
    using System;
    using viviapi.SysConfig;

    public class WebCache
    {
        private static bool applyMemCached = false;
        private static volatile ICacheStrategy cs;
        private static object lockHelper = new object();
        private static ICacheStrategy memcachedStrategy;

        static WebCache()
        {
            InitialCacheStrategy();
        }

        public static ICacheStrategy GetCacheService()
        {
            if (cs == null)
            {
                lock (lockHelper)
                {
                    if (cs == null)
                    {
                        InitialCacheStrategy();
                    }
                }
            }
            return cs;
        }

        private static void InitialCacheStrategy()
        {
            if (MemCachedConfig.ApplyMemCached)
            {
                applyMemCached = true;
            }
            if (applyMemCached)
            {
                try
                {
                    cs = memcachedStrategy = new MemCachedStrategy();
                    return;
                }
                catch
                {
                    throw new Exception("请检查Discuz.EntLib.dll文件是否被放置在bin目录下并配置正确");
                }
            }
            cs = new DefaultCacheStrategy();
        }

        public static void LoadCacheStrategy(ICacheStrategy ics)
        {
            lock (lockHelper)
            {
                if (!applyMemCached)
                {
                    cs = ics;
                }
            }
        }

        public static void LoadDefaultCacheStrategy()
        {
            lock (lockHelper)
            {
                if (applyMemCached)
                {
                    cs = memcachedStrategy;
                }
                else
                {
                    cs = new DefaultCacheStrategy();
                }
            }
        }
    }
}


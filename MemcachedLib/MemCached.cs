using System.Collections;
using viviapi.SysConfig;
using viviLib.Text;
using viviLib.Utils;

namespace MemcachedLib
{
    public sealed class MemCachedManager
    {
        private static MemcachedClient mc = (MemcachedClient)null;
        private static SockIOPool pool = (SockIOPool)null;
        private static string[] serverList = (string[])null;

        public static string[] ServerList
        {
            get
            {
                return MemCachedManager.serverList;
            }
            set
            {
                if (value == null)
                    return;
                MemCachedManager.serverList = value;
            }
        }

        public static MemcachedClient CacheClient
        {
            get
            {
                if (MemCachedManager.mc == null)
                    MemCachedManager.CreateManager();
                return MemCachedManager.mc;
            }
        }

        static MemCachedManager()
        {
            MemCachedManager.CreateManager();
        }

        public static void CreateManager()
        {
            MemCachedManager.serverList = MemCachedConfig.ServerList.Split(',');
            MemCachedManager.pool = SockIOPool.GetInstance(MemCachedConfig.PoolName);
            MemCachedManager.pool.SetServers(MemCachedManager.serverList);
            MemCachedManager.pool.SetWeights(TypeParse.StringToIntArray(MemCachedConfig.ServerList, 1));
            MemCachedManager.pool.InitConnections = MemCachedConfig.IntConnections;
            MemCachedManager.pool.MinConnections = MemCachedConfig.MinConnections;
            MemCachedManager.pool.MaxConnections = MemCachedConfig.MaxConnections;
            MemCachedManager.pool.SocketConnectTimeout = MemCachedConfig.SocketConnectTimeout;
            MemCachedManager.pool.SocketTimeout = MemCachedConfig.SocketTimeout;
            MemCachedManager.pool.MaintenanceSleep = (long)MemCachedConfig.MaintenanceSleep;
            MemCachedManager.pool.Failover = MemCachedConfig.FailOver;
            MemCachedManager.pool.Nagle = MemCachedConfig.Nagle;
            MemCachedManager.pool.HashingAlgorithm = HashingAlgorithm.NewCompatibleHash;
            MemCachedManager.pool.Initialize();
            MemCachedManager.mc = new MemcachedClient();
            MemCachedManager.mc.PoolName = MemCachedConfig.PoolName;
            MemCachedManager.mc.EnableCompression = false;
        }

        public static MemcachedClient GetMemcachedClient(string poolName, ArrayList serverArrayList)
        {
            SockIOPool instance = SockIOPool.GetInstance(poolName);
            instance.SetServers(serverArrayList);
            instance.InitConnections = MemCachedConfig.IntConnections;
            instance.MinConnections = MemCachedConfig.MinConnections;
            instance.MaxConnections = MemCachedConfig.MaxConnections;
            instance.SocketConnectTimeout = MemCachedConfig.SocketConnectTimeout;
            instance.SocketTimeout = MemCachedConfig.SocketTimeout;
            instance.MaintenanceSleep = (long)MemCachedConfig.MaintenanceSleep;
            instance.Failover = MemCachedConfig.FailOver;
            instance.Nagle = MemCachedConfig.Nagle;
            instance.HashingAlgorithm = HashingAlgorithm.NewCompatibleHash;
            instance.Initialize();
            return new MemcachedClient()
            {
                PoolName = poolName,
                EnableCompression = false
            };
        }

        public static void Dispose()
        {
            if (!MemCachedConfig.ApplyMemCached || MemCachedManager.pool == null)
                return;
            MemCachedManager.pool.Shutdown();
        }

        public static string GetSocketHost(string key, object hashCode)
        {
            string str = "";
            SockIO sockIo = (SockIO)null;
            try
            {
                sockIo = SockIOPool.GetInstance(MemCachedConfig.PoolName).GetSock(key, hashCode);
                if (sockIo != null)
                    str = sockIo.Host;
            }
            finally
            {
                if (sockIo != null)
                    sockIo.Close();
            }
            return str;
        }

        public static string[] GetConnectedSocketHost()
        {
            SockIO sockIo = (SockIO)null;
            string target = (string)null;
            foreach (string str in MemCachedManager.serverList)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    try
                    {
                        sockIo = SockIOPool.GetInstance(MemCachedConfig.PoolName).GetConnection(str);
                        if (sockIo != null)
                            target = Strings.MergeString(str, target);
                    }
                    finally
                    {
                        if (sockIo != null)
                            sockIo.Close();
                    }
                }
            }
            return target.Split(',');
        }

        public static ArrayList GetStats()
        {
            ArrayList serverArrayList = new ArrayList();
            foreach (string str in MemCachedManager.serverList)
                serverArrayList.Add((object)str);
            return MemCachedManager.GetStats(serverArrayList, MemCachedManager.Stats.Default, (string)null);
        }

        public static ArrayList GetStats(ArrayList serverArrayList, MemCachedManager.Stats statsCommand, string param)
        {
            ArrayList arrayList = new ArrayList();
            param = string.IsNullOrEmpty(param) ? "" : param.Trim().ToLower();
            string command = "stats";
            switch (statsCommand)
            {
                case MemCachedManager.Stats.Reset:
                    command = "stats reset";
                    break;
                case MemCachedManager.Stats.Malloc:
                    command = "stats malloc";
                    break;
                case MemCachedManager.Stats.Maps:
                    command = "stats maps";
                    break;
                case MemCachedManager.Stats.Sizes:
                    command = "stats sizes";
                    break;
                case MemCachedManager.Stats.Slabs:
                    command = "stats slabs";
                    break;
                case MemCachedManager.Stats.Items:
                    command = "stats items";
                    break;
                case MemCachedManager.Stats.CachedDump:
                    string[] strNumber = param.Split(' ');
                    if (strNumber.Length >= 2 && Utils.IsNumericArray(strNumber))
                    {
                        command = "stats cachedump " + param;
                        break;
                    }
                    break;
                case MemCachedManager.Stats.Detail:
                    if (string.Equals(param, "on") || string.Equals(param, "off") || string.Equals(param, "dump"))
                    {
                        command = "stats detail " + param.Trim();
                        break;
                    }
                    break;
                default:
                    command = "stats";
                    break;
            }
            Hashtable hashtable1 = MemCachedManager.CacheClient.Stats(serverArrayList, command, (string)null);
            foreach (string str1 in (IEnumerable)hashtable1.Keys)
            {
                arrayList.Add((object)"================================================================================================");
                arrayList.Add((object)str1);
                Hashtable hashtable2 = (Hashtable)hashtable1[(object)str1];
                foreach (string str2 in (IEnumerable)hashtable2.Keys)
                {
                    if (statsCommand == MemCachedManager.Stats.CachedDump)
                        arrayList.Add((object)str2);
                    else
                        arrayList.Add((object)(str2 + (object)":" + (string)hashtable2[(object)str2]));
                }
            }
            return arrayList;
        }

        public static ArrayList GetCachedKeyList(ArrayList serverArrayList, string poolName)
        {
            Hashtable hashtable1 = MemCachedManager.GetMemcachedClient(poolName, serverArrayList).Stats(serverArrayList, "stats items", poolName);
            ArrayList arrayList = new ArrayList();
            foreach (string str1 in (IEnumerable)hashtable1.Keys)
            {
                foreach (string str2 in (IEnumerable)((Hashtable)hashtable1[(object)str1]).Keys)
                {
                    Hashtable hashtable2 = MemCachedManager.CacheClient.Stats(serverArrayList, "stats cachedump " + str2.Split(':')[1] + " 0", poolName);
                    foreach (string str3 in (IEnumerable)hashtable2.Keys)
                    {
                        foreach (string str4 in (IEnumerable)((Hashtable)hashtable2[(object)str3]).Keys)
                        {
                            if (!arrayList.Contains((object)str4))
                                arrayList.Add((object)str4);
                        }
                    }
                }
            }
            return arrayList;
        }

        public enum Stats
        {
            Default,
            Reset,
            Malloc,
            Maps,
            Sizes,
            Slabs,
            Items,
            CachedDump,
            Detail,
        }
    }
}

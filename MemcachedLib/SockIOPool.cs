using System;
using System.Collections;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MemcachedLib
{
    public class SockIOPool
    {
        private static Hashtable Pools = new Hashtable();
        private static ResourceManager _resourceManager = new ResourceManager("Discuz.Cache.MemCached.StringMessages", typeof(SockIOPool).Assembly);
        private int _maxCreate = 1;
        private int _poolMultiplier = 4;
        private int _initConns = 3;
        private int _minConns = 3;
        private int _maxConns = 10;
        private long _maxIdle = 180000L;
        private long _maxBusyTime = 300000L;
        private long _maintThreadSleep = 5000L;
        private int _socketTimeout = 10000;
        private int _socketConnectTimeout = 50;
        private bool _failover = true;
        private bool _nagle = true;
        private HashingAlgorithm _hashingAlgorithm = HashingAlgorithm.Native;
        private SockIOPool.MaintenanceThread _maintenanceThread;
        private bool _initialized;
        private Hashtable _createShift;
        private ArrayList _servers;
        private ArrayList _weights;
        private ArrayList _buckets;
        private Hashtable _hostDead;
        private Hashtable _hostDeadDuration;
        private Hashtable _availPool;
        private Hashtable _busyPool;

        public ArrayList Servers
        {
            get
            {
                return this._servers;
            }
        }

        public ArrayList Weights
        {
            get
            {
                return this._weights;
            }
        }

        public int InitConnections
        {
            get
            {
                return this._initConns;
            }
            set
            {
                this._initConns = value;
            }
        }

        public int MinConnections
        {
            get
            {
                return this._minConns;
            }
            set
            {
                this._minConns = value;
            }
        }

        public int MaxConnections
        {
            get
            {
                return this._maxConns;
            }
            set
            {
                this._maxConns = value;
            }
        }

        public long MaxIdle
        {
            get
            {
                return this._maxIdle;
            }
            set
            {
                this._maxIdle = value;
            }
        }

        public long MaxBusy
        {
            get
            {
                return this._maxBusyTime;
            }
            set
            {
                this._maxBusyTime = value;
            }
        }

        public long MaintenanceSleep
        {
            get
            {
                return this._maintThreadSleep;
            }
            set
            {
                this._maintThreadSleep = value;
            }
        }

        public int SocketTimeout
        {
            get
            {
                return this._socketTimeout;
            }
            set
            {
                this._socketTimeout = value;
            }
        }

        public int SocketConnectTimeout
        {
            get
            {
                return this._socketConnectTimeout;
            }
            set
            {
                this._socketConnectTimeout = value;
            }
        }

        public bool Failover
        {
            get
            {
                return this._failover;
            }
            set
            {
                this._failover = value;
            }
        }

        public bool Nagle
        {
            get
            {
                return this._nagle;
            }
            set
            {
                this._nagle = value;
            }
        }

        public HashingAlgorithm HashingAlgorithm
        {
            get
            {
                return this._hashingAlgorithm;
            }
            set
            {
                this._hashingAlgorithm = value;
            }
        }

        public bool Initialized
        {
            get
            {
                return this._initialized;
            }
        }

        protected SockIOPool()
        {
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SockIOPool GetInstance(string poolName)
        {
            if (SockIOPool.Pools.ContainsKey((object)poolName))
                return (SockIOPool)SockIOPool.Pools[(object)poolName];
            SockIOPool sockIoPool = new SockIOPool();
            SockIOPool.Pools[(object)poolName] = (object)sockIoPool;
            return sockIoPool;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SockIOPool GetInstance()
        {
            return SockIOPool.GetInstance(SockIOPool.GetLocalizedString("default instance"));
        }

        public void SetServers(string[] servers)
        {
            this.SetServers(new ArrayList((ICollection)servers));
        }

        public void SetServers(ArrayList servers)
        {
            this._servers = servers;
        }

        public void SetWeights(int[] weights)
        {
            this.SetWeights(new ArrayList((ICollection)weights));
        }

        public void SetWeights(ArrayList weights)
        {
            this._weights = weights;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Initialize()
        {
            if (this._initialized && this._buckets != null && this._availPool != null && this._busyPool != null)
                return;
            this._buckets = new ArrayList();
            this._availPool = new Hashtable(this._servers.Count * this._initConns);
            this._busyPool = new Hashtable(this._servers.Count * this._initConns);
            this._hostDeadDuration = new Hashtable();
            this._hostDead = new Hashtable();
            this._createShift = new Hashtable();
            this._maxCreate = this._poolMultiplier > this._minConns ? this._minConns : this._minConns / this._poolMultiplier;
            if (this._servers == null || this._servers.Count <= 0)
                throw new ArgumentException(SockIOPool.GetLocalizedString("initialize with no servers"));
            for (int index1 = 0; index1 < this._servers.Count; ++index1)
            {
                if (this._weights != null && this._weights.Count > index1)
                {
                    for (int index2 = 0; index2 < (int)this._weights[index1]; ++index2)
                        this._buckets.Add(this._servers[index1]);
                }
                else
                    this._buckets.Add(this._servers[index1]);
                for (int index2 = 0; index2 < this._initConns; ++index2)
                {
                    SockIO socket = this.CreateSocket((string)this._servers[index1]);
                    if (socket != null)
                        SockIOPool.AddSocketToPool(this._availPool, (string)this._servers[index1], socket);
                    else
                        break;
                }
            }
            this._initialized = true;
            if (this._maintThreadSleep <= 0L)
                return;
            this.StartMaintenanceThread();
        }

        protected SockIO CreateSocket(string host)
        {
            if (this._failover && this._hostDead.ContainsKey((object)host) && this._hostDeadDuration.ContainsKey((object)host))
            {
                if (((DateTime)this._hostDead[(object)host]).AddMilliseconds((double)(long)this._hostDeadDuration[(object)host]) > DateTime.Now)
                    return (SockIO)null;
            }
            SockIO sockIo;
            try
            {
                sockIo = new SockIO(this, host, this._socketTimeout, this._socketConnectTimeout, this._nagle);
                if (!sockIo.IsConnected)
                {
                    try
                    {
                        sockIo.TrueClose();
                    }
                    catch
                    {
                        sockIo = (SockIO)null;
                    }
                }
            }
            catch
            {
                sockIo = (SockIO)null;
            }
            if (sockIo == null)
            {
                DateTime now = DateTime.Now;
                this._hostDead[(object)host] = (object)now;
                long num = this._hostDeadDuration.ContainsKey((object)host) ? (long)this._hostDeadDuration[(object)host] * 2L : 100L;
                this._hostDeadDuration[(object)host] = (object)num;
                SockIOPool.ClearHostFromPool(this._availPool, host);
                if (this._buckets.BinarySearch((object)host) >= 0)
                    this._buckets.Remove((object)host);
            }
            else
            {
                this._hostDead.Remove((object)host);
                this._hostDeadDuration.Remove((object)host);
                if (this._buckets.BinarySearch((object)host) < 0)
                    this._buckets.Add((object)host);
            }
            return sockIo;
        }

        public SockIO GetSock(string key)
        {
            return this.GetSock(key, (object)null);
        }

        public SockIO GetSock(string key, object hashCode)
        {
            string str = "<null>";
            if (hashCode != null)
                str = hashCode.ToString();
            if (key == null || key.Length == 0 || !this._initialized || this._buckets.Count == 0)
                return (SockIO)null;
            if (this._buckets.Count == 1)
                return this.GetConnection((string)this._buckets[0]);
            int num1 = 0;
            int num2;
            if (hashCode != null)
            {
                num2 = (int)hashCode;
            }
            else
            {
                switch (this._hashingAlgorithm)
                {
                    case HashingAlgorithm.Native:
                        num2 = key.GetHashCode();
                        break;
                    case HashingAlgorithm.OldCompatibleHash:
                        num2 = HashingAlgorithmHelper.OriginalHashingAlgorithm(key);
                        break;
                    case HashingAlgorithm.NewCompatibleHash:
                        num2 = HashingAlgorithmHelper.NewHashingAlgorithm(key);
                        break;
                    default:
                        num2 = key.GetHashCode();
                        this._hashingAlgorithm = HashingAlgorithm.Native;
                        break;
                }
            }
            while (num1++ <= this._buckets.Count)
            {
                int index = num2 % this._buckets.Count;
                if (index < 0)
                    index += this._buckets.Count;
                SockIO connection = this.GetConnection((string)this._buckets[index]);
                if (connection != null)
                    return connection;
                if (!this._failover)
                    return (SockIO)null;
                switch (this._hashingAlgorithm)
                {
                    case HashingAlgorithm.Native:
                        num2 += ((string)(object)num1 + (object)key).GetHashCode();
                        break;
                    case HashingAlgorithm.OldCompatibleHash:
                        num2 += HashingAlgorithmHelper.OriginalHashingAlgorithm((string)(object)num1 + (object)key);
                        break;
                    case HashingAlgorithm.NewCompatibleHash:
                        num2 += HashingAlgorithmHelper.NewHashingAlgorithm((string)(object)num1 + (object)key);
                        break;
                    default:
                        num2 += ((string)(object)num1 + (object)key).GetHashCode();
                        this._hashingAlgorithm = HashingAlgorithm.Native;
                        break;
                }
            }
            return (SockIO)null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public SockIO GetConnection(string host)
        {
            if (!this._initialized || host == null)
                return (SockIO)null;
            if (this._availPool != null && this._availPool.Count != 0)
            {
                Hashtable hashtable = (Hashtable)this._availPool[(object)host];
                if (hashtable != null && hashtable.Count != 0)
                {
                    foreach (SockIO socket in new IteratorIsolateCollection((IEnumerable)hashtable.Keys))
                    {
                        if (socket.IsConnected)
                        {
                            hashtable.Remove((object)socket);
                            SockIOPool.AddSocketToPool(this._busyPool, host, socket);
                            return socket;
                        }
                        hashtable.Remove((object)socket);
                    }
                }
            }
            object obj = this._createShift[(object)host];
            int num1 = obj != null ? (int)obj : 0;
            int num2 = 1 << num1;
            if (num2 >= this._maxCreate)
                num2 = this._maxCreate;
            else
                ++num1;
            this._createShift[(object)host] = (object)num1;
            for (int index = num2; index > 0; --index)
            {
                SockIO socket = this.CreateSocket(host);
                if (socket != null)
                {
                    if (index == 1)
                    {
                        SockIOPool.AddSocketToPool(this._busyPool, host, socket);
                        return socket;
                    }
                    SockIOPool.AddSocketToPool(this._availPool, host, socket);
                }
                else
                    break;
            }
            return (SockIO)null;
        }

        protected static void AddSocketToPool(Hashtable pool, string host, SockIO socket)
        {
            if (pool == null)
                return;
            if (host != null && host.Length != 0 && pool.ContainsKey((object)host))
            {
                Hashtable hashtable = (Hashtable)pool[(object)host];
                if (hashtable != null)
                {
                    hashtable[(object)socket] = (object)DateTime.Now;
                    return;
                }
            }
            Hashtable hashtable1 = new Hashtable();
            hashtable1[(object)socket] = (object)DateTime.Now;
            pool[(object)host] = (object)hashtable1;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected static void RemoveSocketFromPool(Hashtable pool, string host, SockIO socket)
        {
            if (host != null && host.Length == 0 || pool == null || !pool.ContainsKey((object)host))
                return;
            Hashtable hashtable = (Hashtable)pool[(object)host];
            if (hashtable != null)
                hashtable.Remove((object)socket);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected static void ClearHostFromPool(Hashtable pool, string host)
        {
            if (pool == null || host != null && host.Length == 0 || !pool.ContainsKey((object)host))
                return;
            Hashtable hashtable = (Hashtable)pool[(object)host];
            if (hashtable != null && hashtable.Count > 0)
            {
                foreach (SockIO sockIo in new IteratorIsolateCollection((IEnumerable)hashtable.Keys))
                {
                    try
                    {
                        sockIo.TrueClose();
                    }
                    catch
                    {
                    }
                    hashtable.Remove((object)sockIo);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CheckIn(SockIO socket, bool addToAvail)
        {
            if (socket == null)
                return;
            string host = socket.Host;
            SockIOPool.RemoveSocketFromPool(this._busyPool, host, socket);
            if (!addToAvail || !socket.IsConnected)
                return;
            SockIOPool.AddSocketToPool(this._availPool, host, socket);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CheckIn(SockIO socket)
        {
            this.CheckIn(socket, true);
        }

        protected static void ClosePool(Hashtable pool)
        {
            if (pool == null)
                return;
            foreach (string str in (IEnumerable)pool.Keys)
            {
                Hashtable hashtable = (Hashtable)pool[(object)str];
                foreach (SockIO sockIo in new IteratorIsolateCollection((IEnumerable)hashtable.Keys))
                {
                    try
                    {
                        sockIo.TrueClose();
                    }
                    catch
                    {
                    }
                    hashtable.Remove((object)sockIo);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Shutdown()
        {
            if (this._maintenanceThread != null && this._maintenanceThread.IsRunning)
                this.StopMaintenanceThread();
            SockIOPool.ClosePool(this._availPool);
            SockIOPool.ClosePool(this._busyPool);
            this._availPool = (Hashtable)null;
            this._busyPool = (Hashtable)null;
            this._buckets = (ArrayList)null;
            this._hostDeadDuration = (Hashtable)null;
            this._hostDead = (Hashtable)null;
            this._initialized = false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void StartMaintenanceThread()
        {
            if (this._maintenanceThread != null)
            {
                if (this._maintenanceThread.IsRunning)
                    return;
                this._maintenanceThread.Start();
            }
            else
            {
                this._maintenanceThread = new SockIOPool.MaintenanceThread(this);
                this._maintenanceThread.Interval = this._maintThreadSleep;
                this._maintenanceThread.Start();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void StopMaintenanceThread()
        {
            if (this._maintenanceThread == null || !this._maintenanceThread.IsRunning)
                return;
            this._maintenanceThread.StopThread();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void SelfMaintain()
        {
            foreach (string host in new IteratorIsolateCollection((IEnumerable)this._availPool.Keys))
            {
                Hashtable hashtable = (Hashtable)this._availPool[(object)host];
                if (hashtable.Count < this._minConns)
                {
                    int num = this._minConns - hashtable.Count;
                    for (int index = 0; index < num; ++index)
                    {
                        SockIO socket = this.CreateSocket(host);
                        if (socket != null)
                            SockIOPool.AddSocketToPool(this._availPool, host, socket);
                        else
                            break;
                    }
                }
                else if (hashtable.Count > this._maxConns)
                {
                    int num1 = hashtable.Count - this._maxConns;
                    int num2 = num1 <= this._poolMultiplier ? num1 : num1 / this._poolMultiplier;
                    foreach (SockIO sockIo in new IteratorIsolateCollection((IEnumerable)hashtable.Keys))
                    {
                        if (num2 > 0)
                        {
                            if (((DateTime)hashtable[(object)sockIo]).AddMilliseconds((double)this._maxIdle) < DateTime.Now)
                            {
                                try
                                {
                                    sockIo.TrueClose();
                                }
                                catch
                                {
                                }
                                hashtable.Remove((object)sockIo);
                                --num2;
                            }
                        }
                        else
                            break;
                    }
                }
                this._createShift[(object)host] = (object)0;
            }
            foreach (string str in (IEnumerable)this._busyPool.Keys)
            {
                Hashtable hashtable = (Hashtable)this._busyPool[(object)str];
                foreach (SockIO sockIo in new IteratorIsolateCollection((IEnumerable)hashtable.Keys))
                {
                    if (((DateTime)hashtable[(object)sockIo]).AddMilliseconds((double)this._maxBusyTime) < DateTime.Now)
                    {
                        try
                        {
                            sockIo.TrueClose();
                        }
                        catch
                        {
                        }
                        hashtable.Remove((object)sockIo);
                    }
                }
            }
        }

        private static string GetLocalizedString(string key)
        {
            return SockIOPool._resourceManager.GetString(key);
        }

        private class MaintenanceThread
        {
            private long _interval = 3000L;
            private Thread _thread;
            private SockIOPool _pool;
            private bool _stopThread;

            public long Interval
            {
                get
                {
                    return this._interval;
                }
                set
                {
                    this._interval = value;
                }
            }

            public bool IsRunning
            {
                get
                {
                    return this._thread.IsAlive;
                }
            }

            private MaintenanceThread()
            {
            }

            public MaintenanceThread(SockIOPool pool)
            {
                this._thread = new Thread(new ThreadStart(this.Maintain));
                this._pool = pool;
            }

            public void StopThread()
            {
                this._stopThread = true;
                this._thread.Interrupt();
            }

            private void Maintain()
            {
                while (!this._stopThread)
                {
                    try
                    {
                        Thread.Sleep((int)this._interval);
                        if (this._pool.Initialized)
                            this._pool.SelfMaintain();
                    }
                    catch (ThreadInterruptedException ex)
                    {
                    }
                    catch
                    {
                    }
                }
            }

            public void Start()
            {
                this._stopThread = false;
                this._thread.Start();
            }
        }
    }
}

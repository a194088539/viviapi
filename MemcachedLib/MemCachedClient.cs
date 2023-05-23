using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using viviapi.SysConfig;

namespace MemcachedLib
{
    public class MemcachedClient
    {
        private static readonly BinaryFormatter bf = new BinaryFormatter();
        private const string VALUE = "VALUE";
        private const string STATS = "STAT";
        private const string ITEM = "ITEM";
        private const string DELETED = "DELETED";
        private const string NOTFOUND = "NOT_FOUND";
        private const string STORED = "STORED";
        private const string NOTSTORED = "NOT_STORED";
        private const string OK = "OK";
        private const string END = "END";
        private const string ERROR = "ERROR";
        private const string CLIENT_ERROR = "CLIENT_ERROR";
        private const string SERVER_ERROR = "SERVER_ERROR";
        private const int COMPRESS_THRESH = 30720;
        private const int F_COMPRESSED = 2;
        private const int F_SERIALIZED = 8;
        private bool _primitiveAsString;
        private bool _compressEnable;
        private long _compressThreshold;
        private string _defaultEncoding;
        private string _poolName;

        public string PoolName
        {
            get
            {
                return this._poolName;
            }
            set
            {
                this._poolName = value;
            }
        }

        public bool PrimitiveAsString
        {
            get
            {
                return this._primitiveAsString;
            }
            set
            {
                this._primitiveAsString = value;
            }
        }

        public string DefaultEncoding
        {
            get
            {
                return this._defaultEncoding;
            }
            set
            {
                this._defaultEncoding = value;
            }
        }

        public bool EnableCompression
        {
            get
            {
                return this._compressEnable;
            }
            set
            {
                this._compressEnable = value;
            }
        }

        public long CompressionThreshold
        {
            get
            {
                return this._compressThreshold;
            }
            set
            {
                this._compressThreshold = value;
            }
        }

        public MemcachedClient()
        {
            this.Init();
        }

        private void Init()
        {
            this._primitiveAsString = false;
            this._compressEnable = true;
            this._compressThreshold = 30720L;
            this._defaultEncoding = "UTF-8";
            this._poolName = MemcachedClient.GetLocalizedString("default instance");
        }

        public bool KeyExists(string key)
        {
            return this.Get(key, (object)null, true) != null;
        }

        public bool Delete(string key)
        {
            return this.Delete(key, (object)null, DateTime.MaxValue);
        }

        public bool Delete(string key, DateTime expiry)
        {
            return this.Delete(key, (object)null, expiry);
        }

        public bool Delete(string key, object hashCode, DateTime expiry)
        {
            if (key == null)
                return false;
            SockIO sockIo = SockIOPool.GetInstance(this._poolName).GetSock(key, hashCode);
            if (sockIo == null)
                return false;
            StringBuilder stringBuilder = new StringBuilder("delete ").Append(key);
            if (expiry != DateTime.MaxValue)
                stringBuilder.Append(" " + (object)(MemcachedClient.GetExpirationTime(expiry) / 1000));
            stringBuilder.Append("\r\n");
            try
            {
                sockIo.Write(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
                sockIo.Flush();
                if ("DELETED" == sockIo.ReadLine())
                {
                    sockIo.Close();
                    sockIo = (SockIO)null;
                    return true;
                }
            }
            catch
            {
                try
                {
                    sockIo.TrueClose();
                }
                catch
                {
                }
                sockIo = (SockIO)null;
            }
            if (sockIo != null)
                sockIo.Close();
            return false;
        }

        private static int GetExpirationTime(DateTime expiration)
        {
            if (expiration <= DateTime.Now)
                return 0;
            TimeSpan timeSpan = new TimeSpan(29, 23, 59, 59);
            if (expiration.Subtract(DateTime.Now) > timeSpan)
                return (int)timeSpan.TotalSeconds;
            return (int)expiration.Subtract(DateTime.Now).TotalSeconds;
        }

        public bool Set(string key, object value)
        {
            return this.Set("set", key, value, DateTime.MaxValue, (object)null, this._primitiveAsString);
        }

        public bool Set(string key, object value, int hashCode)
        {
            return this.Set("set", key, value, DateTime.MaxValue, (object)hashCode, this._primitiveAsString);
        }

        public bool Set(string key, object value, DateTime expiry)
        {
            return this.Set("set", key, value, expiry, (object)null, this._primitiveAsString);
        }

        public bool Set(string key, object value, DateTime expiry, int hashCode)
        {
            return this.Set("set", key, value, expiry, (object)hashCode, this._primitiveAsString);
        }

        public bool Add(string key, object value)
        {
            return this.Set("add", key, value, DateTime.MaxValue, (object)null, this._primitiveAsString);
        }

        public bool Add(string key, object value, int hashCode)
        {
            return this.Set("add", key, value, DateTime.MaxValue, (object)hashCode, this._primitiveAsString);
        }

        public bool Add(string key, object value, DateTime expiry)
        {
            return this.Set("add", key, value, expiry, (object)null, this._primitiveAsString);
        }

        public bool Add(string key, object value, DateTime expiry, int hashCode)
        {
            return this.Set("add", key, value, expiry, (object)hashCode, this._primitiveAsString);
        }

        public bool Replace(string key, object value)
        {
            return this.Set("replace", key, value, DateTime.MaxValue, (object)null, this._primitiveAsString);
        }

        public bool Replace(string key, object value, int hashCode)
        {
            return this.Set("replace", key, value, DateTime.MaxValue, (object)hashCode, this._primitiveAsString);
        }

        public bool Replace(string key, object value, DateTime expiry)
        {
            return this.Set("replace", key, value, expiry, (object)null, this._primitiveAsString);
        }

        public bool Replace(string key, object value, DateTime expiry, int hashCode)
        {
            return this.Set("replace", key, value, expiry, (object)hashCode, this._primitiveAsString);
        }

        public static byte[] Serialize(object value)
        {
            if (value == null)
                return (byte[])null;
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Seek(0L, SeekOrigin.Begin);
            MemcachedClient.bf.Serialize((Stream)memoryStream, value);
            return memoryStream.ToArray();
        }

        public static object Deserialize(byte[] someBytes)
        {
            if (someBytes == null)
                return (object)null;
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(someBytes, 0, someBytes.Length);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            return MemcachedClient.bf.Deserialize((Stream)memoryStream);
        }

        public static string ToBase64(byte[] binBuffer)
        {
            char[] outArray = new char[(int)Math.Ceiling((double)binBuffer.Length / 3.0) * 4];
            Convert.ToBase64CharArray(binBuffer, 0, binBuffer.Length, outArray, 0);
            return new string(outArray);
        }

        public static byte[] Base64ToBytes(string base64)
        {
            char[] inArray = base64.ToCharArray();
            return Convert.FromBase64CharArray(inArray, 0, inArray.Length);
        }

        private bool Set(string cmdname, string key, object obj, DateTime expiry, object hashCode, bool asString)
        {
            if (expiry < DateTime.Now)
                return true;
            if (cmdname == null || cmdname.Trim().Length == 0 || key == null || key.Length == 0)
                return false;
            SockIO sockIo1 = SockIOPool.GetInstance(this._poolName).GetSock(key, hashCode);
            if (sockIo1 == null)
                return false;
            if (expiry == DateTime.MaxValue)
                expiry = new DateTime(0L);
            int num = 0;
            byte[] numArray;
            int count;
            SockIO sockIo2;
            if (NativeHandler.IsHandled(obj))
            {
                if (asString)
                {
                    if (obj != null)
                    {
                        try
                        {
                            numArray = Encoding.UTF8.GetBytes(obj.ToString());
                            count = numArray.Length;
                        }
                        catch
                        {
                            sockIo1.Close();
                            sockIo2 = (SockIO)null;
                            return false;
                        }
                    }
                    else
                    {
                        numArray = new byte[0];
                        count = 0;
                    }
                }
                else
                {
                    try
                    {
                        numArray = NativeHandler.Encode(obj);
                        if (MemCachedConfig.ApplyBase64)
                            numArray = Encoding.UTF8.GetBytes(MemcachedClient.ToBase64(MemcachedClient.Serialize(obj)));
                        count = numArray.Length;
                    }
                    catch
                    {
                        sockIo1.Close();
                        sockIo2 = (SockIO)null;
                        return false;
                    }
                }
            }
            else if (obj != null)
            {
                try
                {
                    numArray = MemcachedClient.Serialize(obj);
                    if (MemCachedConfig.ApplyBase64)
                        numArray = Encoding.UTF8.GetBytes(MemcachedClient.ToBase64(numArray));
                    count = numArray.Length;
                    num |= 8;
                }
                catch
                {
                    sockIo1.Close();
                    sockIo2 = (SockIO)null;
                    return false;
                }
            }
            else
            {
                numArray = new byte[0];
                count = 0;
            }
            try
            {
                string s = cmdname + (object)" " + key + " " + (string)(object)num + " " + (string)(object)MemcachedClient.GetExpirationTime(expiry) + " " + (string)(object)count + "\r\n";
                sockIo1.Write(Encoding.UTF8.GetBytes(s));
                sockIo1.Write(numArray, 0, count);
                sockIo1.Write(Encoding.UTF8.GetBytes("\r\n"));
                sockIo1.Flush();
                if ("STORED" == sockIo1.ReadLine())
                {
                    sockIo1.Close();
                    sockIo1 = (SockIO)null;
                    return true;
                }
            }
            catch
            {
                try
                {
                    sockIo1.TrueClose();
                }
                catch
                {
                }
                sockIo1 = (SockIO)null;
            }
            if (sockIo1 != null)
                sockIo1.Close();
            return false;
        }

        public bool StoreCounter(string key, long counter)
        {
            return this.Set("set", key, (object)counter, DateTime.MaxValue, (object)null, true);
        }

        public bool StoreCounter(string key, long counter, int hashCode)
        {
            return this.Set("set", key, (object)counter, DateTime.MaxValue, (object)hashCode, true);
        }

        public long GetCounter(string key)
        {
            return this.GetCounter(key, (object)null);
        }

        public long GetCounter(string key, object hashCode)
        {
            if (key == null)
                return -1L;
            long num = -1L;
            try
            {
                num = long.Parse((string)this.Get(key, hashCode, true), (IFormatProvider)new NumberFormatInfo());
            }
            catch (ArgumentException ex)
            {
            }
            return num;
        }

        public long Increment(string key)
        {
            return this.IncrementOrDecrement("incr", key, 1L, (object)null);
        }

        public long Increment(string key, long inc)
        {
            return this.IncrementOrDecrement("incr", key, inc, (object)null);
        }

        public long Increment(string key, long inc, int hashCode)
        {
            return this.IncrementOrDecrement("incr", key, inc, (object)hashCode);
        }

        public long Decrement(string key)
        {
            return this.IncrementOrDecrement("decr", key, 1L, (object)null);
        }

        public long Decrement(string key, long inc)
        {
            return this.IncrementOrDecrement("decr", key, inc, (object)null);
        }

        public long Decrement(string key, long inc, int hashCode)
        {
            return this.IncrementOrDecrement("decr", key, inc, (object)hashCode);
        }

        private long IncrementOrDecrement(string cmdname, string key, long inc, object hashCode)
        {
            SockIO sockIo = SockIOPool.GetInstance(this._poolName).GetSock(key, hashCode);
            if (sockIo == null)
                return -1L;
            try
            {
                string s = cmdname + (object)" " + key + " " + (string)(object)inc + "\r\n";
                sockIo.Write(Encoding.UTF8.GetBytes(s));
                sockIo.Flush();
                string str = sockIo.ReadLine();
                if (new Regex("\\d+").Match(str).Success)
                {
                    sockIo.Close();
                    return long.Parse(str, (IFormatProvider)new NumberFormatInfo());
                }
            }
            catch
            {
                try
                {
                    sockIo.TrueClose();
                }
                catch (IOException ex)
                {
                }
                sockIo = (SockIO)null;
            }
            if (sockIo != null)
                sockIo.Close();
            return -1L;
        }

        public object Get(string key)
        {
            return this.Get(key, (object)null, false);
        }

        public object Get(string key, int hashCode)
        {
            return this.Get(key, (object)hashCode, false);
        }

        public object Get(string key, object hashCode, bool asString)
        {
            SockIO sock = SockIOPool.GetInstance(this._poolName).GetSock(key, hashCode);
            if (sock == null)
                return (object)null;
            SockIO sockIo;
            try
            {
                string s = "get " + key + "\r\n";
                sock.Write(Encoding.UTF8.GetBytes(s));
                sock.Flush();
                Hashtable hm = new Hashtable();
                this.LoadItems(sock, hm, asString);
                sock.Close();
                return hm[(object)key];
            }
            catch
            {
                try
                {
                    sock.TrueClose();
                }
                catch
                {
                }
                sockIo = (SockIO)null;
            }
            if (sockIo != null)
                sockIo.Close();
            return (object)null;
        }

        public object[] GetMultipleArray(string[] keys)
        {
            return this.GetMultipleArray(keys, (int[])null, false);
        }

        public object[] GetMultipleArray(string[] keys, int[] hashCodes)
        {
            return this.GetMultipleArray(keys, hashCodes, false);
        }

        public object[] GetMultipleArray(string[] keys, int[] hashCodes, bool asString)
        {
            if (keys == null)
                return new object[0];
            Hashtable multiple = this.GetMultiple(keys, hashCodes, asString);
            object[] objArray = new object[keys.Length];
            for (int index = 0; index < keys.Length; ++index)
                objArray[index] = multiple[(object)keys[index]];
            return objArray;
        }

        public Hashtable GetMultiple(string[] keys)
        {
            return this.GetMultiple(keys, (int[])null, false);
        }

        public Hashtable GetMultiple(string[] keys, int[] hashCodes)
        {
            return this.GetMultiple(keys, hashCodes, false);
        }

        public Hashtable GetMultiple(string[] keys, int[] hashCodes, bool asString)
        {
            if (keys == null)
                return new Hashtable();
            Hashtable hashtable = new Hashtable();
            for (int index = 0; index < keys.Length; ++index)
            {
                object hashCode = (object)null;
                if (hashCodes != null && hashCodes.Length > index)
                    hashCode = (object)hashCodes[index];
                SockIO sock = SockIOPool.GetInstance(this._poolName).GetSock(keys[index], hashCode);
                if (sock != null)
                {
                    if (!hashtable.ContainsKey((object)sock.Host))
                        hashtable[(object)sock.Host] = (object)new StringBuilder();
                    ((StringBuilder)hashtable[(object)sock.Host]).Append(" " + keys[index]);
                    sock.Close();
                }
            }
            Hashtable hm = new Hashtable();
            ArrayList arrayList = new ArrayList();
            foreach (string host in (IEnumerable)hashtable.Keys)
            {
                SockIO sock = SockIOPool.GetInstance(this._poolName).GetConnection(host);
                try
                {
                    string s = "get" + (object)(StringBuilder)hashtable[(object)host] + "\r\n";
                    sock.Write(Encoding.UTF8.GetBytes(s));
                    sock.Flush();
                    this.LoadItems(sock, hm, asString);
                }
                catch
                {
                    arrayList.Add((object)host);
                    try
                    {
                        sock.TrueClose();
                    }
                    catch
                    {
                    }
                    sock = (SockIO)null;
                }
                if (sock != null)
                    sock.Close();
            }
            foreach (string str in arrayList)
                hashtable.Remove((object)str);
            return hm;
        }

        private void LoadItems(SockIO sock, Hashtable hm, bool asString)
        {
            while (true)
            {
                string str = sock.ReadLine();
                if (str.StartsWith("VALUE"))
                {
                    string[] strArray = str.Split(' ');
                    string newValue = strArray[1];
                    int num = int.Parse(strArray[2], (IFormatProvider)new NumberFormatInfo());
                    int count = int.Parse(strArray[3], (IFormatProvider)new NumberFormatInfo());
                    byte[] numArray = new byte[count];
                    sock.Read(numArray);
                    sock.ClearEndOfLine();
                    object obj;
                    if ((num & 8) == 0)
                    {
                        if (this._primitiveAsString || asString)
                        {
                            obj = (object)Encoding.GetEncoding(this._defaultEncoding).GetString(numArray);
                        }
                        else
                        {
                            try
                            {
                                obj = !MemCachedConfig.ApplyBase64 ? NativeHandler.Decode(numArray) : MemcachedClient.Deserialize(MemcachedClient.Base64ToBytes(Encoding.UTF8.GetString(numArray, 0, count)));
                            }
                            catch (Exception ex)
                            {
                                throw new IOException(MemcachedClient.GetLocalizedString("loaditems deserialize error").Replace("$$Key$$", newValue), ex);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            if (MemCachedConfig.ApplyBase64)
                                numArray = MemcachedClient.Base64ToBytes(Encoding.UTF8.GetString(numArray, 0, count));
                            obj = MemcachedClient.Deserialize(numArray);
                        }
                        catch (SerializationException ex)
                        {
                            throw new IOException(MemcachedClient.GetLocalizedString("loaditems SerializationException").Replace("$$Key$$", newValue), (Exception)ex);
                        }
                    }
                    hm[(object)newValue] = obj;
                }
                else if ("END" == str || "ERROR" == str || "SERVER_ERROR" == str || "NOT_FOUND" == str)
                    break;
            }
        }

        public bool FlushAll()
        {
            return this.FlushAll((ArrayList)null);
        }

        public bool FlushAll(ArrayList servers)
        {
            SockIOPool instance = SockIOPool.GetInstance(this._poolName);
            if (instance == null)
                return false;
            if (servers == null)
                servers = instance.Servers;
            if (servers == null || servers.Count <= 0)
                return false;
            bool flag = true;
            for (int index = 0; index < servers.Count; ++index)
            {
                SockIO sockIo = instance.GetConnection((string)servers[index]);
                if (sockIo == null)
                {
                    flag = false;
                }
                else
                {
                    string s = "flush_all\r\n";
                    try
                    {
                        sockIo.Write(Encoding.UTF8.GetBytes(s));
                        sockIo.Flush();
                        flag = "OK" == sockIo.ReadLine() && flag;
                    }
                    catch
                    {
                        try
                        {
                            sockIo.TrueClose();
                        }
                        catch
                        {
                        }
                        flag = false;
                        sockIo = (SockIO)null;
                    }
                    if (sockIo != null)
                        sockIo.Close();
                }
            }
            return flag;
        }

        public Hashtable Stats()
        {
            return this.Stats((ArrayList)null, (string)null, (string)null);
        }

        public Hashtable Stats(ArrayList servers, string command, string poolName)
        {
            SockIOPool sockIoPool = !string.IsNullOrEmpty(poolName) ? SockIOPool.GetInstance(poolName) : SockIOPool.GetInstance(this._poolName);
            if (sockIoPool == null)
                return (Hashtable)null;
            if (servers == null)
                servers = sockIoPool.Servers;
            if (servers == null || servers.Count <= 0)
                return (Hashtable)null;
            Hashtable hashtable1 = new Hashtable();
            for (int index = 0; index < servers.Count; ++index)
            {
                SockIO sockIo = sockIoPool.GetConnection((string)servers[index]);
                if (sockIo != null)
                {
                    command = string.IsNullOrEmpty(command) ? "stats\r\n" : command + "\r\n";
                    try
                    {
                        sockIo.Write(Encoding.UTF8.GetBytes(command));
                        sockIo.Flush();
                        Hashtable hashtable2 = new Hashtable();
                        while (true)
                        {
                            string str1 = sockIo.ReadLine();
                            if (str1.StartsWith("STAT") || str1.StartsWith("ITEM"))
                            {
                                string[] strArray = str1.Split(' ');
                                string str2 = strArray[1];
                                string str3 = strArray[2];
                                hashtable2[(object)str2] = (object)str3;
                            }
                            else if ("END" == str1)
                                break;
                            hashtable1[servers[index]] = (object)hashtable2;
                        }
                    }
                    catch
                    {
                        try
                        {
                            sockIo.TrueClose();
                        }
                        catch
                        {
                        }
                        sockIo = (SockIO)null;
                    }
                    if (sockIo != null)
                        sockIo.Close();
                }
            }
            return hashtable1;
        }

        private static string GetLocalizedString(string key)
        {
            return string.Empty;
        }
    }
}

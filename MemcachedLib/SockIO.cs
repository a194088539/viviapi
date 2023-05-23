using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using System.Threading;

namespace MemcachedLib
{
    public class SockIO
    {
        private static ResourceManager _resourceManager = new ResourceManager("Discuz.Cache.MemCached.StringMessages", typeof(SockIO).Assembly);
        private static int IdGenerator;
        private int _id;
        private DateTime _created;
        private SockIOPool _pool;
        private string _host;
        private Socket _socket;
        private Stream _networkStream;

        public string Host
        {
            get
            {
                return this._host;
            }
        }

        public bool IsConnected
        {
            get
            {
                return this._socket != null && this._socket.Connected;
            }
        }

        private SockIO()
        {
            this._id = Interlocked.Increment(ref SockIO.IdGenerator);
            this._created = DateTime.Now;
        }

        public SockIO(SockIOPool pool, string host, int port, int timeout, int connectTimeout, bool noDelay)
          : this()
        {
            if (host == null || host.Length == 0)
                throw new ArgumentNullException(SockIO.GetLocalizedString("host"), SockIO.GetLocalizedString("null host"));
            this._pool = pool;
            if (connectTimeout > 0)
            {
                this._socket = SockIO.GetSocket(host, port, connectTimeout);
            }
            else
            {
                this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this._socket.Connect((EndPoint)new IPEndPoint(IPAddress.Parse(host), port));
            }
            this._networkStream = (Stream)new BufferedStream((Stream)new NetworkStreamIgnoreSeek(this._socket));
            this._host = host + (object)":" + (string)(object)port;
        }

        public SockIO(SockIOPool pool, string host, int timeout, int connectTimeout, bool noDelay)
          : this()
        {
            if (host == null || host.Length == 0)
                throw new ArgumentNullException(SockIO.GetLocalizedString("host"), SockIO.GetLocalizedString("null host"));
            this._pool = pool;
            string[] strArray = host.Split(':');
            if (connectTimeout > 0)
            {
                this._socket = SockIO.GetSocket(strArray[0], int.Parse(strArray[1], (IFormatProvider)new NumberFormatInfo()), connectTimeout);
            }
            else
            {
                this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this._socket.Connect((EndPoint)new IPEndPoint(IPAddress.Parse(strArray[0]), int.Parse(strArray[1], (IFormatProvider)new NumberFormatInfo())));
            }
            this._networkStream = (Stream)new BufferedStream((Stream)new NetworkStreamIgnoreSeek(this._socket));
            this._host = host;
        }

        protected static Socket GetSocket(string host, int port, int timeout)
        {
            SockIO.ConnectThread connectThread = new SockIO.ConnectThread(host, port);
            connectThread.Start();
            int num = 0;
            int millisecondsTimeout = 25;
            while (num < timeout)
            {
                if (connectThread.IsConnected)
                    return connectThread.Socket;
                if (connectThread.IsError)
                    throw new IOException();
                try
                {
                    Thread.Sleep(millisecondsTimeout);
                }
                catch (ThreadInterruptedException ex)
                {
                }
                num += millisecondsTimeout;
            }
            throw new IOException(SockIO.GetLocalizedString("connect timeout").Replace("$$timeout$$", timeout.ToString((IFormatProvider)new NumberFormatInfo())));
        }

        public void TrueClose()
        {
            bool flag = false;
            StringBuilder stringBuilder = new StringBuilder();
            if (this._socket == null || this._networkStream == null)
            {
                flag = true;
                stringBuilder.Append(SockIO.GetLocalizedString("socket already closed"));
            }
            if (this._socket != null)
            {
                try
                {
                    this._socket.Close();
                }
                catch (IOException ex)
                {
                    stringBuilder.Append(SockIO.GetLocalizedString("error closing socket").Replace("$$ToString$$", this.ToString()).Replace("$$Host$$", this.Host) + Environment.NewLine);
                    stringBuilder.Append(ex.ToString());
                    flag = true;
                }
                catch (SocketException ex)
                {
                    stringBuilder.Append(SockIO.GetLocalizedString("error closing socket").Replace("$$ToString$$", this.ToString()).Replace("$$Host$$", this.Host) + Environment.NewLine);
                    stringBuilder.Append(ex.ToString());
                    flag = true;
                }
            }
            if (this._socket != null)
                this._pool.CheckIn(this, false);
            this._networkStream = (Stream)null;
            this._socket = (Socket)null;
            if (flag)
                throw new IOException(stringBuilder.ToString());
        }

        public void Close()
        {
            this._pool.CheckIn(this);
        }

        public string ReadLine()
        {
            if (this._socket == null || !this._socket.Connected)
                throw new IOException(SockIO.GetLocalizedString("read closed socket"));
            byte[] buffer = new byte[1];
            MemoryStream memoryStream = new MemoryStream();
            bool flag = false;
            while (this._networkStream.Read(buffer, 0, 1) != -1)
            {
                if ((int)buffer[0] == 13)
                    flag = true;
                else if (flag)
                {
                    if ((int)buffer[0] != 10)
                        flag = false;
                    else
                        break;
                }
                memoryStream.Write(buffer, 0, 1);
            }
            if (memoryStream == null || memoryStream.Length <= 0L)
                throw new IOException(SockIO.GetLocalizedString("closing dead stream"));
            return Encoding.UTF8.GetString(memoryStream.GetBuffer()).TrimEnd(char.MinValue, '\r', '\n');
        }

        public void ClearEndOfLine()
        {
            if (this._socket == null || !this._socket.Connected)
                throw new IOException(SockIO.GetLocalizedString("read closed socket"));
            byte[] buffer = new byte[1];
            bool flag = false;
            while (this._networkStream.Read(buffer, 0, 1) != -1)
            {
                if ((int)buffer[0] == 13)
                    flag = true;
                else if (flag)
                {
                    if ((int)buffer[0] == 10)
                        break;
                    flag = false;
                }
            }
        }

        public void Read(byte[] bytes)
        {
            if (this._socket == null || !this._socket.Connected)
                throw new IOException(SockIO.GetLocalizedString("read closed socket"));
            if (bytes == null)
                return;
            int offset = 0;
            while (offset < bytes.Length)
            {
                int num = this._networkStream.Read(bytes, offset, bytes.Length - offset);
                offset += num;
            }
        }

        public void Flush()
        {
            if (this._socket == null || !this._socket.Connected)
                throw new IOException(SockIO.GetLocalizedString("write closed socket"));
            this._networkStream.Flush();
        }

        public void Write(byte[] bytes)
        {
            this.Write(bytes, 0, bytes.Length);
        }

        public void Write(byte[] bytes, int offset, int count)
        {
            if (this._socket == null || !this._socket.Connected)
                throw new IOException(SockIO.GetLocalizedString("write closed socket"));
            if (bytes == null)
                return;
            this._networkStream.Write(bytes, offset, count);
        }

        public override string ToString()
        {
            if (this._socket == null)
                return "";
            return this._id.ToString((IFormatProvider)new NumberFormatInfo());
        }

        private static string GetLocalizedString(string key)
        {
            return SockIO._resourceManager.GetString(key);
        }

        private class ConnectThread
        {
            private Thread _thread;
            private Socket _socket;
            private string _host;
            private int _port;
            private bool _error;

            public bool IsConnected
            {
                get
                {
                    return this._socket != null && this._socket.Connected;
                }
            }

            public bool IsError
            {
                get
                {
                    return this._error;
                }
            }

            public Socket Socket
            {
                get
                {
                    return this._socket;
                }
            }

            public ConnectThread(string host, int port)
            {
                this._host = host;
                this._port = port;
                this._thread = new Thread(new ThreadStart(this.Connect));
                this._thread.IsBackground = true;
            }

            private void Connect()
            {
                try
                {
                    this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this._socket.Connect((EndPoint)new IPEndPoint(IPAddress.Parse(this._host), this._port));
                }
                catch (IOException ex)
                {
                    this._error = true;
                }
                catch
                {
                    this._error = true;
                }
            }

            public void Start()
            {
                this._thread.Start();
            }
        }
    }
}

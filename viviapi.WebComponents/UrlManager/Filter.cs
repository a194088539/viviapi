using System;
using System.IO;
using System.Text;
using System.Web;
using viviLib.ExceptionHandling;

namespace viviLib.WebComponents.UrlManager
{
    public class Filter : Stream
    {
        private Stream _sink;
        private long _position;
        private string filePath;
        private MemoryStream _tempMemoryStream;
        private BinaryWriter _writer;
        private byte[] _buffer;
        private int _count;

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override long Length
        {
            get
            {
                return 0L;
            }
        }

        public override long Position
        {
            get
            {
                return this._position;
            }
            set
            {
                this._position = value;
            }
        }

        private MemoryStream TempMemoryStream
        {
            get
            {
                if (this._tempMemoryStream == null)
                    this._tempMemoryStream = new MemoryStream();
                return this._tempMemoryStream;
            }
        }

        private BinaryWriter BWriter
        {
            get
            {
                if (this._writer == null)
                {
                    string directoryName = Path.GetDirectoryName(this.filePath);
                    if (!Directory.Exists(directoryName))
                        Directory.CreateDirectory(directoryName);
                    this._writer = new BinaryWriter((Stream)this.TempMemoryStream);
                }
                return this._writer;
            }
        }

        public Filter(Stream sink, string file)
        {
            this._sink = sink;
            this.filePath = file;
        }

        public override long Seek(long offset, SeekOrigin direction)
        {
            return this._sink.Seek(offset, direction);
        }

        public override void SetLength(long length)
        {
            this._sink.SetLength(length);
        }

        public override void Close()
        {
            try
            {
                if (this._sink != null)
                    this._sink.Close();
                if (this._tempMemoryStream != null)
                {
                    if (System.IO.File.Exists(this.filePath))
                        viviLib.IO.File.Delete(this.filePath);
                    using (FileStream fileStream = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        this._tempMemoryStream.WriteTo((Stream)fileStream);
                        fileStream.Close();
                    }
                    this._tempMemoryStream.Close();
                }
                if (this._writer == null)
                    return;
                this._writer.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            finally
            {
            }
        }

        public override void Flush()
        {
            this._sink.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this._sink.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (HttpContext.Current.Response.ContentType == "text/html" || HttpContext.Current.Response.ContentType == "text/javascript" || (HttpContext.Current.Response.ContentType == "text/vbscript" || HttpContext.Current.Response.ContentType == "text/ecmascript") || HttpContext.Current.Response.ContentType == "text/Jscript" || HttpContext.Current.Response.ContentType == "text/xml")
            {
                Encoding encoding = Encoding.GetEncoding(HttpContext.Current.Response.Charset);
                string @string = encoding.GetString(buffer, offset, count);
                this._buffer = encoding.GetBytes(@string);
                this._count = encoding.GetByteCount(@string);
                this._sink.Write(this._buffer, 0, this._count);
                this.BWriter.Write(this._buffer, 0, this._count);
            }
            else
                this._sink.Write(buffer, offset, count);
        }
    }
}

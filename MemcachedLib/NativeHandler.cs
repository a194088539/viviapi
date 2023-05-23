namespace MemcachedLib
{
    using System;
    using System.Resources;
    using System.Text;

    public sealed class NativeHandler
    {
        private static ResourceManager _resourceManager = new ResourceManager("Discuz.Cache.MemCached.StringMessages", typeof(SockIOPool).Assembly);
        public const byte BoolMarker = 2;
        public const byte ByteMarker = 1;
        public const byte CharMarker = 5;
        public const byte DateTimeMarker = 11;
        public const byte DoubleMarker = 10;
        public const byte Int16Marker = 9;
        public const byte Int32Marker = 3;
        public const byte Int64Marker = 4;
        public const byte SingleMarker = 8;
        public const byte StringBuilderMarker = 7;
        public const byte StringMarker = 6;

        private NativeHandler()
        {
        }

        public static object Decode(byte[] bytes)
        {
            if ((bytes != null) && (bytes.Length != 0))
            {
                if (bytes[0] == 2)
                {
                    return DecodeBool(bytes);
                }
                if (bytes[0] == 3)
                {
                    return DecodeInteger(bytes);
                }
                if (bytes[0] == 6)
                {
                    return DecodeString(bytes);
                }
                if (bytes[0] == 5)
                {
                    return DecodeCharacter(bytes);
                }
                if (bytes[0] == 1)
                {
                    return DecodeByte(bytes);
                }
                if (bytes[0] == 7)
                {
                    return DecodeStringBuilder(bytes);
                }
                if (bytes[0] == 9)
                {
                    return DecodeShort(bytes);
                }
                if (bytes[0] == 4)
                {
                    return DecodeLong(bytes);
                }
                if (bytes[0] == 10)
                {
                    return DecodeDouble(bytes);
                }
                if (bytes[0] == 8)
                {
                    return DecodeFloat(bytes);
                }
                if (bytes[0] == 11)
                {
                    return DecodeDate(bytes);
                }
            }
            return null;
        }

        public static bool DecodeBool(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes", GetLocalizedString("parameter cannot be null"));
            }
            return (bytes[1] == 1);
        }

        public static byte DecodeByte(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes", GetLocalizedString("parameter cannot be null"));
            }
            return bytes[1];
        }

        public static char DecodeCharacter(byte[] bytes)
        {
            return (char)DecodeInteger(bytes);
        }

        public static DateTime DecodeDate(byte[] bytes)
        {
            return new DateTime(ToLong(bytes));
        }

        public static double DecodeDouble(byte[] bytes)
        {
            return BitConverter.ToDouble(bytes, 1);
        }

        public static float DecodeFloat(byte[] bytes)
        {
            return BitConverter.ToSingle(bytes, 1);
        }

        public static int DecodeInteger(byte[] bytes)
        {
            return ToInt(bytes);
        }

        public static long DecodeLong(byte[] bytes)
        {
            return ToLong(bytes);
        }

        public static short DecodeShort(byte[] bytes)
        {
            return (short)DecodeInteger(bytes);
        }

        public static string DecodeString(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }
            return Encoding.UTF8.GetString(bytes, 1, bytes.Length - 1);
        }

        public static StringBuilder DecodeStringBuilder(byte[] bytes)
        {
            return new StringBuilder(DecodeString(bytes));
        }

        public static byte[] Encode(bool value)
        {
            byte[] bytes = new byte[2];
            bytes[0] = 2;
            if (value)
            {
                bytes[1] = 1;
                return bytes;
            }
            bytes[1] = 0;
            return bytes;
        }

        public static byte[] Encode(byte value)
        {
            return new byte[] { 1, value };
        }

        public static byte[] Encode(char value)
        {
            byte[] result = Encode((short)value);
            result[0] = 5;
            return result;
        }

        public static byte[] Encode(DateTime value)
        {
            byte[] bytes = GetBytes(value.Ticks);
            bytes[0] = 11;
            return bytes;
        }

        public static byte[] Encode(double value)
        {
            byte[] temp = BitConverter.GetBytes(value);
            byte[] bytes = new byte[temp.Length + 1];
            bytes[0] = 10;
            Array.Copy(temp, 0, bytes, 1, temp.Length);
            return bytes;
        }

        public static byte[] Encode(short value)
        {
            byte[] bytes = Encode((int)value);
            bytes[0] = 9;
            return bytes;
        }

        public static byte[] Encode(int value)
        {
            byte[] bytes = GetBytes(value);
            bytes[0] = 3;
            return bytes;
        }

        public static byte[] Encode(long value)
        {
            byte[] bytes = GetBytes(value);
            bytes[0] = 4;
            return bytes;
        }

        public static byte[] Encode(object value)
        {
            if (value == null)
            {
                return new byte[0];
            }
            if (value is bool)
            {
                return Encode((bool)value);
            }
            if (value is int)
            {
                return Encode((int)value);
            }
            if (value is char)
            {
                return Encode((char)value);
            }
            if (value is byte)
            {
                return Encode((byte)value);
            }
            if (value is short)
            {
                return Encode((short)value);
            }
            if (value is long)
            {
                return Encode((long)value);
            }
            if (value is double)
            {
                return Encode((double)value);
            }
            if (value is float)
            {
                return Encode((float)value);
            }
            string tempstr = value as string;
            if (tempstr != null)
            {
                return Encode(tempstr);
            }
            StringBuilder tempsb = value as StringBuilder;
            if (tempsb != null)
            {
                return Encode(tempsb);
            }
            if (value is DateTime)
            {
                return Encode((DateTime)value);
            }
            return null;
        }

        public static byte[] Encode(float value)
        {
            byte[] temp = BitConverter.GetBytes(value);
            byte[] bytes = new byte[temp.Length + 1];
            bytes[0] = 8;
            Array.Copy(temp, 0, bytes, 1, temp.Length);
            return bytes;
        }

        public static byte[] Encode(string value)
        {
            if (value == null)
            {
                return new byte[] { 6 };
            }
            byte[] asBytes = Encoding.UTF8.GetBytes(value);
            byte[] result = new byte[asBytes.Length + 1];
            result[0] = 6;
            Array.Copy(asBytes, 0, result, 1, asBytes.Length);
            return result;
        }

        public static byte[] Encode(StringBuilder value)
        {
            if (value == null)
            {
                return new byte[] { 7 };
            }
            byte[] bytes = Encode(value.ToString());
            bytes[0] = 7;
            return bytes;
        }

        public static byte[] GetBytes(int value)
        {
            byte b0 = (byte)((value >> 0x18) & 0xff);
            byte b1 = (byte)((value >> 0x10) & 0xff);
            byte b2 = (byte)((value >> 8) & 0xff);
            byte b3 = (byte)(value & 0xff);
            byte[] bytes = new byte[5];
            bytes[1] = b0;
            bytes[2] = b1;
            bytes[3] = b2;
            bytes[4] = b3;
            return bytes;
        }

        public static byte[] GetBytes(long value)
        {
            byte b0 = (byte)((value >> 0x38) & 0xffL);
            byte b1 = (byte)((value >> 0x30) & 0xffL);
            byte b2 = (byte)((value >> 40) & 0xffL);
            byte b3 = (byte)((value >> 0x20) & 0xffL);
            byte b4 = (byte)((value >> 0x18) & 0xffL);
            byte b5 = (byte)((value >> 0x10) & 0xffL);
            byte b6 = (byte)((value >> 8) & 0xffL);
            byte b7 = (byte)(value & 0xffL);
            byte[] bytes = new byte[9];
            bytes[1] = b0;
            bytes[2] = b1;
            bytes[3] = b2;
            bytes[4] = b3;
            bytes[5] = b4;
            bytes[6] = b5;
            bytes[7] = b6;
            bytes[8] = b7;
            return bytes;
        }

        private static string GetLocalizedString(string key)
        {
            return _resourceManager.GetString(key);
        }

        public static bool IsHandled(object value)
        {
            return ((((((value is bool) || (value is byte)) || ((value is string) || (value is char))) || (((value is StringBuilder) || (value is short)) || ((value is long) || (value is double)))) || ((value is float) || (value is DateTime))) || (value is int));
        }

        public static int ToInt(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes", GetLocalizedString("parameter cannot be null"));
            }
            return ((((bytes[4] & 0xff) + ((bytes[3] & 0xff) << 8)) + ((bytes[2] & 0xff) << 0x10)) + ((bytes[1] & 0xff) << 0x18));
        }

        public static long ToLong(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes", GetLocalizedString("parameter cannot be null"));
            }
            return ((((((((bytes[8] & 0xffL) + ((bytes[7] & 0xffL) << 8)) + ((bytes[6] & 0xffL) << 0x10)) + ((bytes[5] & 0xffL) << 0x18)) + ((bytes[4] & 0xffL) << 0x20)) + ((bytes[3] & 0xffL) << 40)) + ((bytes[2] & 0xffL) << 0x30)) + ((bytes[1] & 0xffL) << 0x38));
        }
    }
}


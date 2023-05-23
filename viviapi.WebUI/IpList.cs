namespace viviapi.WebUI
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public class IpList
    {
        private string country;
        private int countryFlag;
        private string dataPath = (AppDomain.CurrentDomain.BaseDirectory.ToString() + "/App_Data/ipList.dat");
        private long endIp;
        private long endIpOff;
        private string errMsg;
        private long firstStartIp;
        private string ip;
        private long lastStartIp;
        private string local;
        private FileStream objfs;
        private long startIp;

        private string GetCountry()
        {
            switch (this.countryFlag)
            {
                case 1:
                case 2:
                    this.country = this.GetFlagStr(this.endIpOff + 4L);
                    this.local = (1 == this.countryFlag) ? " " : this.GetFlagStr(this.endIpOff + 8L);
                    break;

                default:
                    this.country = this.GetFlagStr(this.endIpOff + 4L);
                    this.local = this.GetFlagStr(this.objfs.Position);
                    break;
            }
            return " ";
        }

        private long GetEndIp()
        {
            this.objfs.Position = this.endIpOff;
            byte[] buffer = new byte[5];
            this.objfs.Read(buffer, 0, 5);
            this.endIp = ((Convert.ToInt64(buffer[0].ToString()) + (Convert.ToInt64(buffer[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString()) * 0x100L) * 0x100L)) + (((Convert.ToInt64(buffer[3].ToString()) * 0x100L) * 0x100L) * 0x100L);
            this.countryFlag = buffer[4];
            return this.endIp;
        }

        private string GetFlagStr(long offSet)
        {
            int num = 0;
            byte[] buffer = new byte[3];
            while (true)
            {
                this.objfs.Position = offSet;
                num = this.objfs.ReadByte();
                if ((num != 1) && (num != 2))
                {
                    if (offSet < 12L)
                    {
                        return " ";
                    }
                    this.objfs.Position = offSet;
                    return this.GetStr();
                }
                this.objfs.Read(buffer, 0, 3);
                if (num == 2)
                {
                    this.countryFlag = 2;
                    this.endIpOff = offSet - 4L;
                }
                offSet = (Convert.ToInt64(buffer[0].ToString()) + (Convert.ToInt64(buffer[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString()) * 0x100L) * 0x100L);
            }
        }

        private long GetStartIp(long recNO)
        {
            long num = this.firstStartIp + (recNO * 7L);
            this.objfs.Position = num;
            byte[] buffer = new byte[7];
            this.objfs.Read(buffer, 0, 7);
            this.endIpOff = (Convert.ToInt64(buffer[4].ToString()) + (Convert.ToInt64(buffer[5].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[6].ToString()) * 0x100L) * 0x100L);
            this.startIp = ((Convert.ToInt64(buffer[0].ToString()) + (Convert.ToInt64(buffer[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString()) * 0x100L) * 0x100L)) + (((Convert.ToInt64(buffer[3].ToString()) * 0x100L) * 0x100L) * 0x100L);
            return this.startIp;
        }

        private string GetStr()
        {
            byte num = 0;
            byte num2 = 0;
            string str = "";
            byte[] bytes = new byte[2];
            while (true)
            {
                num = (byte)this.objfs.ReadByte();
                if (num == 0)
                {
                    return str;
                }
                if (num > 0x7f)
                {
                    num2 = (byte)this.objfs.ReadByte();
                    bytes[0] = num;
                    bytes[1] = num2;
                    Encoding encoding = Encoding.GetEncoding("GB2312");
                    str = str + encoding.GetString(bytes);
                }
                else
                {
                    str = str + ((char)num);
                }
            }
        }

        private string IntToIP(long ip_Int)
        {
            long num = (long)((ip_Int & 0xff000000L) >> 0x18);
            if (num < 0L)
            {
                num += 0x100L;
            }
            long num2 = (ip_Int & 0xff0000L) >> 0x10;
            if (num2 < 0L)
            {
                num2 += 0x100L;
            }
            long num3 = (ip_Int & 0xff00L) >> 8;
            if (num3 < 0L)
            {
                num3 += 0x100L;
            }
            long num4 = ip_Int & 0xffL;
            if (num4 < 0L)
            {
                num4 += 0x100L;
            }
            return (num.ToString() + "." + num2.ToString() + "." + num3.ToString() + "." + num4.ToString());
        }

        public string IPAddInfo()
        {
            this.QQwry();
            return this.local;
        }

        public string IPLocation()
        {
            this.QQwry();
            return this.country;
        }

        public string IPLocation(string dataPath, string ip)
        {
            try
            {
                this.ip = ip;
                this.QQwry();
                return (this.country + this.local);
            }
            catch
            {
                return "火星";
            }
        }

        private long IpToInt(string ip)
        {
            char[] separator = new char[] { '.' };
            if (ip.Split(separator).Length == 3)
            {
                ip = ip + ".0";
            }
            string[] strArray = ip.Split(separator);
            long num = ((long.Parse(strArray[0]) * 0x100L) * 0x100L) * 0x100L;
            long num2 = (long.Parse(strArray[1]) * 0x100L) * 0x100L;
            long num3 = long.Parse(strArray[2]) * 0x100L;
            long num4 = long.Parse(strArray[3]);
            return (((num + num2) + num3) + num4);
        }

        private int QQwry()
        {
            string pattern = @"(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))";
            Regex regex = new Regex(pattern);
            if (!regex.Match(this.ip).Success)
            {
                this.errMsg = "未知IP";
                return 4;
            }
            long num = this.IpToInt(this.ip);
            int num2 = 0;
            if ((num >= this.IpToInt("127.0.0.0")) && (num <= this.IpToInt("127.255.255.255")))
            {
                this.country = "局域网IP";
                this.local = "";
                num2 = 1;
            }
            else if ((((num >= this.IpToInt("0.0.0.0")) && (num <= this.IpToInt("2.255.255.255"))) || ((num >= this.IpToInt("64.0.0.0")) && (num <= this.IpToInt("126.255.255.255")))) || ((num >= this.IpToInt("58.0.0.0")) && (num <= this.IpToInt("60.255.255.255"))))
            {
                this.country = "网络保留地址";
                this.local = "";
                num2 = 1;
            }
            this.objfs = new FileStream(this.dataPath, FileMode.Open, FileAccess.Read);
            try
            {
                this.objfs.Position = 0L;
                byte[] buffer = new byte[8];
                this.objfs.Read(buffer, 0, 8);
                this.firstStartIp = ((buffer[0] + (buffer[1] * 0x100)) + ((buffer[2] * 0x100) * 0x100)) + (((buffer[3] * 0x100) * 0x100) * 0x100);
                this.lastStartIp = ((buffer[4] + (buffer[5] * 0x100)) + ((buffer[6] * 0x100) * 0x100)) + (((buffer[7] * 0x100) * 0x100) * 0x100);
                long num3 = Convert.ToInt64((double)(((double)(this.lastStartIp - this.firstStartIp)) / 7.0));
                if (num3 <= 1L)
                {
                    this.country = "FileDataError";
                    this.objfs.Close();
                    return 2;
                }
                long num4 = num3;
                long recNO = 0L;
                long num6 = 0L;
                while (recNO < (num4 - 1L))
                {
                    num6 = (num4 + recNO) / 2L;
                    this.GetStartIp(num6);
                    if (num == this.startIp)
                    {
                        recNO = num6;
                        break;
                    }
                    if (num > this.startIp)
                    {
                        recNO = num6;
                    }
                    else
                    {
                        num4 = num6;
                    }
                }
                this.GetStartIp(recNO);
                this.GetEndIp();
                if ((this.startIp <= num) && (this.endIp >= num))
                {
                    this.GetCountry();
                    this.local = this.local.Replace("（我们一定要解放台湾！！！）", "");
                }
                else
                {
                    num2 = 3;
                    this.country = "火星";
                    this.local = "";
                }
                this.objfs.Close();
                return num2;
            }
            catch
            {
                return 1;
            }
        }

        public string Country
        {
            get
            {
                return this.country;
            }
        }

        public string DataPath
        {
            set
            {
                this.dataPath = value;
            }
        }

        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }
        }

        public string IP
        {
            set
            {
                this.ip = value;
            }
        }

        public string Local
        {
            get
            {
                return this.local;
            }
        }
    }
}


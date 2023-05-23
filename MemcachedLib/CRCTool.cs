namespace MemcachedLib
{
    public class CRCTool
    {
        private int order = 16;
        private ulong polynom = 4129UL;
        private int direct = 1;
        private ulong crcinit = (ulong)ushort.MaxValue;
        private ulong crcxor = 0UL;
        private int refin = 0;
        private int refout = 0;
        private ulong[] crctab = new ulong[256];
        private ulong crcmask;
        private ulong crchighbit;
        private ulong crcinit_direct;
        private ulong crcinit_nondirect;

        public void Init(CRCTool.CRCCode CodingType)
        {
            switch (CodingType)
            {
                case CRCTool.CRCCode.CRC_CCITT:
                    this.order = 16;
                    this.direct = 1;
                    this.polynom = 4129UL;
                    this.crcinit = (ulong)ushort.MaxValue;
                    this.crcxor = 0UL;
                    this.refin = 0;
                    this.refout = 0;
                    break;
                case CRCTool.CRCCode.CRC16:
                    this.order = 16;
                    this.direct = 1;
                    this.polynom = 32773UL;
                    this.crcinit = 0UL;
                    this.crcxor = 0UL;
                    this.refin = 1;
                    this.refout = 1;
                    break;
                case CRCTool.CRCCode.CRC32:
                    this.order = 32;
                    this.direct = 1;
                    this.polynom = 79764919UL;
                    this.crcinit = (ulong)uint.MaxValue;
                    this.crcxor = (ulong)uint.MaxValue;
                    this.refin = 1;
                    this.refout = 1;
                    break;
            }
            this.crcmask = (ulong)((1L << this.order - 1) - 1L << 1) | 1UL;
            this.crchighbit = 1UL << this.order - 1;
            this.generate_crc_table();
            if (this.direct == 0)
            {
                this.crcinit_nondirect = this.crcinit;
                ulong num1 = this.crcinit;
                for (int index = 0; index < this.order; ++index)
                {
                    ulong num2 = num1 & this.crchighbit;
                    num1 <<= 1;
                    if ((long)num2 != 0L)
                        num1 ^= this.polynom;
                }
                this.crcinit_direct = num1 & this.crcmask;
            }
            else
            {
                this.crcinit_direct = this.crcinit;
                ulong num1 = this.crcinit;
                for (int index = 0; index < this.order; ++index)
                {
                    ulong num2 = num1 & 1UL;
                    if ((long)num2 != 0L)
                        num1 ^= this.polynom;
                    num1 >>= 1;
                    if ((long)num2 != 0L)
                        num1 |= this.crchighbit;
                }
                this.crcinit_nondirect = num1;
            }
        }

        public ulong crctablefast(byte[] p)
        {
            ulong crc = this.crcinit_direct;
            if (this.refin != 0)
                crc = this.reflect(crc, this.order);
            if (this.refin == 0)
            {
                for (int index = 0; index < p.Length; ++index)
                    crc = crc << 8 ^ this.crctab[checked((ulong)(unchecked((long)(crc >> this.order - 8)) & (long)byte.MaxValue ^ (long)p[index]))];
            }
            else
            {
                for (int index = 0; index < p.Length; ++index)
                    crc = crc >> 8 ^ this.crctab[checked((ulong)(unchecked((long)crc) & (long)byte.MaxValue ^ (long)p[index]))];
            }
            if ((this.refout ^ this.refin) != 0)
                crc = this.reflect(crc, this.order);
            return (crc ^ this.crcxor) & this.crcmask;
        }

        public ulong crctable(byte[] p)
        {
            ulong crc = this.crcinit_nondirect;
            if (this.refin != 0)
                crc = this.reflect(crc, this.order);
            if (this.refin == 0)
            {
                for (int index = 0; index < p.Length; ++index)
                    crc = (crc << 8 | (ulong)p[index]) ^ this.crctab[checked((ulong)(unchecked((long)(crc >> this.order - 8)) & (long)byte.MaxValue))];
            }
            else
            {
                for (int index = 0; index < p.Length; ++index)
                    crc = (ulong)(((int)(crc >> 8) | (int)p[index] << this.order - 8) ^ (int)this.crctab[checked((ulong)(unchecked((long)crc) & (long)byte.MaxValue))]);
            }
            if (this.refin == 0)
            {
                for (int index = 0; index < this.order / 8; ++index)
                    crc = crc << 8 ^ this.crctab[checked((ulong)(unchecked((long)(crc >> this.order - 8)) & (long)byte.MaxValue))];
            }
            else
            {
                for (int index = 0; index < this.order / 8; ++index)
                    crc = crc >> 8 ^ this.crctab[checked((ulong)(unchecked((long)crc) & (long)byte.MaxValue))];
            }
            if ((this.refout ^ this.refin) != 0)
                crc = this.reflect(crc, this.order);
            return (crc ^ this.crcxor) & this.crcmask;
        }

        public ulong crcbitbybit(byte[] p)
        {
            ulong crc1 = this.crcinit_nondirect;
            for (int index = 0; index < p.Length; ++index)
            {
                ulong crc2 = (ulong)p[index];
                if (this.refin != 0)
                    crc2 = this.reflect(crc2, 8);
                ulong num1 = 128UL;
                while ((long)num1 != 0L)
                {
                    ulong num2 = crc1 & this.crchighbit;
                    crc1 <<= 1;
                    if (((long)crc2 & (long)num1) != 0L)
                        crc1 |= 1UL;
                    if ((long)num2 != 0L)
                        crc1 ^= this.polynom;
                    num1 >>= 1;
                }
            }
            for (int index = 0; index < this.order; ++index)
            {
                ulong num = crc1 & this.crchighbit;
                crc1 <<= 1;
                if ((long)num != 0L)
                    crc1 ^= this.polynom;
            }
            if (this.refout != 0)
                crc1 = this.reflect(crc1, this.order);
            return (crc1 ^ this.crcxor) & this.crcmask;
        }

        public ulong crcbitbybitfast(byte[] p)
        {
            ulong crc1 = this.crcinit_direct;
            for (int index = 0; index < p.Length; ++index)
            {
                ulong crc2 = (ulong)p[index];
                if (this.refin != 0)
                    crc2 = this.reflect(crc2, 8);
                ulong num1 = 128UL;
                while (num1 > 0UL)
                {
                    ulong num2 = crc1 & this.crchighbit;
                    crc1 <<= 1;
                    if ((crc2 & num1) > 0UL)
                        num2 ^= this.crchighbit;
                    if (num2 > 0UL)
                        crc1 ^= this.polynom;
                    num1 >>= 1;
                }
            }
            if (this.refout > 0)
                crc1 = this.reflect(crc1, this.order);
            return (crc1 ^ this.crcxor) & this.crcmask;
        }

        public ushort CalcCRCITT(byte[] p)
        {
            uint num1 = (uint)ushort.MaxValue;
            for (int index1 = 0; index1 < p.Length; ++index1)
            {
                uint num2 = (uint)p[index1] << 8;
                for (int index2 = 0; index2 < 8; ++index2)
                {
                    if ((((int)num1 ^ (int)num2) & 32768) != 0)
                        num1 = (uint)((int)num1 << 1 ^ 4129);
                    else
                        num1 <<= 1;
                    num2 <<= 1;
                }
            }
            return (ushort)num1;
        }

        private ulong reflect(ulong crc, int bitnum)
        {
            ulong num1 = 1UL;
            ulong num2 = 0UL;
            ulong num3 = 1UL << bitnum - 1;
            while ((long)num3 != 0L)
            {
                if (((long)crc & (long)num3) != 0L)
                    num2 |= num1;
                num1 <<= 1;
                num3 >>= 1;
            }
            return num2;
        }

        private void generate_crc_table()
        {
            for (int index1 = 0; index1 < 256; ++index1)
            {
                ulong crc1 = (ulong)index1;
                if (this.refin != 0)
                    crc1 = this.reflect(crc1, 8);
                ulong crc2 = crc1 << this.order - 8;
                for (int index2 = 0; index2 < 8; ++index2)
                {
                    ulong num = crc2 & this.crchighbit;
                    crc2 <<= 1;
                    if ((long)num != 0L)
                        crc2 ^= this.polynom;
                }
                if (this.refin != 0)
                    crc2 = this.reflect(crc2, this.order);
                ulong num1 = crc2 & this.crcmask;
                this.crctab[index1] = num1;
            }
        }

        public enum CRCCode
        {
            CRC_CCITT,
            CRC16,
            CRC32,
        }
    }
}

namespace CryptoKitLib
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [ComImport, Guid("C25BAD24-0E3B-4D5D-8171-62C8195CBE86"), TypeLibType((short)2), ClassInterface((short)0)]
    public class CryptoAgentClass : CryptoAgent, ICryptoAgent
    {
        [return: MarshalAs(UnmanagedType.BStr)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(5)]
        public virtual extern string GetLastErrorDesc();
        [return: MarshalAs(UnmanagedType.BStr)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(1)]
        public virtual extern string SelectSignCertificatebyPFX([In, MarshalAs(UnmanagedType.BStr)] string bstrPFXFileName, [In, MarshalAs(UnmanagedType.BStr)] string bstrPFXPassword);
        [return: MarshalAs(UnmanagedType.BStr)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(3)]
        public virtual extern string SelectVerifyCertbyCer([In, MarshalAs(UnmanagedType.BStr)] string bstrCerFileName);
        [return: MarshalAs(UnmanagedType.BStr)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(2)]
        public virtual extern string SignMessagePKCS1([In, MarshalAs(UnmanagedType.BStr)] string bstrSourceMsg);
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(4)]
        public virtual extern bool VerifyMessageSignaturePKCS1([In, MarshalAs(UnmanagedType.BStr)] string bstrPKCS1Signature, [In, MarshalAs(UnmanagedType.BStr)] string bstrContent);
    }
}


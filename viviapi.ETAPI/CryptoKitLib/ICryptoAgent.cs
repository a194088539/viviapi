using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CryptoKitLib
{
    [TypeLibType((short)4288)]
    [Guid("A740A699-FA0B-4865-883E-089461894519")]
    [ComImport]
    public interface ICryptoAgent
    {
        [DispId(1)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.BStr)]
        string SelectSignCertificatebyPFX([MarshalAs(UnmanagedType.BStr), In] string bstrPFXFileName, [MarshalAs(UnmanagedType.BStr), In] string bstrPFXPassword);

        [DispId(2)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.BStr)]
        string SignMessagePKCS1([MarshalAs(UnmanagedType.BStr), In] string bstrSourceMsg);

        [DispId(3)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.BStr)]
        string SelectVerifyCertbyCer([MarshalAs(UnmanagedType.BStr), In] string bstrCerFileName);

        [DispId(4)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        bool VerifyMessageSignaturePKCS1([MarshalAs(UnmanagedType.BStr), In] string bstrPKCS1Signature, [MarshalAs(UnmanagedType.BStr), In] string bstrContent);

        [DispId(5)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetLastErrorDesc();
    }
}

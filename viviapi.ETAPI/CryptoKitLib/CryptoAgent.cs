using System.Runtime.InteropServices;

namespace CryptoKitLib
{
    [CoClass(typeof(CryptoAgentClass))]
    [Guid("A740A699-FA0B-4865-883E-089461894519")]
    [ComImport]
    public interface CryptoAgent : ICryptoAgent
    {
    }
}

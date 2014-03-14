using System;
using System.Configuration;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace GmailNotifierReplacement
{
    // Code taken from http://weblogs.asp.net/jgalloway/archive/2008/04/13/encrypting-passwords-in-a-net-app-config-file.aspx
    public static class PasswordEncryption
    {
        private static readonly byte[] entropy = Encoding.Unicode.GetBytes(ConfigurationManager.AppSettings["PasswordEncryptionEntropy"]);

        public static string EncryptString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var encryptedData = ProtectedData.Protect(
                Encoding.Unicode.GetBytes(input),
                entropy,
                DataProtectionScope.CurrentUser
            );

            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            if (string.IsNullOrEmpty(encryptedData))
                return ToSecureString(string.Empty);

            var decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedData),
                entropy,
                DataProtectionScope.CurrentUser
            );

            return ToSecureString(Encoding.Unicode.GetString(decryptedData));
        }

        public static SecureString ToSecureString(string input)
        {
            var secure = new SecureString();

            foreach (char c in input)
                secure.AppendChar(c);

            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            var returnValue = string.Empty;
            var ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);

            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }

            return returnValue;
        }
    }
}

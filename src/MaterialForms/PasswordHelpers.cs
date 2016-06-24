using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MaterialForms
{
    public static class PasswordHelpers
    {
        public static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                throw new ArgumentNullException(nameof(securePassword));
            }

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        //public static SecureString ConvertToSecureString(string password)
        //{
        //    if (password == null)
        //        throw new ArgumentNullException(nameof(password));

        //    unsafe
        //    {
        //        fixed (char* passwordChars = password)
        //        {
        //            var securePassword = new SecureString(passwordChars, password.Length);
        //            securePassword.MakeReadOnly();
        //            return securePassword;
        //        }
        //    }
        //}
    }
}

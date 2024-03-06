using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    internal class EncryptionUtility
    {
        private static readonly string _AllChars = " =ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly string _AltChars = " =xvXOq6HmGLCcRfK8YUw2bZ1rjMzg0Af3nBv7lDy4iQJ9dNp5kPStaWTuFhsoVeI";
        public static StringBuilder Encrypt(string Password)
        {
            var ReturnedSb = new StringBuilder();
            for(int i =0; i < Password.Length;i++)
            {
                for(int j = 0; j < _AllChars.Length; j++)
                {
                    if (Password[i]== _AllChars[j])
                    {
                        ReturnedSb.Append(_AltChars[j]);
                        break;
                    }
                }
            }
            return ReturnedSb;
        }

        public static StringBuilder Decrypt(string filetext)
        {
            StringBuilder sb = new StringBuilder(filetext);
            for(int i = 0; i<sb.Length;i++)
            {
                for (int j = 0; j < _AltChars.Length; j++)
                {
                    if (sb[i] == _AltChars[j])
                    {
                        sb[i] = _AllChars[j];
                        break;
                    }
                }
            }
            return sb;
        }
    }

}

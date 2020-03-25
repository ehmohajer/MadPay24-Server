using System;
using System.Collections.Generic;
using System.Text;

namespace MadPay24.Common.Helper
{
    public class Utilities
    {
        public static void CreatePaswordHash(string password,out byte[] passwordhash,out byte[] passwordsalt)
        {
            using (var hamc = new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = hamc.Key;
                passwordhash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password,  byte[] passwordhash,  byte[] passwordsalt)
        {
            using (var hamc = new System.Security.Cryptography.HMACSHA512(passwordsalt))
            {
                var computedHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordhash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

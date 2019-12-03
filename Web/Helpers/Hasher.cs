using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Web.Helpers
{
    public static class Hasher
    {
        private static MD5 _md5;

        private static MD5 MD5Instance
        {
            get
            {
                if (_md5 == null)
                {
                    _md5 = MD5.Create();
                }

                return _md5;
            }
        }

        public static string HashMD5(string input)
        {
            byte[] data = MD5Instance.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}

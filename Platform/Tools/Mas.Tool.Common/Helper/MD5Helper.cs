using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mas.Tool.Common.Helper
{
    public class MD5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5(string input)
        {
            input = input.Trim();
            using (var provider = new MD5CryptoServiceProvider())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var res = provider.ComputeHash(bytes, 0, bytes.Length);
                return res.Aggregate("", (current, t) => current + t.ToString("x2"));
            }
        }
    }
}

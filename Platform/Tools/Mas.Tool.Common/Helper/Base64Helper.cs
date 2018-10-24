using System;
using System.Text;

namespace Mas.Tool.Common.Helper
{
    public class Base64Helper
    {
        /// <summary>
        ///     Base64加密
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Code(string Message)
        {
            var bytes = Encoding.Default.GetBytes(Message);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        ///     Base64解密
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Decode(string Message)
        {
            var bytes = Convert.FromBase64String(Message);
            return Encoding.Default.GetString(bytes);
        }
    }
}
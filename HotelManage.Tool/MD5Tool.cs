using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HotelManage.Tool
{
    /// <summary>
    /// MD5 的工具类
    /// </summary>
    public class MD5Tool
    {
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="txt">要加密的字符</param>
        /// <returns>加密后的字符</returns>
        public static string MD5Encryption(string txt)  
        {         
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(txt));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
        
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
    }
}

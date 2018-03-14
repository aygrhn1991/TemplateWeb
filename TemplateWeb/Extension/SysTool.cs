using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TemplateWeb.Extension
{
    public class SysTool
    {
        public static string MD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] byteValue = Encoding.Unicode.GetBytes(str);
            byte[] hashValue = md5.ComputeHash(byteValue);
            string result = null;
            for (int i = 0; i < hashValue.Length; i++)
            {
                result += hashValue[i].ToString("X");
            }
            return result;
        }
    }
}
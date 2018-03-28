using qcloudsms_csharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateWeb.Component
{
    public class VerificationCodeTool
    {
        public static bool SendCode(string phone)
        {
            SmsSingleSender ssender = new SmsSingleSender(1400078324, "b6bcf068fb3ef4b611833bffb0181aaa");
            var result = ssender.sendWithParam("86", phone, 100278, new[] { "5678","10" }, null, "", "");  // 签名参数未提供或者为空时，会使用默认签名发送短信
            Console.WriteLine(result);
            return true;
        }
    }
}
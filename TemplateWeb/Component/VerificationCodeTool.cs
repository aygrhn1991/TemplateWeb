using qcloudsms_csharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateWeb.Models.DB;

namespace TemplateWeb.Component
{
    public class VerificationCodeTool
    {
        public VerificationCodeTool(string phone, int templateId, string[] paramters)
        {
            this.phone = phone;
            this.templateId = templateId;
            this.paramters = paramters;
            EntityDB entity = new EntityDB();
            var lay_setting = entity.lay_setting;
            this.appId = int.Parse(lay_setting.FirstOrDefault(p => p.key == "qcloudesms_appid").value);
            this.appKey = lay_setting.FirstOrDefault(p => p.key == "qcloudesms_appkey").value;
        }

        private int appId { get; set; }
        private string appKey { get; set; }
        private string phone { get; set; }
        private int templateId { get; set; }
        private string[] paramters { get; set; }

        public bool SendCode()
        {
            SmsSingleSender ssender = new SmsSingleSender(this.appId, this.appKey);
            var result = ssender.sendWithParam("86", this.phone, this.templateId, this.paramters, null, "", "");  // 签名参数未提供或者为空时，会使用默认签名发送短信
            Console.WriteLine(result);
            return result.result == 0;
        }
        private string CreateCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 10000);
            return code.ToString();
        }
    }
}
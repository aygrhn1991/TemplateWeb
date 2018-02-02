using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace TemplateWeb.Plugin.zyuploader.handler
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpFileCollection files = context.Request.Files;
            if (files.Count <= 0)
            {
                return;
            }
            HttpPostedFile file = files[0];
            string pathPrefix = "/Upload/zyuploader/banner/";
            string relativePath = String.Format(pathPrefix + "{0}-{1}-{2}/", DateTime.Now.Year, DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"));
            string AabsolutePath = context.Server.MapPath(relativePath);
            string filename = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            string imgUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + relativePath + filename;
            if (!Directory.Exists(Path.GetDirectoryName(AabsolutePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(AabsolutePath));
            }
            file.SaveAs(AabsolutePath + filename);
            context.Response.Write(imgUrl);
            return;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
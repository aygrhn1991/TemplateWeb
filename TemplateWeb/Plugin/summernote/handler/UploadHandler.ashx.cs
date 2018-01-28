using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TemplateWeb.Plugin.summernote.handler
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpFileCollection files = context.Request.Files;
            if (files.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                string path = context.Server.MapPath("~/Upload/editor");
                string name = file.FileName;
                string filename = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                file.SaveAs(path + "\\" + filename);
                string imgurl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/Attachments/" + filename;
                
            }
            context.Response.Write("okle");
            return;
            //HttpPostedFile file = files[0];
            //if (file == null)
            //{
            //    context.Response.Write("error|file is null");
            //    return;
            //}
            //else
            //{
            //    string path = context.Server.MapPath("~/Attachments");
            //    string name = file.FileName;
            //    string filename = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            //    file.SaveAs(path + "\\" + filename);
            //    string imgurl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/Attachments/" + filename;
            //    context.Response.Write(imgurl);
            //    return;
            //}
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
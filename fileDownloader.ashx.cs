using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;

namespace GCBC_NextGen.View.PP
{
    /// <summary>
    /// Summary description for fileDownloader
    /// </summary>
    public class fileDownloader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string filePath = context.Request.QueryString["filePath"];
                string fileName = context.Request.QueryString["fileName"];
                string url = "https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/" + filePath;
                WebClient client = new WebClient();
                client.UseDefaultCredentials = true;
                byte[] bytes = client.DownloadData(url);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.Clear();
                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                context.Response.ContentType = "application/octet-stream";
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

                context.Response.BinaryWrite(bytes);
                context.Response.Flush();
                context.Response.End();
            }
            catch (Exception ex)
            {
            }
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
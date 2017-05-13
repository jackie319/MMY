using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMY.FrontSite.WebUI
{
    /// <summary>
    /// author : ReversalMinutes
    /// </summary>
    public static class RMUtility
    {
        public static ActionResult HtmlContent(this Controller c, string Path)
        {
            var html = "";
            try
            {
                html = PathFixed(System.IO.File.ReadAllText(c.Server.MapPath(Path)));
            }
            catch (Exception ex)
            {

                html = ex.Message;
            }

            var content = new ContentResult()
            {
                Content = html,
                ContentType = "text/html"
            };
            return content;
        }

        public static string PathFixed(string html)
        {
            return html.Replace("href=\"css/", "href=\"/html/css/");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Blogcatch.Areas.Admin.Models
{
    public static class ImageManager
    {
        /// <summary>
        /// Set the display picture image url
        /// </summary>
        public static string GetImageFilePath(HttpPostedFileBase Image,string filePath = "~/fileman/Uploads/Images")
        {
            if (Image != null && Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(HttpContext.Current.Server.MapPath(filePath), fileName);
                Image.SaveAs(path);
                return ("/fileman/Uploads/Images/" + fileName);
            }
            return null;
        }
    }
}
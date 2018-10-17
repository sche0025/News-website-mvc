using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Collections;
using System;

namespace DLEVEL.Controllers
{
    public class FileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            var path = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if(Path.GetExtension(file.FileName).ToLower()==".jpg" ||
                        Path.GetExtension(file.FileName).ToLower() == ".png" ||
                        Path.GetExtension(file.FileName).ToLower() == ".gif" ||
                        Path.GetExtension(file.FileName).ToLower() == ".jpeg"
                        )
                    {
                        path = Path.Combine(Server.MapPath("~/Photos"),file.FileName);
                        file.SaveAs(path);
                        ViewBag.UploadSuccess = true;
                    }
                }
            }
           
            return View();
        }

    }
}
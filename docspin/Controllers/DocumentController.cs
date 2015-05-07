using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using docspin.Models;

namespace docspin.Controllers
{
    public class DocumentController : Controller
    {
      


        // GET: /Document/
        public ActionResult Index(int? id)
        {
            ViewBag.RepoId = id;
            return View();
        }

       
    }
}

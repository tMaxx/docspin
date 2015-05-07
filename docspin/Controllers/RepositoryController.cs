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
    public class RepositoryController : Controller
    {
        private DataModelContainer db = new DataModelContainer();

        // GET: /Repository/
        public ActionResult Index()
        {
            return View(db.RepositorySet.ToList());
        }

       
    }
}

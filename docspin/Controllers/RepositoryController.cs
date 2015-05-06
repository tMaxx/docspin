using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using docspin.Filters;
using docspin.Models;

namespace docspin.Controllers
{
    public class RepositoryController : Controller
    {
        DataModelContainer db = new DataModelContainer();

        //
        // GET: /Repository/
        public ActionResult Index()
        {
            return View(db.RepositorySet.ToList());
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

	}
}
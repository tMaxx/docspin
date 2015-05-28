using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocSpin2.Models;
using DocSpin2.Util;
using System.IO;
using System.Collections.Specialized;

namespace DocSpin2.Controllers
{
	[Authorize]
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index()
        {
			if (ApplicationUser.CurrentUserRole == UserRole.Admin)
	            return View(db.DocumentSet.ToList());
			return View("Index", "Home");
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.DocumentSet.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
			
			if (!ObjectAuth.DocumentAction(id, AccessControlSetting.Read))
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			
			return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
			if (!ObjectAuth.DocumentAction(id, AccessControlSetting.Write))
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

			ViewData["RepositoryList"] =
				new SelectList(Repository.GetRepositoriesList(), "Id", "Name");
			
			return View();
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,ACS,RepositoryId")] Document document)
        {
			if (!ObjectAuth.DocumentAction(id, AccessControlSetting.Write))
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

			ViewData["RepositoryList"] =
				new SelectList(Repository.GetRepositoriesList(), "Id", "Name");

			if (ModelState.IsValid)
            {
                db.DocumentSet.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.DocumentSet.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }

			if (!ObjectAuth.DocumentAction(id, AccessControlSetting.Read))
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            return View(document);
        }

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ACS")] Document document)
        {
			if (!ObjectAuth.DocumentAction(id, AccessControlSetting.Write))
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
		{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.DocumentSet.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }

			if (!ObjectAuth.DocumentAction(id, AccessControlSetting.Write))
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            
			return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			if (!ObjectAuth.DocumentAction(id, AccessControlSetting.Write))
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            Document document = db.DocumentSet.Find(id);
			if (ApplicationUser.CurrentUserRole != UserRole.Admin)
			{
				document.IsRemoved = true;
				db.Entry(document).State = EntityState.Modified;
			}
			else
				db.DocumentSet.Remove(document);
	        db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(Upload upload)
        {
            foreach (var file in upload.Files)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Util.FileManager.FileFolder, fileName);
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("FileUpload");
        }
    }
}

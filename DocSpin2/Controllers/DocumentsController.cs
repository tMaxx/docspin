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
			return RedirectToAction("Index", "Repositories");
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.DocumentSet.Where(r => r.Id == id)
				.Include(d => d.Comments.Select(c => c.Author))
				.Include(w => w.Versions).FirstOrDefault();
            
            if (document == null)
            {
                return HttpNotFound();
            }

			var e = ObjectAuth.DocumentAction(id, AccessControlSetting.Write);
			if (e != null)
				return View("AuthError", e);
			
			return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
			ViewData["RepositoryList"] =
				new SelectList(Repository.GetRepositoriesList(), "Id", "Name");
			
			return View();
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,ACS,RepositoryId")] Document document, Upload upload)
        {
			var e = ObjectAuth.DocumentAction(int.Parse(this.Request["RepositoryId"]), AccessControlSetting.Write);
			if (e != null)
				return View("AuthError", e);

			if (ModelState.IsValid)
            {
                db.DocumentSet.Add(document);
                               
                foreach (var file in upload.Files)
                {
                    var fileName = "";
                    var path = "";
                    if (file.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(file.FileName);
                        path = Path.Combine(Util.FileManager.FileFolder, fileName);
                        file.SaveAs(path);
                    }
                    DocumentVersion docv = new DocumentVersion()
                    {
                        FileTimestamp = DateTime.Now,
                        Filename = fileName,
                        OriginalFilename = fileName,
                        UploadTimestamp = DateTime.Now,
                        IsRemoved = false,
                        Hash = path,
                        DocumentId = document.Id,
                        AuthorId = ApplicationUser.CurrentUserId
                    };
                    db.DocumentVersionSet.Add(docv);
                }

                db.SaveChanges();

				return RedirectToAction("Details", "Repositories", new { id = document.RepositoryId });
            }

			ViewData["RepositoryList"] =
				new SelectList(Repository.GetRepositoriesList(), "Id", "Name");

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

			var e = ObjectAuth.DocumentAction(id, AccessControlSetting.Read);
			if (e != null)
				return View("AuthError", e);

            return View(document);
        }

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ACS")] Document document, Upload upload)
        {
			if (ModelState.IsValid)
            {
				Document find = db.DocumentSet.Find(document.Id);
				var e = ObjectAuth.DocumentAction(find.Id, AccessControlSetting.Write);
				if (e != null)
					return View("AuthError", e);

				find.Name = document.Name;
				find.Description = document.Description;
				find.ACS = document.ACS;

                db.Entry(find).State = EntityState.Modified;

                foreach (var file in upload.Files)
                {
                    var fileName = "";
                    var path = "";
                    if (file.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(file.FileName);
                        path = Path.Combine(Util.FileManager.FileFolder, fileName);
                        file.SaveAs(path);
                    }
                 
                    DocumentVersion docv = new DocumentVersion()
                    {
                        FileTimestamp = DateTime.Now,
                        Filename = fileName,
                        OriginalFilename = fileName,
                        UploadTimestamp = DateTime.Now,
                        IsRemoved = false,
                        Hash = path,
                        DocumentId = document.Id,
                        AuthorId = ApplicationUser.CurrentUserId
                    };
                    db.DocumentVersionSet.Add(docv);
                }

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

			var e = ObjectAuth.DocumentAction(id, AccessControlSetting.Write);
			if (e != null)
				return View("AuthError", e);
            
			return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			var e = ObjectAuth.DocumentAction(id, AccessControlSetting.Write);
			if (e != null)
				return View("AuthError", e);

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

        

        public ActionResult FileDownload(int? id)
        {
            DocumentVersion docv = db.DocumentVersionSet.Find(id);

            byte[] fileBytes = System.IO.File.ReadAllBytes(@docv.Hash.ToString());
            string fileName = docv.Filename;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }



        public ActionResult Chat(string msg, string docId)
        {
            
            Comment comment = new Comment()
            {
                Content = msg,
                Timestamp = DateTime.Now,
                IsRemoved = false,
                DocumentId = int.Parse(docId),
                AuthorId = ApplicationUser.CurrentUserId
            };
            db.CommentSet.Add(comment);
            db.SaveChanges();
            ViewBag.UserName = ApplicationUser.CurrentUser.FullName;
            return PartialView("Comment", comment);
        }


    }
}

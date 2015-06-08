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

namespace DocSpin2.Controllers
{
	[Authorize]
    public class RepositoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Repositories
        public ActionResult Index()
        {
            return View(Repository.GetRepositoriesList());
        }

        // GET: Repositories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db.RepositorySet.Where(r => r.Id == id).Include(d => d.Documents).FirstOrDefault();
            if (repository == null)
            {
                return HttpNotFound();
            }

			var e = ObjectAuth.RepositoryAction(id, AccessControlSetting.Read);
			if (e != null)
				return View("AuthError", e);

            return View(repository);
        }

        // GET: Repositories/Create
        public ActionResult Create()
        {
			if (ApplicationUser.CurrentUserRole != UserRole.Admin)
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);			
			return View();
        }

        // POST: Repositories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ACS")] Repository repository)
        {
			if (ApplicationUser.CurrentUserRole != UserRole.Admin)
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);				

			if (ModelState.IsValid)
            {
                db.RepositorySet.Add(repository);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repository);
        }

        // GET: Repositories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db.RepositorySet.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }

			if (!ObjectAuth.IsRepositorySupervisor(id))
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

			return View(repository);
        }

        // POST: Repositories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ACS")] Repository repository)
        {
			if (!ObjectAuth.IsRepositorySupervisor(int.Parse(this.Request["Id"])))
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			
			if (ModelState.IsValid)
            {
                db.Entry(repository).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(repository);
        }

        // GET: Repositories/Delete/5
        public ActionResult Delete(int? id)
        {
			if (ApplicationUser.CurrentUserRole != UserRole.Admin)
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db.RepositorySet.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            return View(repository);
        }

        // POST: Repositories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			if (ApplicationUser.CurrentUserRole != UserRole.Admin)
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			
			Repository repository = db.RepositorySet.Find(id);
            db.RepositorySet.Remove(repository);
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
    }
}

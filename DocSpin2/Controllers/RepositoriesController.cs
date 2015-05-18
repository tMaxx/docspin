using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocSpin2.Models;

namespace DocSpin2.Controllers
{
    public class RepositoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Repositories
        public ActionResult Index()
        {
            return View(db.RepositorySet.ToList());
        }

        // GET: Repositories/Details/5
        public ActionResult Details(int? id)
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
            return View(repository);
        }

        // GET: Repositories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repositories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ACS")] Repository repository)
        {
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
            return View(repository);
        }

        // POST: Repositories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ACS")] Repository repository)
        {
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

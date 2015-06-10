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
    public class ACLsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ACLs
        public ActionResult Index()
        {
            return View();
        }

		// ACList for specific repo/doc
        // GET: ACLs/Details/5?type=Repository|Document
        public ActionResult Details(int? id, string type)
        {
			if (id == null || !(type == "Repository" || type == "Document"))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (type == "Repository")
			{
				var ret = CheckRepositorySupervisor((int)id);
				if (ret != null)
					return ret;

				var repo = db.RepositorySet.FirstOrDefault(r => r.Id == id);

				var acl = from a in db.RepositoryACLSet.Include(r => r.User)
						  where a.RepositoryId == id
						  select new ACLItemViewModel
						  {
							  model_id = a.Id,
							  user_id = a.UserId,
							  user_name = a.User.FullName,
							  acs = a.ACS
						  };

				return View(new ACLViewModel
					{
						src_type = ACLViewModel.Type.Repository,
						src_type_id = (int)id,
						src_type_name = repo.Name,
						elements = acl.ToList()
					});
			}	
			else
			{
				var ret = CheckDocumentSupervisor((int)id);
				if (ret != null)
					return ret;

				var doc = db.DocumentSet.Where(d => d.Id == id)
					.Include(d => d.Repository).FirstOrDefault();
				
				var acl = from a in db.DocumentACLSet.Include(r => r.User)
						  where a.DocumentId == id
						  select new ACLItemViewModel
						  {
							  model_id = a.Id,
							  user_id = a.UserId,
							  user_name = a.User.FullName,
							  acs = a.ACS
						  };

				return View(new ACLViewModel
					{
						src_type = ACLViewModel.Type.Document,
						src_type_id = (int)id,
						src_type_name = doc.Repository.Name,
						elements = acl.ToList()
					});
			}
        }

		// GET: ACLs/Create/5?type=Repository|Document
        public ActionResult Create(int? id, string type)
        {
			if (id == null || !(type == "Repository" || type == "Document"))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var ret = CheckSupervisor(type, (int)id);
			if (ret != null)
				return ret;

			ViewData["UsersList"] = 
				new SelectList(ApplicationUser.GetList(), "Id", "FullName");

			return View(new ACLCreateModel
			{
				object_id = id.Value,
				object_type = type,
				acs = AccessControlSetting.None
			});
        }

        // POST: ACLs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "object_id,object_type,user_id,acs")] ACLCreateModel acl)
        {
			if (!(acl.object_type == "Repository" || acl.object_type == "Document"))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var ret = CheckSupervisor(acl.object_type, acl.object_id);
			if (ret != null)
				return ret;

			if (ModelState.IsValid)
            {
				if (acl.object_type == "Repository")
					db.RepositoryACLSet.Add(new RepositoryACL 
					{
						RepositoryId = acl.object_id,
 						ACS = acl.acs,
						UserId = acl.user_id
					});
				else
					db.DocumentACLSet.Add(new DocumentACL
					{
						DocumentId = acl.object_id,
						ACS = acl.acs,
						UserId = acl.user_id
					}); 
				db.SaveChanges();
				return RedirectToAction("Index", new { id = acl.object_id, type = acl.object_type });
            }

			ViewData["UsersList"] =
				new SelectList(ApplicationUser.GetList(), "Id", "FullName");
			
			return View(acl);
        }

        // GET: ACLs/Delete/5
        public ActionResult Delete(int? id, string type)
        {
			if (id == null || !(type == "Repository" || type == "Document"))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var ret = CheckSupervisor(type, (int)id);
			if (ret != null)
				return ret;

			ACLDeleteModel acl;
			if (type == "Document")
			{
				var fetch = db.DocumentACLSet.Where(d => d.Id == id)
					.Include(d => d.Document)
					.Include(d => d.Document.Repository)
					.Include(d => d.User).FirstOrDefault();
				acl = new ACLDeleteModel
				{
					object_id = fetch.Id,
					object_name = fetch.Document.Name,
					object_type = "Document",
					repo_name = fetch.Document.Repository.Name,
					acs = fetch.ACS,
					user_name = fetch.User.FullName
				};
			} 
			else
			{
				var fetch = db.RepositoryACLSet.Where(r => r.Id == id)
					.Include(r => r.Repository)
					.Include(r => r.User).FirstOrDefault();
				acl = new ACLDeleteModel
				{
					object_id = fetch.Id,
					object_name = fetch.Repository.Name,
					object_type = "Repository",
					acs = fetch.ACS,
					user_name = fetch.User.FullName
				};
			}
			
            return View(acl);
        }

        // POST: ACLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed([Bind(Include = "object_id,object_type")] ACLDeleteModel acl)
        {
			if (acl.object_id == null || !(acl.object_type == "Repository" || acl.object_type == "Document"))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var ret = CheckSupervisor(acl.object_type, acl.object_id);
			if (ret != null)
				return ret;

			if (acl.object_type == "Repository")
			{
				var fetch = db.RepositoryACLSet.FirstOrDefault(r => r.Id == acl.object_id);
				db.RepositoryACLSet.Remove(fetch);
			}
			else
			{
				var fetch = db.DocumentACLSet.FirstOrDefault(d => d.Id == acl.object_id);
				db.DocumentACLSet.Remove(fetch);
			}

			db.SaveChanges();
			return RedirectToAction("Index", new { id = acl.object_id, type = acl.object_type });
		}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
		}

		#region helpers

		private ActionResult CheckSupervisor(string type, int id)
		{
			if (type == "Repository")
			{
				var ret = CheckRepositorySupervisor(id);
				if (ret != null)
					return ret;
			}
			else
			{
				var ret = CheckDocumentSupervisor(id);
				if (ret != null)
					return ret;
			}
			return null;
		}

		private ActionResult CheckRepositorySupervisor(int id)
		{
			var repo = db.RepositorySet.FirstOrDefault(r => r.Id == id);
			if (repo == null)
				return HttpNotFound();
			if (!repo.IsSupervisor)
				return View("AuthError", new ObjectAuthError(ObjectAuthErrorReason.RepositoryActionDenied,
					"You must be a supervisor to add ACL on repo id=" + id));
			return null;
		}

		private ActionResult CheckDocumentSupervisor(int id)
		{
			var doc = db.DocumentSet.Where(d => d.Id == id)
				.Include(d => d.Repository).FirstOrDefault();
			if (doc == null)
				return HttpNotFound();
			//TODO: maybe extend to doc's acs Supervisor prop
			if (!doc.Repository.IsSupervisor)
				return View("AuthError", new ObjectAuthError(ObjectAuthErrorReason.DocumentActionDenied,
					"You must be a repository supervisor to add ACL on document id=" + id + ", repo id=" + doc.RepositoryId));
			return null;
		}

		#endregion
	}
}

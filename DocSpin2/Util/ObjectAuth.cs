using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocSpin2.Models;

namespace DocSpin2.Util
{
	//Class for checking object action access
	//(whether required action is permitted)
	public class ObjectAuth
	{
		public static bool RepositoryAction(int id, AccessControlSetting action)
		{
			using (var db = ApplicationDbContext.Create())
			{
				Repository repo = db.RepositorySet.First(r => r.Id == id);
				if (repo == null)
					return false;

				if (ApplicationUser.CurrentUserRole == UserRole.Admin)
					return true;

				string uid = ApplicationUser.CurrentUserId;
				if (db.SupervisorSet.Where(s => s.RepositoryId == id && s.UserId == uid).Count() > 0)
					return true;

				RepositoryACL fetch = db.RepositoryACLSet
					.First(s => s.RepositoryId == id && s.UserId == uid);
				if (fetch != null)
					return AccessControlSettingHelper.Compare(fetch.ACS, action);

				return AccessControlSettingHelper.Compare(repo.ACS, action);
			}
			return false;
		}

		public static bool DocumentAction(int id, AccessControlSetting action)
		{
			using (var db = ApplicationDbContext.Create())
			{
				Document doc = db.DocumentSet.First(d => d.Id == id);
				if (doc == null)
					return false;

				if (ApplicationUser.CurrentUserRole == UserRole.Admin)
					return true;

				DocumentACL fetch = db.DocumentACLSet
					.First(a => a.DocumentId == id && a.UserId == ApplicationUser.CurrentUserId);
				if (fetch != null)
					return AccessControlSettingHelper.Compare(fetch.ACS, action);

				if (!RepositoryAction(doc.RepositoryId, action))
					return false;

				return AccessControlSettingHelper.Compare(doc.ACS, action);
			}
			return false;
		}
	}
}

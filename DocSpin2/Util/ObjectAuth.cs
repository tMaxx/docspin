using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocSpin2.Models;

namespace DocSpin2.Util
{
	public enum ObjectAuthErrorReason
	{
		RepositoryNotFound,
		DocumentNotFound,
		RepositoryActionDenied,
		DocumentActionDenied
	}

	public struct ObjectAuthError
	{
		public readonly ObjectAuthErrorReason err;
		public readonly string details;

		public ObjectAuthError(ObjectAuthErrorReason e, string d)
		{
			err = e; details = d;
		}
	}

	//Class for checking object action access
	//(whether required action is permitted)
	public class ObjectAuth
	{
		public static ObjectAuthError? RepositoryAction(int? id, AccessControlSetting action)
		{
			if (id == null)
				return new ObjectAuthError(
					ObjectAuthErrorReason.RepositoryNotFound,
					"Wrong repository id");

			using (var db = ApplicationDbContext.Create())
			{
				Repository repo = db.RepositorySet.FirstOrDefault(r => r.Id == id);
				if (repo == null)
					return new ObjectAuthError(
						ObjectAuthErrorReason.RepositoryNotFound,
						"No repository exists with id=" + id);

				if (ApplicationUser.CurrentUserRole == UserRole.Admin)
					return null;

				string uid = ApplicationUser.CurrentUserId;
				if (db.SupervisorSet.Where(s => s.RepositoryId == id && s.UserId == uid).Count() > 0)
					return null;

				{
					RepositoryACL fetch = db.RepositoryACLSet
						.FirstOrDefault(s => s.RepositoryId == id && s.UserId == uid);
					if (fetch != null && !AccessControlSettingHelper.Compare(fetch.ACS, action))
						return new ObjectAuthError(ObjectAuthErrorReason.RepositoryActionDenied,
							"Action '" + AccessControlSettingHelper.Describe(action)
							+ "' is not permitted by ACL's '"
							+ AccessControlSettingHelper.Describe(fetch.ACS)
							+ "' setting on repository id=" + id);
				}

				if (!AccessControlSettingHelper.Compare(repo.ACS, action))
					return new ObjectAuthError(ObjectAuthErrorReason.RepositoryActionDenied,
						"Action '" + AccessControlSettingHelper.Describe(action)
						+ "' is not permitted by repository's '"
						+ AccessControlSettingHelper.Describe(repo.ACS)
						+ "' setting on repository id=" + id);
			}
			return null;
		}

		public static ObjectAuthError? DocumentAction(int? id, AccessControlSetting action)
		{
			if (id == null) 
				return new ObjectAuthError(
					ObjectAuthErrorReason.DocumentNotFound,
					"Wrong document id");
			using (var db = ApplicationDbContext.Create())
			{
				Document doc = db.DocumentSet.FirstOrDefault(d => d.Id == id);
				if (doc == null)
					return new ObjectAuthError(
						ObjectAuthErrorReason.DocumentNotFound,
						"No document with id=" + id);
				if (ApplicationUser.CurrentUserRole == UserRole.Admin)
					return null;

				{
					DocumentACL fetch = db.DocumentACLSet
						.FirstOrDefault(a => a.DocumentId == id && a.UserId == ApplicationUser.CurrentUserId);
					if (fetch != null && !AccessControlSettingHelper.Compare(fetch.ACS, action))
						return new ObjectAuthError(ObjectAuthErrorReason.DocumentActionDenied,
							"Action '" + AccessControlSettingHelper.Describe(action)
							+ "' is not permitted by ACL's '"
							+ AccessControlSettingHelper.Describe(fetch.ACS)
							+ "' setting on document id=" + id);
				}
				{
					ObjectAuthError? err = RepositoryAction(doc.RepositoryId, action);
					if (err != null)
						return err;
				}

				if (!AccessControlSettingHelper.Compare(doc.ACS, action))
					return new ObjectAuthError(ObjectAuthErrorReason.RepositoryActionDenied,
						"Action '" + AccessControlSettingHelper.Describe(action)
						+ "' is not permitted by document's '"
						+ AccessControlSettingHelper.Describe(doc.ACS)
						+ "' setting on document id=" + id);
			}
			return null;
		}

		//Warning: doesn't check if repo exists
		public static bool IsRepositorySupervisor(int? id)
		{
			if (id == null)
				return false;

			if (ApplicationUser.CurrentUserRole == UserRole.Admin)
				return true;
			
			using (var db = ApplicationDbContext.Create())
			{
				return db.SupervisorSet
					.Where(s => s.Id == id && s.UserId == ApplicationUser.CurrentUserId)
					.Count() > 0;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;


namespace DocSpin2.Models
{
    public partial class Repository
    {
        public Repository()
        {
			this.ACS = AccessControlSettingHelper.Default;
			this.Supervisor = new HashSet<Supervisor>();
            this.Documents = new HashSet<Document>();
            this.ACL = new HashSet<RepositoryACL>();
        }

		[Key]
        public int Id { get; set; }
		[Required]
        public string Name { get; set; }
		public string Description { get; set; }
		[Required]
        public AccessControlSetting ACS { get; set; }
    
        public virtual ICollection<Supervisor> Supervisor { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<RepositoryACL> ACL { get; set; }

		public bool IsSupervisor
		{
			get
			{
				if (ApplicationUser.CurrentUserRole == UserRole.Admin)
					return true;
				using (var ctx = ApplicationDbContext.Create())
				{
					var ret = from s in ctx.SupervisorSet
							  where s.User.Id == ApplicationUser.CurrentUserId
									&& s.RepositoryId == this.Id
							  select s;
					return ret.Count() > 0;
				}
			}
		}

		public bool IsReadOnly
		{
			get
			{
				using (var ctx = ApplicationDbContext.Create())
				{
					var ret = from a in ctx.RepositoryACLSet
							  where a.UserId == ApplicationUser.CurrentUserId
									&& a.RepositoryId == this.Id
							  select a;
					var obj = ret.FirstOrDefault();
					AccessControlSetting acs;
					if (obj != null)
					{
						acs = obj.ACS & AccessControlSetting.Write;
						if (acs == AccessControlSetting.Read
							|| acs == AccessControlSetting.Comment)
							return true;
					}

					acs = this.ACS & AccessControlSetting.Write;
					if (acs == AccessControlSetting.Read
						|| acs == AccessControlSetting.Comment)
						return true;
				}
				return false;
			}
		}


		public static List<Repository> GetRepositoriesList(bool? isAdmin = null)
		{
			if (isAdmin == null)
				isAdmin = (ApplicationUser.CurrentUserRole == UserRole.Admin);

			using (var ctx = new ApplicationDbContext())
			{
				if (isAdmin == true)
				{
					var ret = from r in ctx.RepositorySet select r;
					return ret.Distinct().ToList();
				}
				else 
				{
					var ret = from r in ctx.RepositorySet
						  where ( //is a supervisor
									(from s in ctx.SupervisorSet
									 where (s.User.Id == ApplicationUser.CurrentUserId
										   && s.Repository.Id == r.Id)
									 select s.Id).Count() > 0
								)
								||
								( //has standard access
									!r.ACS.HasFlag(AccessControlSetting.SupervisorOnly)
									&& (from l in ctx.RepositoryACLSet
										where (l.User.Id == ApplicationUser.CurrentUserId
											  && l.Repository.Id == r.Id
											  && l.ACS != AccessControlSetting.None) //access not revoked
										select l.Id).Count() == 0
								)
						  select r;
					return ret.Distinct().ToList();
				}
			}
		}
	}
}

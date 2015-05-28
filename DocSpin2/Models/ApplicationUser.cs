using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin;


namespace DocSpin2.Models
{
	public partial class ApplicationUser : IdentityUser
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public ApplicationUser()
        {
            this.RepositorySupervisor = new HashSet<Supervisor>();
            this.DocumentVersion = new HashSet<DocumentVersion>();
            this.Comments = new HashSet<Comment>();
            this.RepositoryACL = new HashSet<RepositoryACL>();
            this.DocumentACL = new HashSet<DocumentACL>();
			this.Role = UserRole.User;
        }
    
		[Key]
        public override string Id { get; set; }
		//[Index]
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//public int UserId { get; set; }
		[Required]
		public string FullName { get; set; }
		[Required]
        public UserRole Role { get; set; }
    
        public virtual ICollection<Supervisor> RepositorySupervisor { get; set; }
        public virtual ICollection<DocumentVersion> DocumentVersion { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<RepositoryACL> RepositoryACL { get; set; }
        public virtual ICollection<DocumentACL> DocumentACL { get; set; }

		[NotMapped]
		private static bool _curUserObjInited = false;
		[NotMapped]
		private static ApplicationUser _curUserObj = null;
		[NotMapped]
		private static UserRole _curUserRole = UserRole.None;
		public static ApplicationUser CurrentUser
		{
			get
			{
				//slight abuse ahead
				if (!_curUserObjInited)
				{
					var usr = HttpContext.Current
						.GetOwinContext()
						.Get<ApplicationSignInManager>()
						.UserManager.FindById(
						HttpContext.Current.User.Identity.GetUserId()
						);
					_curUserObj = usr;
					if (_curUserObj != null)
						_curUserRole = usr.Role;
					_curUserObjInited = true;
				}
				return _curUserObj;
			}
		}

		public static UserRole CurrentUserRole
		{
			get
			{
				if (!_curUserObjInited && CurrentUser == null)
					return UserRole.None;
				return (UserRole)_curUserRole;
			}
		}

		public static string CurrentUserId
		{
			get
			{
				if ((!_curUserObjInited && CurrentUser == null)
					|| (_curUserObj == null))
					return "not logged in";
				return _curUserObj.Id;
			}
		}
    }
}

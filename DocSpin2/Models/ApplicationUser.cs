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
        public UserRole Role { get; set; }
    
        public virtual ICollection<Supervisor> RepositorySupervisor { get; set; }
        public virtual ICollection<DocumentVersion> DocumentVersion { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<RepositoryACL> RepositoryACL { get; set; }
        public virtual ICollection<DocumentACL> DocumentACL { get; set; }


		private static UserRole? _curUserRole = null;
		public static UserRole currentUserRole
		{
			get
			{
				if (_curUserRole == null)
				{
					var usr = HttpContext.Current
						.GetOwinContext()
						.Get<ApplicationSignInManager>()
						.UserManager.FindById(
						HttpContext.Current.User.Identity.GetUserId()
						);
					if (usr == null)
						_curUserRole = UserRole.None;
					else
						_curUserRole = usr.Role;
				}
				return (UserRole)_curUserRole;
			}
		}
    }
}

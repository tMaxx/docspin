using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;


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
    
        public override string Id { get; set; }
		[Required]
		public string FullName { get; set; }
        public UserRole Role { get; set; }
    
        public virtual ICollection<Supervisor> RepositorySupervisor { get; set; }
        public virtual ICollection<DocumentVersion> DocumentVersion { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<RepositoryACL> RepositoryACL { get; set; }
        public virtual ICollection<DocumentACL> DocumentACL { get; set; }
    }
}

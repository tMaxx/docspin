using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace DocSpin2.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
			: base("name=DefaultConnection")
        {
        }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

		public static void InitializeDBContext()
		{
			using (var context = Create())
			{
				context.Database.Initialize(false);
			}
		}

		public virtual IDbSet<Repository> RepositorySet { get; set; }
		public virtual IDbSet<Document> DocumentSet { get; set; }
		public virtual IDbSet<Supervisor> SupervisorSet { get; set; }
		public virtual IDbSet<DocumentVersion> DocumentVersionSet { get; set; }
		public virtual IDbSet<Comment> CommentSet { get; set; }
		public virtual IDbSet<RepositoryACL> RepositoryACLSet { get; set; }
		public virtual IDbSet<DocumentACL> DocumentACLSet { get; set; }

		public System.Data.Entity.DbSet<DocSpin2.Models.Repository> Repositories { get; set; }
		//public virtual DbSet<ApplicationUser> ApplicationUserSet { get; set; }
    }
}

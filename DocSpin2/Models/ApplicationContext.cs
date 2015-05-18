using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System;


namespace DocSpin2.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public virtual IDbSet<Repository> RepositorySet { get; set; }
		public virtual IDbSet<Document> DocumentSet { get; set; }
		public virtual IDbSet<Supervisor> SupervisorSet { get; set; }
		public virtual IDbSet<DocumentVersion> DocumentVersionSet { get; set; }
		public virtual IDbSet<Comment> CommentSet { get; set; }
		public virtual IDbSet<RepositoryACL> RepositoryACLSet { get; set; }
		public virtual IDbSet<DocumentACL> DocumentACLSet { get; set; }

		public ApplicationDbContext()
			: base("name=DefaultConnection")
        {
        }

		public override int SaveChanges()
		{
			foreach (var obj in this.ChangeTracker.Entries()
					.Where(x => x.State == EntityState.Modified || x.State == EntityState.Added))
			{
				if (obj.State == EntityState.Modified && obj.Entity is EntityEvents.IModified)
					((EntityEvents.IModified)obj.Entity).entityOnModified(this);
				else if (obj.State == EntityState.Added && obj.Entity is EntityEvents.IAdded)
					((EntityEvents.IAdded)obj.Entity).entityOnAdded(this);
			}
			return base.SaveChanges();
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

    }
}

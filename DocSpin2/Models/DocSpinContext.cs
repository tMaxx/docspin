using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace DocSpin2.Models
{
	public partial class DocSpinContext : ApplicationDbContext
    {
		public static void InitializeDBContext()
		{
			using (var context = new DocSpin2.Models.DocSpinContext())
			{
				context.Database.Initialize(false);
			}
		}


		public DocSpinContext() : base() { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
        
        public virtual DbSet<Repository> RepositorySet { get; set; }
        public virtual DbSet<Document> DocumentSet { get; set; }
        public virtual DbSet<Supervisor> SupervisorSet { get; set; }
        public virtual DbSet<DocumentVersion> DocumentVersionSet { get; set; }
        public virtual DbSet<Comment> CommentSet { get; set; }
        public virtual DbSet<RepositoryACL> RepositoryACLSet { get; set; }
        public virtual DbSet<DocumentACL> DocumentACLSet { get; set; }
		//public virtual DbSet<ApplicationUser> ApplicationUserSet { get; set; }
    }
}

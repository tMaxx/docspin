using System;
using System.Collections.Generic;


namespace DocSpin2.Models
{
    public partial class ApplicationUser
    {
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
		//public string UserName { get; set; }
        public string FullName { get; set; }
        public string Active { get; set; }
        public string Password { get; set; }
        public override string Email { get; set; }
        public UserRole Role { get; set; }
    
        public virtual ICollection<Supervisor> RepositorySupervisor { get; set; }
        public virtual ICollection<DocumentVersion> DocumentVersion { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<RepositoryACL> RepositoryACL { get; set; }
        public virtual ICollection<DocumentACL> DocumentACL { get; set; }
    }
}

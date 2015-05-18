using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DocSpin2.Models
{
    public partial class Document : EntityEvents.IAdded, EntityEvents.IModified
    {
        public Document()
        {
            this.IsRemoved = false;
			this.ACS = new AccessControlSetting();
			this.Versions = new HashSet<DocumentVersion>();
            this.Comments = new HashSet<Comment>();
            this.ACL = new HashSet<DocumentACL>();
        }
    
        public int Id { get; set; }
		[Required]
        public string Name { get; set; }
        public string Description { get; set; }
		[Required]
		public System.DateTime TsCreated { get; set; }
		[Required]
		public System.DateTime TsModified { get; set; }
		[Required]
        public AccessControlSetting ACS { get; set; }
		[Required]
		public bool IsRemoved { get; set; }
    
        public virtual ICollection<DocumentVersion> Versions { get; set; }
		[Required]
        public virtual Repository Repository { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<DocumentACL> ACL { get; set; }


		public void entityOnAdded(ApplicationDbContext ctx)
		{
			this.IsRemoved = false;
			this.TsCreated = DateTime.Now;
			this.TsModified = DateTime.Now;
		}

		public void entityOnModified(ApplicationDbContext ctx)
		{
			this.TsModified = DateTime.Now;
		}

	}
}

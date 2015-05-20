using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    

namespace DocSpin2.Models
{
    public partial class Repository
    {
        public Repository()
        {
			this.ACS = new AccessControlSetting();
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
    }
}

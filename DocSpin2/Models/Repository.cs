using System;
using System.Collections.Generic;
    

namespace DocSpin2.Models
{
    public partial class Repository
    {
        public Repository()
        {
            this.Supervisor = new HashSet<Supervisor>();
            this.Documents = new HashSet<Document>();
            this.ACL = new HashSet<RepositoryACL>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public AccessControlSetting ACS { get; set; }
    
        public virtual ICollection<Supervisor> Supervisor { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<RepositoryACL> ACL { get; set; }
    }
}

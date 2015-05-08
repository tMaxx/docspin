using System;
using System.Collections.Generic;
    

namespace DocSpin2.Models
{
    public partial class RepositoryACL
    {
        public int Id { get; set; }
        public AccessControlSetting ACS { get; set; }
    
        public virtual Repository Repository { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

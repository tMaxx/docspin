using System;
using System.Collections.Generic;
    

namespace DocSpin2.Models
{
    public partial class DocumentACL
    {
        public int Id { get; set; }
        public AccessControlSetting ACS { get; set; }
    
        public virtual Document Document { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

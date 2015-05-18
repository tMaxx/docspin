using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    

namespace DocSpin2.Models
{
    public partial class RepositoryACL
    {
        public int Id { get; set; }
		[Required]
        public AccessControlSetting ACS { get; set; }
    
        public virtual Repository Repository { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

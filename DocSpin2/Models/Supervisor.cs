using System;
using System.Collections.Generic;


namespace DocSpin2.Models
{
    public partial class Supervisor
    {
        public int Id { get; set; }
    
        public virtual Repository Repository { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

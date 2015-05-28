using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DocSpin2.Models
{
    public partial class Supervisor
    {
        public int Id { get; set; }
		[Required]
		public int RepositoryId { get; set; }
		[Required]
		public string UserId { get; set; }
 
        public virtual Repository Repository { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

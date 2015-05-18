using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DocSpin2.Models
{
    public partial class Comment
    {
        public Comment()
        {
            this.IsRemoved = false;
			this.Timestamp = System.DateTime.Now;
        }
    
        public int Id { get; set; }
		[Required]
		public string Content { get; set; }
		[Required]
		public System.DateTime Timestamp { get; set; }
		[Required]
		public bool IsRemoved { get; set; }

		[Required]
		public virtual ApplicationUser Author { get; set; }
		[Required]
		public virtual Document Document { get; set; }
    }
}

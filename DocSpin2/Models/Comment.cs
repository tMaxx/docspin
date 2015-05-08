using System;
using System.Collections.Generic;


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
        public string Content { get; set; }
        public System.DateTime Timestamp { get; set; }
        public bool IsRemoved { get; set; }
    
        public virtual ApplicationUser Author { get; set; }
        public virtual Document Document { get; set; }
    }
}

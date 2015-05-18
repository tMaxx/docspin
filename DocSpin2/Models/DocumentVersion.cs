using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    

namespace DocSpin2.Models
{
    public partial class DocumentVersion
    {
        public DocumentVersion()
        {
            this.IsRemoved = false;
			this.UploadTimestamp = DateTime.Now;
        }
    
        public int Id { get; set; }
		[Required]
		public System.DateTime FileTimestamp { get; set; }
		[Required]
		public string Filename { get; set; }
		[Required]
		public string OriginalFilename { get; set; }
		[Required]
		public System.DateTime UploadTimestamp { get; set; }
		[Required]
		public bool IsRemoved { get; set; }
		[Required]
		public string Hash { get; set; }
    
		[Required]
        public virtual Document Document { get; set; }
		[Required]
        public virtual ApplicationUser Author { get; set; }
    }
}

using System;
using System.Collections.Generic;
    

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
        public System.DateTime FileTimestamp { get; set; }
        public string Filename { get; set; }
        public string OriginalFilename { get; set; }
        public System.DateTime UploadTimestamp { get; set; }
        public bool IsRemoved { get; set; }
        public string Hash { get; set; }
    
        public virtual Document Document { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}

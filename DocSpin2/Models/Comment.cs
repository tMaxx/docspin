//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocSpin2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public Comment()
        {
            this.IsRemoved = false;
        }
    
        public int Id { get; set; }
        public string Content { get; set; }
        public System.DateTime Timestamp { get; set; }
        public bool IsRemoved { get; set; }
    
        public virtual ApplicationUser Author { get; set; }
        public virtual Document Document { get; set; }
    }
}

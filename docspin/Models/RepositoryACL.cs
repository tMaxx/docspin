//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace docspin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RepositoryACL
    {
        public int Id { get; set; }
        public byte ACS { get; set; }
    
        public virtual Repository Repository { get; set; }
        public virtual User User { get; set; }
    }
}
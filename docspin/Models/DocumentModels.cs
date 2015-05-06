using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace docspin.Models
{
    public class DocumentContext : DbContext
    {
        public DocumentContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public System.Data.Entity.DbSet<docspin.Models.Document> Documents { get; set; }
    }


}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace docspin.Models
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public System.Data.Entity.DbSet<docspin.Models.Repository> Repositories { get; set; }
    }

    public class CreateRepository
    {
        [Required]
        [Display(Name = "Repository Name")]
        public string Name { get; set; }
    }

   
}

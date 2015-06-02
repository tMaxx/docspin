using DocSpin2.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DocSpin2.Models;
using System.Data.Entity.Validation;
using System.Net;

namespace DocSpin2.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<DocSpin2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
#if DEBUG
			AutomaticMigrationDataLossAllowed = true;
#endif
        }

        protected override void Seed(DocSpin2.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
			if (context.Users.Where(u => u.Email == "admin@admin.admin").Count() == 0)
			{
				ApplicationUserManager aum = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
				var user = new ApplicationUser
				{
					UserName = "admin@admin.admin",
					Email = "admin@admin.admin",
					FullName = "System Administrator",
					Role = Models.UserRole.Admin
				};
				IdentityResult result = aum.Create(user, "Admin");
			}

        }
    }
}

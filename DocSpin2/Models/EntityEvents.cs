using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocSpin2.Models.EntityEvents
{
	interface IAdded
	{
		void entityOnAdded(ApplicationDbContext ctx);
	}

	interface IModified
	{
		void entityOnModified(ApplicationDbContext ctx);
	}
}

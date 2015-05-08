using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using docspin.Models;

namespace docspin.Filters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
	{
		private static SimpleMembershipInitializer _initializer;
		private static object _initializerLock = new object();
		private static bool _isInitialized;

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// Ensure ASP.NET Simple Membership is initialized only once per app start
			LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
		}

		private class SimpleMembershipInitializer
		{
			public SimpleMembershipInitializer()
			{
				Database.SetInitializer<DataModelContainer>(null);

				try
				{
					using (DataModelContainer context = new DataModelContainer())
					{
						if (!context.Database.Exists())
							((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
					}

					WebSecurity.InitializeDatabaseConnection("DataModelContainer", "User", "Id", "UserName", autoCreateTables: true);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException("SimpleMembership doesn't work, AGAIN. Msg: " + ex.Message, ex);
				}
			}
		}
	}
}

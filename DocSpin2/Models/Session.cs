using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocSpin2.Models
{
	public class Session
	{
		public static bool IsInitialized
		{
			get
			{
				if (HttpContext.Current.Session["UserInited"] == null)
					return false;
				return (bool)HttpContext.Current.Session["UserInited"];
			}
			set
			{
				HttpContext.Current.Session["UserInited"] = value;
			}
		}

		public static bool IsSet(string key)
		{
			return HttpContext.Current.Session[key] != null;
		}

		public static void Remove(string key)
		{
			HttpContext.Current.Session.Remove(key);
		}

		public static T Get<T>(string key)
		{
			if (HttpContext.Current.Session[key] == null)
				return default(T);
			return (T)HttpContext.Current.Session[key];
		}

		public static void Set<T>(string key, T value)
		{
			HttpContext.Current.Session[key] = value;
		}

		public static void Clear()
		{
			HttpContext.Current.Session.RemoveAll();
		}
	}
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace DocSpin2.Models
{    
	[ModelBinder(typeof(AccessControlSettingBinder))]
    [Flags]
    public enum AccessControlSetting : byte
    {
        None = 0,
        Read = 1,
        Comment = 3,
        Write = 7,
		//Move = 8,
		//[Display(Name = "Archived")]
		//Archival = 16,
		[Display(Name = "Supervisors only")]
        SupervisorOnly = 32
    }

	
	public class AccessControlSettingHelper
	{
		public static AccessControlSetting Default { get { return AccessControlSetting.Write; } }

		public static string DescribeSingle(uint a)
		{
			if ((a & 32u) == 32u)
				return "Supervisors only";
			return Enum.GetName(typeof(AccessControlSetting), a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string DescribeSingle(AccessControlSetting a)
		{
			return DescribeSingle((uint)a);
		}

		public static string Describe(AccessControlSetting a)
		{
			StringBuilder ret = new StringBuilder();

			for (uint i = 32; i > 0; i >>= 1)
				if (((uint)a & i) == i)
				{
					ret.Append(DescribeSingle((AccessControlSetting)i));
					ret.Append(", ");
				}

			return ret.ToString(0, ret.Length - 2);
		}

		//weakly check if action can be performed
		public static bool Compare(AccessControlSetting against, AccessControlSetting action)
		{
			//is a supervisor of some sort
			if (action.HasFlag(AccessControlSetting.SupervisorOnly))
				return true;
			against &= action;
			//supervisor should be checked beforehand
			if (against.HasFlag(AccessControlSetting.SupervisorOnly))
				return false;
			//filter values
			return against == action;
		}

		public static bool Compare(AccessControlSetting against, AccessControlSetting action, bool isSuper)
		{ return Compare(against, (isSuper ? action |= AccessControlSetting.SupervisorOnly : action)); }
	}


	public class AccessControlSettingBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			HttpRequestBase req = controllerContext.HttpContext.Request;
			string[] vals = req.Form.GetValues("ACS");
			byte temp;
			AccessControlSetting ret = AccessControlSetting.None;

			foreach (string v in vals)
			{
				if (byte.TryParse(v, out temp) && temp != 0)
					ret |= (AccessControlSetting)temp;
			}
			return ret;
		}
	}

}

using System;
using System.ComponentModel.DataAnnotations;
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
        Comment = 2,
        Write = 4,
        Move = 8,
		//[Display(Name = "Archived")]
        Archival = 16,
		[Display(Name = "Supervisors only")]
        SupervisorOnly = 32
    }

	public class AccessControlSettingHelper
	{
		//public static 
	}

	public class AccessControlSettingBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			HttpRequestBase req = controllerContext.HttpContext.Request;
			string[] vals = req.Form.GetValues("ACS");
			int temp;
			AccessControlSetting ret = AccessControlSetting.None;

			foreach (string v in vals)
			{
				try	{ temp = byte.Parse(v);	}
				catch (Exception e) { continue; }
				ret |= (AccessControlSetting)temp;
			}
			return ret;
		}
	}

}

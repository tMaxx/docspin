using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DocSpin2.Models
{
	[NotMapped]
	public class ACLItemViewModel
	{
		public int model_id;
		public string user_id, user_name;
		public AccessControlSetting acs;
	}

	[NotMapped]
	public class ACLViewModel
	{
		public enum Type
		{
			Repository,
			Document
		}
		public Type src_type;
		public int src_type_id;
		public string src_type_name;

		public IEnumerable<ACLItemViewModel> elements;
	}

	[NotMapped]
	public class ACLCreateModel
	{
		public int object_id { get; set; }
		public string object_type { get; set; }
		public string user_id { get; set; }
		public AccessControlSetting ACS { get; set; }
	}

	[NotMapped]
	public class ACLDeleteModel : ACLCreateModel
	{
		public string object_name;
		public string repo_name = "";
		public string user_name;
	}
}
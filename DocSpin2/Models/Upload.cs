using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocSpin2.Models
{

    public class Upload
    {
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }

}

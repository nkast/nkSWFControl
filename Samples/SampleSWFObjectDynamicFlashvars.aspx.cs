using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nkSWFControl;

namespace TestWeb
{
    public partial class SampleSWFObjectDynamicFlashvars : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SWFControl1.Flashvars.Add(new Flashvar("page","13"));

        }
    }
}

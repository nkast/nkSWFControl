using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nkSWFControl.Renderers
{
    class RendererNestedObject : RendererBase
    {   
        public RendererNestedObject(SWFControl ctrl):base(ctrl) 
        {
        }

        #region IRenderer Members

        public override void CreateChildControls()
        {   
            ctrl.Controls.Add(new LiteralControl("\n<!--[if !IE]>-->\n"));
            WebControl nobj = new WebControl(HtmlTextWriterTag.Object);
            nobj.Attributes.Add("type", "application/x-shockwave-flash");
            nobj.Attributes.Add("width", ctrl.Width.Value.ToString());
            nobj.Attributes.Add("height", ctrl.Width.Value.ToString());
            nobj.Attributes.Add("data", ctrl.Movie);
            ctrl.Controls.Add(nobj);
            ctrl.Controls.Add(new LiteralControl("\n<!--<![endif]-->\n"));

            nobj.Controls.Add(new LiteralControl("<!--<![endif]-->\n"));

            CreateParam(nobj, "movie", ctrl.Movie);
            CreateFlashvars(nobj);

            CreateAlternativeContent(nobj);

            nobj.Controls.Add(new LiteralControl("\n<!--[if !IE]>-->"));
            return;
        }

        public override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Object; }
        }

        public override void AddAttributes(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributes(writer); // base will add ID
            writer.AddAttribute("classid", "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000");
            writer.AddAttribute("width", ctrl.Width.Value.ToString());
            writer.AddAttribute("height", ctrl.Width.Value.ToString());
        }

        #endregion
    }
}

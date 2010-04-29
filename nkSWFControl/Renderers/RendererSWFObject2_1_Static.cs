using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResourceAttribute("nkSWFObject.SWFObject2_1.swfobject.js", "application/x-javascript")]
[assembly: WebResourceAttribute("nkSWFObject.SWFObject2_1.expressInstall.swf", "application/x-shockwave-flash")]

namespace nkSWFControl.Renderers
{
    class RendererSWFObject2_1_Static : RendererBase
    {
        internal RendererSWFObject2_1_Static(SWFControl ctrl): base(ctrl) 
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

        public override void PreRender()
        {
            ClientScriptManager cs = ctrl.Page.ClientScript;
            Type rType = this.GetType();

            cs.RegisterClientScriptResource(rType, "nkSWFObject.SWFObject2_1.swfobject.js");
            //cs.RegisterClientScriptResource(rType, "nkSWFObject.SWFObject2_1.expressInstall.swf");

            //load script
            object[] args = {
                    ctrl.Movie,
                    ctrl.UniqueID,
                    ctrl.Width.Value.ToString(),
                    ctrl.Height.Value.ToString(),
                    "9.0.0",
                    cs.GetWebResourceUrl(rType, "expressInstall.swf")
            };
            string script = String.Format("swfobject.embedSWF('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", args);
            cs.RegisterStartupScript(rType, this.ToString(), script, true);

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

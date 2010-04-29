using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.Collections;
using nkSWFControl.Renderers;

namespace nkSWFControl
{
    [
     AspNetHostingPermission(SecurityAction.Demand,Level = AspNetHostingPermissionLevel.Minimal),
     AspNetHostingPermission(SecurityAction.InheritanceDemand,Level = AspNetHostingPermissionLevel.Minimal),
     //DefaultProperty(""),     
     ParseChildren(true),
     ToolboxData("<{0}:SWFControl runat=\"server\" PublishingMethod=\"NestedObject\" Width=\"80px\" Height=\"60px\" >\n\t<AlternativeContentTemplate></AlternativeContentTemplate>\n\t</{0}:SWFControl>")
    ]
    public partial class SWFControl : WebControl, INamingContainer
    {
        IRenderer _renderer;
        private ITemplate _template;    

        
        private void ChangeRenderer()
        {
            _renderer = null;


            if(DesignMode)
            {
                _renderer = new RendererDesignMode(this);
                return;
            }

            switch (PublishingMethod)
            {
                case PublishingMethod.NestedEmbed: // html4
                    _renderer = new RendererNestedEmbed(this);
                    break;
                case PublishingMethod.NestedObject: // XHTML 1.0
                    _renderer = new RendererNestedObject(this);                    
                    break;

                case PublishingMethod.SWFObject2_1_Dynamic:
                    _renderer = new RendererSWFObject2_1_Dynamic(this);
                    break;
                case PublishingMethod.SWFObject2_1_Static:
                    _renderer = new RendererSWFObject2_1_Static(this);
                    break;

                case PublishingMethod.SWFObject2_2_Dynamic:
                    _renderer = new RendererSWFObject2_2_Dynamic(this);
                    break;
                case PublishingMethod.SWFObject2_2_Static:
                    _renderer = new RendererSWFObject2_2_Static(this);
                    break;
            }
            return;
        }

        protected override void CreateChildControls()
        {
            _renderer.CreateChildControls();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            _renderer.PreRender();
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            _renderer.AddAttributes(writer);
        }

        protected override HtmlTextWriterTag TagKey
        {
            get {return _renderer.TagKey;}
        }
            

        internal string GetFlashVars()
        {
            string fvs = "";

           
            foreach (Flashvar fv in Flashvars)
            {
                fvs += fv.GetPair();
                fvs += "&";
            }
            fvs = fvs.Substring(0, fvs.Length - 1);
           

            return fvs;
        }
    }
}

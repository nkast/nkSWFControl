using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace nkSWFControl.Renderers
{
    class RendererDesignMode : RendererBase
    {
       public RendererDesignMode(SWFControl ctrl):base(ctrl)
       {
       }

       #region IRenderer Members

       public override HtmlTextWriterTag TagKey
       {
           get { return HtmlTextWriterTag.Div; }
       }

       public override void CreateChildControls()
       {
           ctrl.Controls.Add(new LiteralControl( String.Format("<strong>{0}</strong> - '{1}'" ,ctrl.ClientID , ctrl.Movie) ) );            
       }

       public override void AddAttributes(System.Web.UI.HtmlTextWriter writer)
       {
           base.AddAttributes(writer); // add ID

            String style = "";
            style += String.Format("width:{0};", ctrl.Width);
            style += String.Format("height:{0}; ", ctrl.Height);
            style += String.Format("border:solid 1px black;");

            writer.AddAttribute("style", style);

            return;
       }


        #endregion
    }
}

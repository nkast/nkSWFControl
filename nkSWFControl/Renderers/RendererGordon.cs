﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

[assembly: WebResourceAttribute("nkSWFObject.Gordon.gordon.js", "application/x-javascript")]

namespace nkSWFControl.Renderers
{
    class RendererSWFObject2_1_Dynamic : RendererBase
    {
        internal RendererSWFObject2_1_Dynamic(SWFControl ctrl): base(ctrl) 
        {
        }

        #region IRenderer Members

        public override void CreateChildControls()
        {
            CreateAlternativeContent(ctrl);

            return;
        }

        public override void PreRender()
        {
            ClientScriptManager cs = ctrl.Page.ClientScript;
            Type rType = this.GetType();

            cs.RegisterClientScriptResource(rType, "nkSWFObject.Gordon.gordon.js");

            //load script
            object[] args = {
                    ctrl.Movie, 
                    ctrl.UniqueID,
                    ctrl.Width.Value.ToString(), 
                    ctrl.Height.Value.ToString(),
                    "9.0.0",
                    cs.GetWebResourceUrl(rType, "expressInstall.swf")
            };
            string script = "";
            script += "\n\tvar params = { \n\t };";
            script += String.Format("\n\t new Gordon.Movie('{0}', params );", ctrl.Movie );
            cs.RegisterStartupScript(rType, this.ToString(), script, true);

            return;
        }

        public override void AddAttributes(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributes(writer); // base will add ID
        }


        public override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        #endregion

       

    }
}

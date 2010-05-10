/*
Copyright (c) 2010 Kastellanos Nikos

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

[assembly: WebResourceAttribute("nkSWFControl.SWFObject2_2.swfobject.js", "application/x-javascript")]
[assembly: WebResourceAttribute("nkSWFControl.SWFObject2_2.expressInstall.swf", "application/x-shockwave-flash")]

namespace nkSWFControl.Renderers
{
    class RendererSWFObject2_2_Dynamic : RendererBase
    {
        internal RendererSWFObject2_2_Dynamic(SWFControl ctrl): base(ctrl) 
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

            cs.RegisterClientScriptResource(rType, "nkSWFControl.SWFObject2_2.swfobject.js");
            //cs.RegisterClientScriptResource(rType, "nkSWFControl.SWFObject2_2.expressInstall.swf");

            //load script
            object[] args = {
                    ctrl.Movie, 
                    ctrl.UniqueID,
                    ctrl.Width.Value.ToString(), 
                    ctrl.Height.Value.ToString(),
                    ctrl.Version,
                    cs.GetWebResourceUrl(rType, "expressInstall.swf")
            };
            string script = "";
            script += "\n\t var flashvars = {" + ctrl.GetJFlashVars() + "};";
            script += "\n\t var params = {" + ctrl.GetJParams() + "};";
            script += String.Format("\n\t swfobject.embedSWF('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', flashvars, params );", args);
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

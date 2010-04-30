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

[assembly: WebResourceAttribute("nkSWFObject.Gordon.gordon.js", "application/x-javascript")]

namespace nkSWFControl.Renderers
{
    class RendererGordon : RendererBase
    {
        internal RendererGordon(SWFControl ctrl) : base(ctrl) 
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
                    ctrl.UniqueID,
                    ctrl.Width.Value.ToString(), 
                    ctrl.Height.Value.ToString()
            };
            string script = "";
            script += String.Format("\n\tvar params = { \n\t id:{0}, width:{1}, height:{2} };", args);
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

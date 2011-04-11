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
            nobj.Attributes.Add("height", ctrl.Height.Value.ToString());
            nobj.Attributes.Add("data", ctrl.ResolvedMovie);
            ctrl.Controls.Add(nobj);
            ctrl.Controls.Add(new LiteralControl("\n<!--<![endif]-->\n"));

            nobj.Controls.Add(new LiteralControl("<!--<![endif]-->\n"));

            CreateParam(nobj, "movie", ctrl.ResolvedMovie);
            CreateParam(nobj, "wmode", ctrl.WindowMode.ToString() );

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
            writer.AddAttribute("height", ctrl.Height.Value.ToString());
        }

        #endregion
    }
}



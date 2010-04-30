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

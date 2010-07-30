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
    class RendererNestedEmbed : RendererBase
    {
        public RendererNestedEmbed(SWFControl ctrl) : base(ctrl) 
        {
        }

        #region IRenderer Members

        public override void CreateChildControls()
        {
            string movieUrl = ctrl.ResolveClientUrl(ctrl.ResolvedMovie);
            

            CreateParam("movie", movieUrl);
            CreateParam("wmode", ctrl.WindowMode.ToString());
            CreateParam("scale", ctrl.Scale.ToString());

            CreateFlashvars(ctrl);

            WebControl embed = new WebControl(HtmlTextWriterTag.Embed);
            embed.Attributes.Add("type", "application/x-shockwave-flash");
            embed.Attributes.Add("pluginspage", "http://www.macromedia.com/go/getflashplayer"); 
            embed.Attributes.Add("width", ctrl.Width.Value.ToString());
            embed.Attributes.Add("height", ctrl.Height.Value.ToString());
            embed.Attributes.Add("src", ctrl.ResolvedMovie);
            embed.Attributes.Add("wmode", ctrl.WindowMode.ToString());
            embed.Attributes.Add("scale", ctrl.Scale.ToString());
            //embed.Attributes.Add("quality", "high");
            //embed.Attributes.Add("name", "movie" );
            ctrl.Controls.Add(embed);
          
            /*
            ITemplate templ = ctrl.AlternativeContentTemplate;
            if (templ != null)
            {
                CompiledTemplateBuilder ctb = templ as CompiledTemplateBuilder;
                ctb.InstantiateIn(nobj);
            }
             */

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
            writer.AddAttribute("codebase", "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0");
            writer.AddAttribute("width", ctrl.Width.Value.ToString());
            writer.AddAttribute("height", ctrl.Width.Value.ToString());
        }

        #endregion
    }
}


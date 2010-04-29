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
            CreateParam("movie", ctrl.Movie);
            CreateFlashvars(ctrl);

            WebControl embed = new WebControl(HtmlTextWriterTag.Embed);
            embed.Attributes.Add("type", "application/x-shockwave-flash");
            embed.Attributes.Add("pluginspage", "http://www.macromedia.com/go/getflashplayer"); 
            embed.Attributes.Add("width", ctrl.Width.Value.ToString());
            embed.Attributes.Add("height", ctrl.Width.Value.ToString());
            embed.Attributes.Add("src", ctrl.Movie);
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


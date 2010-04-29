using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace nkSWFControl.Renderers
{
    interface IRenderer
    {
        void CreateChildControls(); 
        void PreRender();
        void AddAttributes(HtmlTextWriter writer);
        HtmlTextWriterTag TagKey { get; } 
    }
}

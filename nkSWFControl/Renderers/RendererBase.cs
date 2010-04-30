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
    class RendererBase:IRenderer
    {
        protected SWFControl ctrl;

        public RendererBase(SWFControl ctrl)
        {
            this.ctrl = ctrl;
        }

        #region IRenderer Members

        public virtual void CreateChildControls() {}
        public virtual void PreRender() {}

        public virtual void AddAttributes(System.Web.UI.HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ctrl.ID);
        }

        public virtual HtmlTextWriterTag TagKey { get { return HtmlTextWriterTag.Span; } }
            

        #endregion

        protected WebControl CreateParam(string name, string value)
        {
            return CreateParam(ctrl, name, value);
        }

        protected WebControl CreateParam(Control control, string name, string value)
        {
            WebControl param = new WebControl(HtmlTextWriterTag.Param);
            param.Attributes.Add("name", name);
            param.Attributes.Add("value", value);
            control.Controls.Add(param);
            return param;
        }

        protected void CreateFlashvars(WebControl control)
        {
            if (ctrl.Flashvars != null && ctrl.Flashvars.Count > 0)
                CreateParam(control, "flashvars", ctrl.GetFlashVars());
        }

        protected void CreateAlternativeContent(WebControl control)
        {
            CompiledTemplateBuilder ctb = ctrl.AlternativeContentTemplate as CompiledTemplateBuilder;
            if (ctb != null) ctb.InstantiateIn(control);            

            return;
        }

    }
}

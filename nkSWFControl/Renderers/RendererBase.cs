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

        private void CreateFlashvars(WebControl control)
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

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
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.Collections;
using nkSWFControl.Renderers;
using System.IO;
using System.Design;
using System.ComponentModel.Design;


namespace nkSWFControl
{
    [
     AspNetHostingPermission(SecurityAction.Demand,Level = AspNetHostingPermissionLevel.Minimal),
     AspNetHostingPermission(SecurityAction.InheritanceDemand,Level = AspNetHostingPermissionLevel.Minimal),
     //DefaultProperty(""),     
     ParseChildren(true),
     ToolboxData("<{0}:SWFControl runat=\"server\" PublishingMethod=\"NestedObject\" Width=\"80px\" Height=\"60px\" >\n\t<AlternativeContentTemplate></AlternativeContentTemplate>\n\t</{0}:SWFControl>")
    ]
    [Designer(typeof(SWFControlDesigner))]
    public partial class SWFControl : CompositeControl
    {
        IRenderer _renderer;
        private ITemplate _template;    

        
        private void ChangeRenderer()
        {
            _renderer = null;


            if(DesignMode)
            {
                _renderer = new RendererDesignMode(this);
                return;
            }

            switch (PublishingMethod)
            {
                case PublishingMethod.NestedEmbed: // html4
                    _renderer = new RendererNestedEmbed(this);
                    break;
                case PublishingMethod.NestedObject: // XHTML 1.0
                    _renderer = new RendererNestedObject(this);                    
                    break;

                case PublishingMethod.SWFObject2_1_Dynamic:
                    _renderer = new RendererSWFObject2_1_Dynamic(this);
                    break;
                case PublishingMethod.SWFObject2_1_Static:
                    _renderer = new RendererSWFObject2_1_Static(this);
                    break;

                case PublishingMethod.SWFObject2_2_Dynamic:
                    _renderer = new RendererSWFObject2_2_Dynamic(this);
                    break;
                case PublishingMethod.SWFObject2_2_Static:
                    _renderer = new RendererSWFObject2_2_Static(this);
                    break;
                case PublishingMethod.Gordon:
                    _renderer = new RendererGordon(this);
                    break;
            }
            return;
        }

        protected override void CreateChildControls()
        {
            _renderer.CreateChildControls();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            _renderer.PreRender();
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            _renderer.AddAttributes(writer);
        }

        protected override HtmlTextWriterTag TagKey
        {
            get {return _renderer.TagKey;}
        }
            

        internal string GetFlashVars()
        {
            StringBuilder builder = new StringBuilder();
            string delimiter = "";
            foreach (Flashvar fv in Flashvars)
            {
                builder.Append(delimiter);
                builder.Append(fv.Name);
                builder.Append("=");
                builder.Append(fv.Value);
                delimiter = "&";
            }
            return builder.ToString();
        }

        internal string GetJFlashVars()
        {
            StringBuilder builder = new StringBuilder();
            string delimiter = "";
            foreach (Flashvar fv in Flashvars)
            {
                builder.Append(delimiter);
                builder.Append(fv.Name);
                builder.Append(":'");
                builder.Append(fv.Value);
                builder.Append("'");
                delimiter = ",";
            }
            return builder.ToString();
        }

        internal string GetJParams()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("wmode:\"");
            builder.Append(WindowMode.ToString());
            builder.Append("\"");
            //builder.Append(",");
            
            return builder.ToString();
        }

        internal void Autosize()
        {
            string file = Movie;
            if(File.Exists(file))
            {
                Width = 300;
            }

            IDesignerHost dh = (IDesignerHost)Site.GetService(typeof(IDesignerHost));

            string t = HttpRuntime.AppDomainAppVirtualPath;
            t = HttpRuntime.AspInstallDirectory;


            /*
            //EnvDTE.DTE _dte = (EnvDTE.DTE)Site.GetService(typeof(EnvDTE.DTE));
             Array projects = (System.Array)devenv.ActiveSolutionProjects;

     if((projects.Length == 0) || (projects.Length > 1))
     {
       html = "Exactly one project must be active";
     } 
     else 
     {
       // go through the items of the project to find the configuration
       EnvDTE.Project project = (EnvDTE.Project)(projects.GetValue(0));
       foreach(EnvDTE.ProjectItem item in project.ProjectItems)
       {
         // if it is the configuration, then open it up
         if(string.Compare(item.Name, "web.config", true) == 0)
           ...
       }
     }
*/

            Literal l = new Literal();
            l.Text = Path.GetFullPath("~/");

            Movie = Path.GetFullPath("~/");

            Controls.Add(l);

            Width = 400;
            Height = 200;
            return;
        }
    }
}

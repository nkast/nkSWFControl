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
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;
using System.Drawing.Design;

namespace nkSWFControl
{
    public partial class SWFControl : CompositeControl
    {
       
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(PublishingMethod.NestedObject)]
        [Localizable(true)]
        [Description("Publishing Method")]
        public PublishingMethod PublishingMethod
        {
            get
            {
                if (ViewState["PublishingMethod"] == null) 
                    return PublishingMethod.NestedObject;
                return (PublishingMethod)ViewState["PublishingMethod"];
            }
            set
            {
                ViewState["PublishingMethod"] = value;
                ChangeRenderer();
            }
        }

        [Bindable(true)]
        [Category("Properties")]
        [Description("SWF filename")]
        [DefaultValue("10.0.45")]
        [Localizable(true)]
        public String Version
        {
            get
            {
                String s = (String)ViewState["Version"];
                return ((s != null) ? s : "10.0.45");
            }
            set
            {
                ViewState["Version"] = value;
            }
        }

        [Bindable(true)]
        [Category("Properties")]
        [Description("SWF filename")]
        [DefaultValue("default.swf")]
        [UrlProperty]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]        
        [Localizable(true)]        
        public String Movie
        {
            get
            {
                String s = (String)ViewState["Movie"];
                return ((s != null) ? s : "default.swf");
            }
            set
            {
                ViewState["Movie"] = value;
            }
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(PublishingMethod.NestedObject)]
        [Localizable(true)]
        [Description("Publishing Method")]
        public WindowMode WindowMode
        {
            get
            {
                if (ViewState["WindowMode"] == null)
                    return WindowMode.Opaque;
                return (WindowMode)ViewState["WindowMode"];
            }
            set
            {
                ViewState["WindowMode"] = value;
            }
        }
      
       [Browsable(false)]
       [PersistenceMode(PersistenceMode.InnerProperty)]
       [DefaultValue(typeof(ITemplate), null)]
       [Description("Control template")]
       [TemplateContainer(typeof(SWFControl))]
       public virtual ITemplate AlternativeContentTemplate
       {
            get
            {
                return _template;
            }
            set
            {
                _template = value;
            }
       }
        
       List<Flashvar> _flashvars;

       [Bindable(true)]
       [Category("Properties")]
       [Description("Flash variables")]
       [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
       [Editor(typeof(FlashvarsCollectionEditor), typeof(UITypeEditor))]
       [PersistenceMode(PersistenceMode.InnerProperty)]
       public List<Flashvar> Flashvars
       {
           get
           {
               if (_flashvars == null) _flashvars = new List<Flashvar>();
               return _flashvars;
           }
       }


    }
}

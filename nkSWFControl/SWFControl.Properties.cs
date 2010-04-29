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
    public partial class SWFControl : WebControl
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
        [DefaultValue("10.0.")]
        [Localizable(true)]
        public String Version
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
        [Category("Properties")]
        [Description("SWF filename")]
        [DefaultValue("default.swf")]
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

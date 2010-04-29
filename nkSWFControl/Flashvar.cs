using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;


namespace nkSWFControl
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Flashvar
    {
        private string _name;
        private string _value;

        public Flashvar(): this(String.Empty, String.Empty)
        {
        }

        public Flashvar(string name, string value)
        {
            this._name = name;
            this._value = value;            
        }

        [Category("Properties")]
        [DefaultValue("")]
        [Description("Name")]
        [NotifyParentProperty(true)]        
        public String Name
        {
            get {return _name;}
            set {_name = value;}
        }

        [Category("Properties")]
        [DefaultValue("")]
        [Description("Value")]
        [NotifyParentProperty(true)]
        public String Value
        {
            get { return _value;}
            set { _value = value;}
        }


        internal string GetPair()
        {
            return _name + "=" + _value;
        }
    }
}

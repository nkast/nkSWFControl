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

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design.WebControls;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace nkSWFControl
{
    class SWFControlDesigner : CompositeControlDesigner
    {

        private DesignerActionListCollection _actionLists = null;
        
        // Return a custom ActionList collection
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.AddRange(base.ActionLists);
                    // Add a custom DesignerActionList
                    _actionLists.Add(new ActionList(this));
                }
                return _actionLists;
            }
        }

        public class ActionList : DesignerActionList
        {
            private SWFControlDesigner _parent;
            private DesignerActionItemCollection _items;

            // Constructor
            public ActionList(SWFControlDesigner parent)
                : base(parent.Component)
            {
                _parent = parent;

            }

            // Create the ActionItem collection and add one command
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                if (_items == null)
                {
                    _items = new DesignerActionItemCollection();
                    _items.Add(new DesignerActionMethodItem(this, "Autosize", "Auto size", true));
                }
                return _items;
            }

            // ActionList command to change the text size
            private void Autosize()
            {
                // Get a reference to the parent designer's associated control
                SWFControl ctl = (SWFControl)_parent.Component;
                ctl.Autosize();
                ctl.Attributes["Width"] = ctl.Width.Value.ToString();
                string ts = ctl.AppRelativeTemplateSourceDirectory;
                string ts2 = ctl.TemplateSourceDirectory;
                


                return;
            }
        }
    

       
       

    }
}

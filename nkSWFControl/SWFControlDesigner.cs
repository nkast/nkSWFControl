using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design.WebControls;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Web.UI.Design;
using System.IO;
using SWFFile.SWF;

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

                string filename = ctl.Movie;
                string path = ctl.SitePath;
                //string path = Path.GetFullPath("~/");
                string fullpath = Path.Combine(path, filename);
                if (!File.Exists(fullpath))
                {
                    return;
                }

                SWFFile.SWF.SWFReader swfReader = new SWFReader(fullpath);
                ctl.Width = swfReader.Header.FrameWidth;
                ctl.Height = swfReader.Header.FrameHeight;
                
                
                //update size
                _parent.Tag.SetAttribute("Width", ctl.Width.ToString());
                _parent.Tag.SetAttribute("Height", ctl.Height.ToString());
                                
                _parent.UpdateDesignTimeHtml();

                return;
            }
        }
    

       
       

    }
}

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
                    _items.Add(new DesignerActionMethodItem(this, "Fullsize", "Full size", true));
                    _items.Add(new DesignerActionMethodItem(this, "Autosize", "Auto size", true));
                    _items.Add(new DesignerActionMethodItem(this, "SetSize468x60", "468x60", true));
                    _items.Add(new DesignerActionMethodItem(this, "SetSize300x250", "300x250", true));
                    _items.Add(new DesignerActionMethodItem(this, "SetSize120x600", "120x600", true));
                    _items.Add(new DesignerActionMethodItem(this, "SetSize800x600", "800x600", true));
                    _items.Add(new DesignerActionMethodItem(this, "SetSize1024x768", "1024x768", true));
                }
                return _items;
            } 
            
            // ActionList command to change the size
            private void SetSize468x60() {SetSize(468,60);}
            private void SetSize300x250() { SetSize(300, 250); }
            private void SetSize120x600() { SetSize(120, 600); }
            private void SetSize800x600() { SetSize(800, 600); }
            private void SetSize1024x768() { SetSize(1024, 768); }

            // ActionList command to change the size
            private void Fullsize() 
            {
                SWFControl ctl = (SWFControl)_parent.Component;
                ctl.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                ctl.Height = System.Web.UI.WebControls.Unit.Percentage(100);
                _parent.Tag.SetAttribute("Width", "100%");
                _parent.Tag.SetAttribute("Height", "100%");
                _parent.UpdateDesignTimeHtml();                
            }
           

            private void SetSize(int w, int h)
            {
                SWFControl ctl = (SWFControl)_parent.Component;
                ctl.Width = System.Web.UI.WebControls.Unit.Pixel(w);
                ctl.Height = System.Web.UI.WebControls.Unit.Pixel(h);
                _parent.Tag.SetAttribute("Width", ctl.Width.ToString());
                _parent.Tag.SetAttribute("Height", ctl.Height.ToString());
                _parent.UpdateDesignTimeHtml();
            }


            private void Autosize()
            {
                // Get a reference to the parent designer's associated control
                SWFControl ctrl = (SWFControl)_parent.Component;

                string filename = ctrl.ResolvedMovie;
                string path = ctrl.SitePath;
                if(path==String.Empty)
                {
                    //this seems to return the solution path.
                    //string fpath = Path.GetFullPath("~/");
                    //path = fpath.Split('~')[0];
                    //ctrl.SitePath = path;
                    return;
                }
                
                
                string fullpath = Path.Combine(path, filename);
                if (!File.Exists(fullpath)) return;
                SWFFile.SWF.SWFReader swfReader = new SWFReader(fullpath);                
                SetSize(swfReader.Header.FrameWidth, swfReader.Header.FrameHeight);

                return;
            }
        }
    

       
       

    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace nkSWFControl
{

    class FlashvarsCollectionEditor : CollectionEditor
    {      
        public FlashvarsCollectionEditor(Type type) : base(type)
        {
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(Flashvar);
        }
      
    }
     
}

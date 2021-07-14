using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EMS.Dialog.EMSMenu
{
    class CMComposite : ContextMenu
    {
        public MenuItem AddParallel { get; set; }
        public MenuItem AddAlternative { get; set; }
        public MenuItem AddDiscrete { get; set; }
        public MenuItem AddContinuous { get; set; }

        public MenuItem RemoveNode { get; set; }

        public MenuItem EditValues { get; set; }

        public CMComposite()
        {
            AddParallel = new MenuItem();
            AddParallel.Header = "Add parallel node";

            AddAlternative = new MenuItem();
            AddAlternative.Header = "Add alternative node";

            AddDiscrete = new MenuItem();
            AddDiscrete.Header = "Add discrete node";

            AddContinuous = new MenuItem();
            AddContinuous.Header = "Add continuous node";

            RemoveNode = new MenuItem();
            RemoveNode.Header = "Remove node";

            EditValues = new MenuItem();
            EditValues.Header = "Edit Values";
        }
    }
}

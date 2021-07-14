using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EMS.Dialog
{
    class EMSEditorContextMenu : ContextMenu
    {
        public MenuItem AddParallel { get; set; }
        public MenuItem AddAlternative { get; set; }
        public MenuItem AddDiscrete { get; set; }
        public MenuItem AddContinuous { get; set; }

        public MenuItem RemoveNode { get; set; }

        public MenuItem EditValues { get; set; }

        /// <summary>
        /// Initialisiert das Kontextmenü mit den Auswahlmöglichkeiten:
        /// - AddParallel
        /// - AddAlternative
        /// </summary>
        public void SetInitMenu()
        {
            this.Items.Clear();
            this.Items.Add(AddParallel);
            this.Items.Add(AddAlternative);
        }

        /// <summary>
        /// Initialisiert das Kontextmenü mit den Auswahlmöglichkeiten:
        /// - AddParallel
        /// - AddAlternative
        /// - AddDiscrete
        /// - AddContinuous
        /// - RemoveNode
        /// </summary>
        public void SetComplexMenu()
        {
            SetInitMenu();
            this.Items.Add(AddDiscrete);
            this.Items.Add(AddContinuous);
            this.Items.Add(RemoveNode);
        }

        /// <summary>
        /// Initialisiert das Kontextmenü mit den Auswahlmöglichkeiten:
        /// - EditValues
        /// - RemoveNode
        /// </summary>
        public void SetLeafMenu()
        {
            this.Items.Clear();
            this.Items.Add(EditValues);
            this.Items.Add(RemoveNode);
        }

        /// <summary>
        /// Erzeugt ein Objekt mit den Eigenschaften:
        /// - AddParallel
        /// - AddAlternative
        /// - AddDiscrete
        /// - AddContinuous
        /// - RemoveNode
        /// - EditValues
        /// </summary>
        public EMSEditorContextMenu()
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

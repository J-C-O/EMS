using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;

namespace EMS.Editor
{
    /// <summary>
    /// Interaktionslogik für toolbox.xaml
    /// </summary>
    public partial class Toolbox : UserControl
    {
        Graph factorNode = new Graph();
        //Graph subGraphNode = new Graph();
        Graph leafNode = new Graph();

        GraphViewer factorViewer = new GraphViewer();
        //GraphViewer subGraphViewer = new GraphViewer();
        GraphViewer leafViewer = new GraphViewer();

        Graph parallelNode = new Graph();
        GraphViewer parallelViewer = new GraphViewer();

        Graph alternativeNode = new Graph();
        GraphViewer alternativeViewer = new GraphViewer();

        Graph discreteNode = new Graph();
        GraphViewer discreteViewer = new GraphViewer();

        Graph continuousNode = new Graph();
        GraphViewer continuousViewer = new GraphViewer();

        public Toolbox()
        {
            InitializeComponent();
            Loaded += Toolbox_Loaded;

            parallelViewer.BindToPanel(PanelParallel);
            parallelViewer.LayoutEditingEnabled = false;
            parallelViewer.MouseDown += ParallelViewer_MouseDown;

            alternativeViewer.BindToPanel(PanelAlternative);
            alternativeViewer.LayoutEditingEnabled = false;
            alternativeViewer.MouseDown += AlternativeViewer_MouseDown;

            discreteViewer.BindToPanel(PanelDiscrete);
            discreteViewer.LayoutEditingEnabled = false;
            discreteViewer.MouseDown += DiscreteViewer_MouseDown;

            continuousViewer.BindToPanel(PanelContinuous);
            continuousViewer.LayoutEditingEnabled = false;
            continuousViewer.MouseDown += ContinuousViewer_MouseDown;
        }

        private void ContinuousViewer_MouseDown(object sender, MsaglMouseEventArgs e)
        {
            string type = "continuous";

            DataObject dataObject = new DataObject(type);
            DragDrop.DoDragDrop(PanelContinuous, dataObject, DragDropEffects.Copy);
        }

        private void DiscreteViewer_MouseDown(object sender, MsaglMouseEventArgs e)
        {
            string type = "discrete";

            DataObject dataObject = new DataObject(type);
            DragDrop.DoDragDrop(PanelDiscrete, dataObject, DragDropEffects.Copy);
        }

        private void AlternativeViewer_MouseDown(object sender, MsaglMouseEventArgs e)
        {
            string type = "alternative";

            DataObject dataObject = new DataObject(type);
            DragDrop.DoDragDrop(PanelAlternative, dataObject, DragDropEffects.Copy);
        }

        private void ParallelViewer_MouseDown(object sender, MsaglMouseEventArgs e)
        {
            string type = "parallel";

            DataObject dataObject = new DataObject(type);
            DragDrop.DoDragDrop(PanelParallel, dataObject, DragDropEffects.Copy);
        }

        private void Toolbox_Loaded(object sender, RoutedEventArgs e)
        {
            parallelNode.AddNode("parallel factor");
            parallelViewer.Graph = parallelNode;

            alternativeNode.AddNode("alternative factor");
            alternativeViewer.Graph = alternativeNode;

            discreteNode.AddNode("discrete leaf");
            discreteViewer.Graph = discreteNode;

            continuousNode.AddNode("continuous leaf");
            continuousViewer.Graph = continuousNode;
        }
    }
}

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

using Microsoft.Msagl.Core;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;
using Color = Microsoft.Msagl.Drawing.Color;

namespace EMS.Editor
{
    /// <summary>
    /// Interaktionslogik für GraphToolBox.xaml
    /// </summary>
    public partial class GraphToolBox : UserControl
    {
        public GraphToolBox()
        {
            InitializeComponent();
            //graphViewer.MouseDown += GraphViewer_MouseDown;
            graphViewer.BindToPanel(toolboxCanvas);
            graphViewer.LayoutEditingEnabled = false;
            //graphViewer.GraphCanvas.Width = 200;
            Loaded += (a,b) => GraphToolBox_Loaded(null, null);
        }

        private void GraphViewer_MouseDown(object sender, MsaglMouseEventArgs e)
        {
            Graph graph = (Graph)sender;
        }

        private void GraphToolBox_Loaded(object sender, RoutedEventArgs e)
        {
            masterGraph.AddNode("Factor");

            graphViewer.Graph = masterGraph;

        }

        #region Properties und Felder
        GraphViewer graphViewer = new GraphViewer();
        Graph masterGraph = new Graph();
        

        #endregion
    }
}

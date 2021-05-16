using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;

namespace EMS.Views
{
    /// <summary>
    /// Interaktionslogik für EditTreeView.xaml
    /// </summary>
    public partial class EditTreeView : UserControl
    {
        GraphViewer graphViewer = new GraphViewer();
        Graph masterGraph = new Graph();
        //AutomaticGraphLayoutControl graphControl = new AutomaticGraphLayoutControl();
        public EditTreeView()
        {
            InitializeComponent();

            //Funktionalität aus WpfGraphControl.GraphViewer.BindToPanel()
            //graphViewerPanel.Children.Add(((ViewModels.EditTreeViewModel)DataContext).graphViewer.GraphCanvas);
            //((ViewModels.EditTreeViewModel)DataContext).graphViewer.GraphCanvas.UpdateLayout();

            graphViewer.ObjectUnderMouseCursorChanged += GraphViewer_ObjectUnderMouseCursorChanged;
            graphViewer.BindToPanel(graphViewerPanel);
            graphViewer.MouseDown += GraphViewer_MouseDown;
            //Loaded += (a, b) => EditTreeView_Loaded(null, null);
            
        }





        /// <summary>
        /// Gibt das Objekt unter dem Mauscursor in der Status-TextBox aus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphViewer_ObjectUnderMouseCursorChanged(object sender, ObjectUnderMouseCursorChangedEventArgs e)
        {
            var node = graphViewer.ObjectUnderMouseCursor as IViewerNode;
            if (node != null)
            {
                var drawingNode = (Node)node.DrawingObject;
                statusTextBox.Text = drawingNode.Label.Text;
            }
            else
            {
                var edge = graphViewer.ObjectUnderMouseCursor as IViewerEdge;
                if (edge != null)
                    statusTextBox.Text = ((Edge)edge.DrawingObject).SourceNode.Label.Text + "->" +
                                         ((Edge)edge.DrawingObject).TargetNode.Label.Text;
                else
                    statusTextBox.Text = "No object";
            }
        }

        /// <summary>
        /// Methode für das Click-Verhalten 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphViewer_MouseDown(object sender, MsaglMouseEventArgs e)
        {
            statusTextBox.Text = "there was a click...";
        }

        /// <summary>
        /// Methode zum erzeugen des Graphen beim Laden der Sicht (NOT USED)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Beispiele
                //Graph graph = new Graph();

                //graph.LayoutAlgorithmSettings=new MdsLayoutSettings();

                //graph.AddEdge("1", "2");
                //graph.AddEdge("1", "3");
                //var edge = graph.AddEdge("4", "5");
                //edge.LabelText = "Some edge label";
                //edge.Attr.Color = Color.Red;
                //edge.Attr.LineWidth *= 2;

                //graph.AddEdge("4", "6");
                //edge = graph.AddEdge("7", "8");
                //edge.Attr.LineWidth *= 2;
                //edge.Attr.Color = Color.Red;

                //graph.AddEdge("7", "9");
                //edge = graph.AddEdge("5", "7");
                //edge.Attr.Color = Color.Red;
                //edge.Attr.LineWidth *= 2;

                //graph.AddEdge("2", "7");
                //graph.AddEdge("10", "11");
                //graph.AddEdge("10", "12");
                //graph.AddEdge("2", "10");
                //graph.AddEdge("8", "10");
                //graph.AddEdge("5", "10");
                //graph.AddEdge("13", "14");
                //graph.AddEdge("13", "15");
                //graph.AddEdge("8", "13");
                //graph.AddEdge("2", "13");
                //graph.AddEdge("5", "13");
                //graph.AddEdge("16", "17");
                //graph.AddEdge("16", "18");
                //graph.AddEdge("16", "18");
                //graph.AddEdge("19", "20");
                //graph.AddEdge("19", "21");
                //graph.AddEdge("17", "19");
                //graph.AddEdge("2", "19");
                //graph.AddEdge("22", "23");

                //edge = graph.AddEdge("22", "24");
                //edge.Attr.Color = Color.Red;
                //edge.Attr.LineWidth *= 2;

                //edge = graph.AddEdge("8", "22");
                //edge.Attr.Color = Color.Red;
                //edge.Attr.LineWidth *= 2;

                //graph.AddEdge("20", "22");
                //graph.AddEdge("25", "26");
                //graph.AddEdge("25", "27");
                //graph.AddEdge("20", "25");
                //graph.AddEdge("28", "29");
                //graph.AddEdge("28", "30");
                //graph.AddEdge("31", "32");
                //graph.AddEdge("31", "33");
                //graph.AddEdge("5", "31");
                //graph.AddEdge("8", "31");
                //graph.AddEdge("2", "31");
                //graph.AddEdge("20", "31");
                //graph.AddEdge("17", "31");
                //graph.AddEdge("29", "31");
                //graph.AddEdge("34", "35");
                //graph.AddEdge("34", "36");
                //graph.AddEdge("20", "34");
                //graph.AddEdge("29", "34");
                //graph.AddEdge("5", "34");
                //graph.AddEdge("2", "34");
                //graph.AddEdge("8", "34");
                //graph.AddEdge("17", "34");
                //graph.AddEdge("37", "38");
                //graph.AddEdge("37", "39");
                //graph.AddEdge("29", "37");
                //graph.AddEdge("5", "37");
                //graph.AddEdge("20", "37");
                //graph.AddEdge("8", "37");
                //graph.AddEdge("2", "37");
                //graph.AddEdge("40", "41");
                //graph.AddEdge("40", "42");
                //graph.AddEdge("17", "40");
                //graph.AddEdge("2", "40");
                //graph.AddEdge("8", "40");
                //graph.AddEdge("5", "40");
                //graph.AddEdge("20", "40");
                //graph.AddEdge("29", "40");
                //graph.AddEdge("43", "44");
                //graph.AddEdge("43", "45");
                //graph.AddEdge("8", "43");
                //graph.AddEdge("2", "43");
                //graph.AddEdge("20", "43");
                //graph.AddEdge("17", "43");
                //graph.AddEdge("5", "43");
                //graph.AddEdge("29", "43");
                //graph.AddEdge("46", "47");
                //graph.AddEdge("46", "48");
                //graph.AddEdge("29", "46");
                //graph.AddEdge("5", "46");
                //graph.AddEdge("17", "46");
                //graph.AddEdge("49", "50");
                //graph.AddEdge("49", "51");
                //graph.AddEdge("5", "49");
                //graph.AddEdge("2", "49");
                //graph.AddEdge("52", "53");
                //graph.AddEdge("52", "54");
                //graph.AddEdge("17", "52");
                //graph.AddEdge("20", "52");
                //graph.AddEdge("2", "52");
                //graph.AddEdge("50", "52");
                //graph.AddEdge("55", "56");
                //graph.AddEdge("55", "57");
                //graph.AddEdge("58", "59");
                //graph.AddEdge("58", "60");
                //graph.AddEdge("20", "58");
                //graph.AddEdge("29", "58");
                //graph.AddEdge("5", "58");
                //graph.AddEdge("47", "58");

                //var subgraph = new Subgraph("subgraph 1");
                //graph.RootSubgraph.AddSubgraph(subgraph);
                //subgraph.AddNode(graph.FindNode("47"));
                //subgraph.AddNode(graph.FindNode("58"));

                //graph.AddEdge(subgraph.Id, "55");

                //var node = graph.FindNode("5");
                //node.LabelText = "Label of node 5";
                //node.Label.FontSize = 5;
                //node.Label.FontName = "New Courier";
                //node.Label.FontColor = Microsoft.Msagl.Drawing.Color.Blue;

                //node = graph.FindNode("55");


                //graph.Attr.LayerDirection = LayerDirection.LR;

                //   graph.LayoutAlgorithmSettings.EdgeRoutingSettings.RouteMultiEdgesAsBundles = true;
                //graph.LayoutAlgorithmSettings.EdgeRoutingSettings.EdgeRoutingMode = EdgeRoutingMode.SplineBundling;
                //layout the graph and draw it
                //Graph graph = new Graph();
                //graph.AddEdge("47", "58");
                //graph.AddEdge("70", "71");



                //var subgraph = new Subgraph("subgraph1");
                //graph.RootSubgraph.AddSubgraph(subgraph);
                //subgraph.AddNode(graph.FindNode("47"));
                //subgraph.AddNode(graph.FindNode("58"));

                //var subgraph2 = new Subgraph("subgraph2");
                //subgraph2.Attr.Color = Color.Black;
                //subgraph2.Attr.FillColor = Color.Yellow;
                //subgraph2.AddNode(graph.FindNode("70"));
                //subgraph2.AddNode(graph.FindNode("71"));
                //subgraph.AddSubgraph(subgraph2);
                //graph.AddEdge("58", subgraph2.Id);
                //graph.Attr.LayerDirection = LayerDirection.LR;

                /*
                Graph graph = new Graph("Experiment");
                graph.AddEdge("Experiment", "Production");

                graph.AddEdge("Production", "ProductionParameter");
                graph.AddEdge("Production", "Environment");

                graph.AddEdge("ProductionParmeter", "Zwischenankunftszeit");
                graph.AddEdge("ProductionParmeter", "Liefertermin");
                graph.AddEdge("ProductionParmeter", "Runtime");
                graph.AddEdge("ProductionParmeter", "Arbeitsganzeit");
                */
                //graphViewer.Graph = graph;
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Load Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        /// <summary>
        /// Fügt einen neuen Knoten hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            string nodeName = tb_Name.Text;

            //Graph graph = new Graph();
            masterGraph.AddNode(nodeName);
            graphViewer.Graph = masterGraph;
        }

        private void button_Connect_Click(object sender, RoutedEventArgs e)
        {
            string rootName = tb_RootName.Text;
            string childName = tb_ChildName.Text;

            masterGraph.AddEdge(rootName, childName);
            graphViewer.Graph = masterGraph;
        }

        private void button_SubGraph_Click(object sender, RoutedEventArgs e)
        {
            string rootName = tb_RootName.Text;
            string childName = tb_ChildName.Text;

            masterGraph.AddEdge(rootName, childName);
            Subgraph subgraph = new Subgraph("Master");
            masterGraph.RootSubgraph.AddSubgraph(subgraph);
            subgraph.AddNode(masterGraph.FindNode(rootName));
            subgraph.AddNode(masterGraph.FindNode(childName));

            graphViewer.Graph = masterGraph;
        }
    }
}

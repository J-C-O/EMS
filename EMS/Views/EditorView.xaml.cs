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

namespace EMS.Views
{
    /// <summary>
    /// Interaktionslogik für EditorView.xaml
    /// </summary>
    public partial class EditorView : UserControl
    {
        private Graph masterGraph = new Graph();
        GraphViewer masterViewer = new GraphViewer();

        private Graph undoGraph = new Graph();

        private string[] nodeNames = new string[2];
        private int nameIndex = 0;

        public EditorView()
        {
            InitializeComponent();

            Loaded += EditorView_Loaded;
            masterViewer.BindToPanel(targetBox);
            masterViewer.MouseDown += MasterViewer_MouseDown;
            masterViewer.MouseUp += MasterViewer_MouseUp;
            masterViewer.co
        }

        /// <summary>
        /// Setzt undoGraph auf Zustand von masterViewer.Graph und 
        /// masterViewer.Graph auf den Zustand von masterGraph
        /// </summary>
        private void SetGraph()
        {
            undoGraph = masterViewer.Graph;
            masterViewer.Graph = masterGraph;
        }

        private void MasterViewer_MouseUp(object sender, MsaglMouseEventArgs e)
        {
            statusTextBox.Background = Brushes.Transparent;
            statusTextBox.Text = "Left-Click = Move Node | Right-Click = Add Edge";
        }

        private void MasterViewer_MouseDown(object sender, MsaglMouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                statusTextBox.Background = Brushes.Green;
                statusTextBox.Text = "Edit-Mode: Move Node";

            }
            else
            {
                var rootNode = masterViewer.ObjectUnderMouseCursor as IViewerNode;
                if (rootNode != null)
                {
                    int rank = nameIndex + 1;
                    var drawingRoot = (Node)rootNode.DrawingObject;
                    nodeNames[nameIndex] = drawingRoot.Label.Text;

                    statusTextBox.Background = Brushes.Orange;
                    statusTextBox.Text = "Edit-Mode: Add Edge - " + rank.ToString() + ". Node = " + nodeNames[nameIndex];

                    nameIndex++;
                    if (nameIndex == 2)
                    {
                        masterGraph.AddEdge(nodeNames[0], nodeNames[1]);
                        SetGraph();
                        nameIndex = 0;
                    }
                }
            }
        }

        private void EditorView_Loaded(object sender, RoutedEventArgs e)
        {
            SetGraph();
            statusTextBox.Text = "Left-Click = Move Node | Right-Click = Add Edge";
        }

        private void Target_Drop(object sender, DragEventArgs e)
        {
            string graphFlag = (string)e.Data.GetData(typeof(string));

            switch (graphFlag)
            {
                case "parallel":
                    var dialogPara = new Dialog.NewParallel();
                    if (dialogPara.ShowDialog() == true)
                    {
                        masterGraph.AddNode(dialogPara.ResponseText);
                        SetGraph();
                    }
                    break;
                case "alternative":
                    var dialogAlt = new Dialog.NewAlternative();
                    if (dialogAlt.ShowDialog() == true)
                    {
                        masterGraph.AddNode(dialogAlt.ResponseText);
                        SetGraph();
                    }
                    break;
                case "discrete":
                    
                    break;
                case "continuous":
                    break;
                default:
                    break;
            }


        }

    }
}

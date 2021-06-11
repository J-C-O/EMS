using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EMS.Dialog.EMSMenu;
using EMS.EMSMSAGL.FactorNodes;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;

using EMS.Backend;

namespace EMS.Views
{
    /// <summary>
    /// Interaktionslogik für EditorView.xaml
    /// </summary>
    public partial class EditorView : UserControl
    {
        private Graph masterGraph = new Graph();
        GraphViewer masterViewer = new GraphViewer();

        //Backend.EmsMsaglLinker linkGraph = (Backend.EmsMsaglLinker)Application.Current.TryFindResource("EMSLinker");

        private Graph undoGraph = new Graph();

        private string[] nodeNames = new string[2];
        private int nameIndex = 0;

        private IViewerNode rightclickedNode;

        public EditorView()
        {
            InitializeComponent();



            Loaded += EditorView_Loaded;
            masterViewer.BindToPanel(targetBox);
            masterViewer.MouseDown += MasterViewer_MouseDown;
            masterViewer.MouseUp += MasterViewer_MouseUp;
        }

        /// <summary>
        /// Setzt undoGraph auf Zustand von masterViewer.Graph und 
        /// masterViewer.Graph auf den Zustand von masterGraph
        /// </summary>
        private void SetGraph()
        {
            undoGraph = masterViewer.Graph;
            masterViewer.Graph = EmsMsaglLinker.Graph;

            masterViewer.Graph.Directed = true;

        }

        private void MasterViewer_MouseUp(object sender, MsaglMouseEventArgs e)
        {
            statusTextBox.Background = Brushes.Transparent;
            statusTextBox.Text = "Left-Click = Move Node | Right-Click = Add Edge";
        }

        /// <summary>
        /// Eventhandler für Mausklicks.
        /// Unterschieden wird zwischen: 
        /// Linksklick = Knoten verschieben
        /// Rechtsklick = Knoten einfügen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterViewer_MouseDown(object sender, MsaglMouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var node = masterViewer.ObjectUnderMouseCursor as IViewerNode;
                if (node != null)
                {
                    statusTextBox.Background = Brushes.Green;
                    statusTextBox.Text = "Edit-Mode: Move Node" + node.DrawingObject.GetType().ToString();
                }
            }
            else
            {
                var rootNode = masterViewer.ObjectUnderMouseCursor as IViewerNode;
                rightclickedNode = rootNode;
                if (rootNode != null)
                {
                    #region edge drwaing old
                    //int rank = nameIndex + 1;
                    //var drawingRoot = (Node)rootNode.DrawingObject;
                    //nodeNames[nameIndex] = drawingRoot.Label.Text;

                    //statusTextBox.Background = Brushes.Orange;
                    //statusTextBox.Text = "Edit-Mode: Add Edge - " + rank.ToString() + ". Node = " + nodeNames[nameIndex];

                    //nameIndex++;
                    //if (nameIndex == 2)
                    //{
                    //    masterGraph.AddEdge(nodeNames[0], nodeNames[1]);
                    //    SetGraph();
                    //    nameIndex = 0;
                    //}
                    #endregion

                    if (rootNode.DrawingObject.GetType() == typeof(ParallelNode) || rootNode.DrawingObject.GetType() == typeof(AlternativeNode))
                    {
                        CMComposite menuComposite = new CMComposite();
                        menuComposite.PlacementTarget = sender as DockPanel;

                        menuComposite.AddParallel.Click += AddParallel_Click;
                        menuComposite.AddAlternative.Click += AddAlternative_Click;
                        menuComposite.AddDiscrete.Click += AddDiscrete_Click;
                        menuComposite.AddContinuous.Click += AddContinuous_Click;
                        menuComposite.RemoveNode.Click += RemoveNode_Click;


                        menuComposite.Items.Add(menuComposite.AddParallel);
                        menuComposite.Items.Add(menuComposite.AddAlternative);
                        menuComposite.Items.Add(menuComposite.AddDiscrete);
                        menuComposite.Items.Add(menuComposite.AddContinuous);
                        menuComposite.Items.Add(menuComposite.RemoveNode);
                        
                        menuComposite.IsOpen = true;
                    } else 
                    if (rootNode.DrawingObject.GetType() == typeof(DiscreteNode) || rootNode.DrawingObject.GetType() == typeof(ContinuousNode))
                    {
                        CMComposite menuLeaf = new CMComposite();
                        menuLeaf.PlacementTarget = sender as DockPanel;

                        menuLeaf.RemoveNode.Click += RemoveNode_Click;
                        menuLeaf.Items.Add(menuLeaf.RemoveNode);

                        menuLeaf.IsOpen = true;
                    }

                } else
                {
                    CMComposite menuNon = new CMComposite();
                    menuNon.PlacementTarget = sender as DockPanel;

                    menuNon.AddParallel.Click += AddParallel_Click;
                    menuNon.AddAlternative.Click += AddAlternative_Click;
                    
                    menuNon.Items.Add(menuNon.AddParallel);
                    menuNon.Items.Add(menuNon.AddAlternative);

                    menuNon.IsOpen = true;
                }
            }
        }

        private void RemoveNode_Click(object sender, RoutedEventArgs e)
        {
            if(rightclickedNode != null)
            {
                EmsMsaglLinker.RemoveFactor(rightclickedNode.Node.LabelText);
                SetGraph();
            }
        }

        /// <summary>
        /// Fügt einen kontinuierlichen Knoten zum Graphen hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContinuous_Click(object sender, RoutedEventArgs e)
        {
            if (rightclickedNode != null)
            {
                var dialog = new Dialog.NewContinuous();
                if (dialog.ShowDialog() == true)
                {
                    if (masterViewer.Graph.NodeMap.ContainsKey(dialog.ResponseText))
                    {
                        statusTextBox.Text = "No cycles allowed";
                    }
                    else
                    {
                        EmsMsaglLinker.AddFactor_Continuous(
                            rightclickedNode.Node.LabelText,
                            dialog.ResponseText,
                            new EMSFactorClasses.Intervall(dialog.ResponseText, dialog.StartValueNUM, dialog.EndValueNUM, dialog.IncrementNUM)
                        );
                        SetGraph();
                    }
                }
            }
        }

        /// <summary>
        /// Fügt einen diskreten Knoten zum Graphen hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDiscrete_Click(object sender, RoutedEventArgs e)
        {
            if (rightclickedNode != null)
            {
                var dialog = new Dialog.NewDiscrete();
                if (dialog.ShowDialog() == true)
                {
                    if (masterViewer.Graph.NodeMap.ContainsKey(dialog.ResponseText))
                    {
                        statusTextBox.Text = "No cycles allowed";
                    }
                    else
                    {
                        //masterGraph.AddNode(new DiscreteNode(dialog.ResponseText));
                        //masterGraph.AddEdge(rightclickedNode.Node.LabelText, dialog.ResponseText);

                        EmsMsaglLinker.AddFactor_Discrete(
                            rightclickedNode.Node.LabelText, 
                            dialog.ResponseText, 
                            new EMSFactorClasses.ArrayValue<string>(dialog.ResponseText, dialog.Values)
                        );
                        SetGraph();
                    }
                }
            }
        }

        /// <summary>
        /// Fügt einen alternativen Knoten zum Graphen hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAlternative_Click(object sender, RoutedEventArgs e)
        {
            if (rightclickedNode != null)
            {
                var dialog = new Dialog.NewAlternative();
                if (dialog.ShowDialog() == true)
                {
                    if(masterViewer.Graph.NodeMap.ContainsKey(dialog.ResponseText))
                    {
                        statusTextBox.Text = "No cycles allowed";
                    } else
                    {
                        EmsMsaglLinker.AddFactor_Alternative(rightclickedNode.Node.LabelText, dialog.ResponseText);
                        SetGraph();
                    }                    
                }
            } else
            {
                var dialog = new Dialog.NewAlternative();
                if (dialog.ShowDialog() == true)
                {
                    if (masterViewer.Graph.NodeMap.ContainsKey(dialog.ResponseText))
                    {
                        statusTextBox.Text = "No cycles allowed";
                    }
                    else
                    {
                        EmsMsaglLinker.AddFactor_Alternative(dialog.ResponseText);
                        SetGraph();
                    }
                }
            }
        }
       
        /// <summary>
        /// Fügt einen parallelen Knoten zum Graphen hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParallel_Click(object sender, RoutedEventArgs e)
        {
            if(rightclickedNode != null)
            {
                var dialog = new Dialog.NewParallel();
                if (dialog.ShowDialog() == true)
                {
                    if (masterViewer.Graph.NodeMap.ContainsKey(dialog.ResponseText))
                    {
                        statusTextBox.Text = "No cycles allowed";
                    }
                    else
                    {
                        EmsMsaglLinker.AddFactor_Parallel(rightclickedNode.Node.LabelText, dialog.ResponseText);
                        SetGraph();
                    }
                }   
            } else
            {
                var dialog = new Dialog.NewParallel();
                if (dialog.ShowDialog() == true)
                {
                    if (masterViewer.Graph.NodeMap.ContainsKey(dialog.ResponseText))
                    {
                        statusTextBox.Text = "No cycles allowed";
                    }
                    else
                    {
                        EmsMsaglLinker.AddFactor_Parallel(dialog.ResponseText);
                        SetGraph();
                    }
                }
            }
        }

        /// <summary>
        /// Initialisiert die Ansicht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorView_Loaded(object sender, RoutedEventArgs e)
        {            
            SetGraph();
            statusTextBox.Text = "Left-Click = Move Node | Right-Click = Add Edge";
        }

        /// <summary>
        /// Führt den "Drop"-Vorgang eines Drag&Drop Prozesses aus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Target_Drop(object sender, DragEventArgs e)
        {
            string graphFlag = (string)e.Data.GetData(typeof(string));

            switch (graphFlag)
            {
                case "parallel":
                    var dialogPara = new Dialog.NewParallel();
                    if (dialogPara.ShowDialog() == true)
                    {
                        masterGraph.AddNode(new ParallelNode(dialogPara.ResponseText));
                        SetGraph();
                    }
                    break;
                case "alternative":
                    var dialogAlt = new Dialog.NewAlternative();
                    if (dialogAlt.ShowDialog() == true)
                    {
                        masterGraph.AddNode(new AlternativeNode(dialogAlt.ResponseText));
                        SetGraph();
                    }
                    break;
                case "discrete":
                    var dialogDisc = new Dialog.NewDiscrete();
                    if (dialogDisc.ShowDialog() == true)
                    {
                        masterGraph.AddNode(new DiscreteNode(dialogDisc.ResponseText));
                        SetGraph();
                    }
                    break;
                case "continuous":
                    var dialogCont = new Dialog.NewContinuous();
                    if (dialogCont.ShowDialog() == true)
                    {
                        masterGraph.AddNode(new ContinuousNode(dialogCont.ResponseText));
                        SetGraph();
                    }
                    break;
                default:
                    break;
            }


        }

        private void bttnResetGraph_Click(object sender, RoutedEventArgs e)
        {
            EmsMsaglLinker.ResetTree();
            EmsMsaglLinker.ResetGraph();

            SetGraph();
        }
    }
}

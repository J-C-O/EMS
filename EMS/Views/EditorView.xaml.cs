using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EMS.Dialog.EMSMenu;
using EMS.EMSMSAGL.FactorNodes;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;

using EMS.Backend;
using System.Collections.Generic;
using System.Collections;
using EMS.Dialog;

namespace EMS.Views
{
    /// <summary>
    /// Interaktionslogik für EditorView.xaml
    /// </summary>
    public partial class EditorView : UserControl
    {
        private Graph masterGraph = new Graph();
        GraphViewer masterViewer = new GraphViewer();

        // NICHT MEHR VERWENDET
        //private string[] nodeNames = new string[2];
        //private int nameIndex = 0;

        private IViewerNode _rClickedNode;

        public EditorView()
        {
            InitializeComponent();



            Loaded += EditorView_Loaded;
            masterViewer.BindToPanel(targetBox);
            masterViewer.MouseDown += MasterViewer_MouseDown;
            masterViewer.MouseUp += MasterViewer_MouseUp;
        }

        /// <summary>
        /// Setzt masterViewer.Graph auf den Zustand von EmsMsaglLinker.Graph
        /// </summary>
        private void SetGraph()
        {
            masterViewer.Graph = EmsMsaglLinker.Graph;
            masterViewer.Graph.Directed = true;
        }
        /// <summary>
        /// Setzt den Faktorbaum und seine grafische Darstellung zurück.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnResetGraph_Click(object sender, RoutedEventArgs e)
        {
            EmsMsaglLinker.ResetTree();
            EmsMsaglLinker.ResetGraph();

            SetGraph();
        }



        /// <summary>
        /// Initialisiert die Ansicht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorView_Loaded(object sender, RoutedEventArgs e)
        {
            SetGraph();
            statusTextBox.Text = "Left-Click = Move Node | Right-Click = Add Node";
        }
        /// <summary>
        /// Eventhandler für das loslassen einer Maustaste, der statusTextBox initialisiert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterViewer_MouseUp(object sender, MsaglMouseEventArgs e)
        {
            statusTextBox.Background = Brushes.Transparent;
            statusTextBox.Text = "Left-Click = Move Node | Right-Click = Add Node";
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
                _rClickedNode = rootNode;

                EMSEditorContextMenu editorCMenu = new EMSEditorContextMenu();
                editorCMenu.PlacementTarget = sender as DockPanel;

                editorCMenu.AddParallel.Click += AddParallel_Click;
                editorCMenu.AddAlternative.Click += AddAlternative_Click;

                editorCMenu.AddDiscrete.Click += AddDiscrete_Click;
                editorCMenu.AddContinuous.Click += AddContinuous_Click;

                editorCMenu.EditValues.Click += EditLeafValue_Click;
                editorCMenu.RemoveNode.Click += RemoveNode_Click;

                if (rootNode != null)
                {
                    #region OUTDATED Edge Drawing
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
                        editorCMenu.SetComplexMenu();
                        editorCMenu.IsOpen = true;
                    } else 
                    if (rootNode.DrawingObject.GetType() == typeof(DiscreteNode) || rootNode.DrawingObject.GetType() == typeof(ContinuousNode))
                    {
                        editorCMenu.SetLeafMenu();
                        editorCMenu.IsOpen = true;
                    }

                } else
                {
                    editorCMenu.SetInitMenu();
                    editorCMenu.IsOpen = true;
                }
            }
        }



        #region ADD NODES
        /// <summary>
        /// Fügt einen parallelen Knoten zum Graphen hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParallel_Click(object sender, RoutedEventArgs e)
        {
            if (_rClickedNode != null)
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
                        EmsMsaglLinker.AddFactor_Parallel(_rClickedNode.Node.LabelText, dialog.ResponseText);
                        SetGraph();
                    }
                }
            }
            else
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
        /// Fügt einen alternativen Knoten zum Graphen hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAlternative_Click(object sender, RoutedEventArgs e)
        {
            if (_rClickedNode != null)
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
                        EmsMsaglLinker.AddFactor_Alternative(_rClickedNode.Node.LabelText, dialog.ResponseText);
                        SetGraph();
                    }
                }
            }
            else
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
        /// Fügt einen diskreten Knoten zum Graphen hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDiscrete_Click(object sender, RoutedEventArgs e)
        {
            if (_rClickedNode != null)
            {
                var dialog = new EditValues();
                if (dialog.ShowDialog() == true)
                {
                    if (masterViewer.Graph.NodeMap.ContainsKey(dialog.ResponseText))
                    {
                        statusTextBox.Text = "No cycles allowed";
                    }
                    else
                    {
                        EmsMsaglLinker.AddFactor_Discrete(
                            _rClickedNode.Node.LabelText,
                            dialog.ResponseText,
                            new EMSFactorClasses.ArrayValue<string>(dialog.ResponseText, dialog.ResultArray)
                        );
                        SetGraph();
                    }
                }
            }
        }
        /// <summary>
        /// Fügt einen kontinuierlichen Knoten zum Graphen hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContinuous_Click(object sender, RoutedEventArgs e)
        {
            if (_rClickedNode != null)
            {
                //var dialog = new Dialog.NewContinuous();
                var dialog = new EditIntervall();
                if (dialog.ShowDialog() == true)
                {
                    if (masterViewer.Graph.NodeMap.ContainsKey(dialog.ResponseText))
                    {
                        statusTextBox.Text = "No cycles allowed";
                    }
                    else
                    {
                        EmsMsaglLinker.AddFactor_Continuous(
                            _rClickedNode.Node.LabelText,
                            dialog.ResponseText,
                            //new EMSFactorClasses.Intervall(dialog.ResponseText, dialog.StartValueNUM, dialog.EndValueNUM, dialog.IncrementNUM)
                            new EMSFactorClasses.Intervall(dialog.ResponseText, dialog.StartValue, dialog.EndValue, dialog.Increment)
                        );
                        SetGraph();
                    }
                }
            }
        }

        #endregion

        #region REMOVE/EDIT NODES
        /// <summary>
        /// Prüft den Typen des angeklickten Knotens und ruft ein entsprechendes Fenster zum editieren der Werte auf.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditLeafValue_Click(object sender, RoutedEventArgs e)
        {
            if (_rClickedNode != null)
            {
                Hashtable valuesHT = EmsMsaglLinker.GetLeafValues(_rClickedNode.Node.LabelText);
                if (_rClickedNode.DrawingObject.GetType() == typeof(DiscreteNode))
                {
                    List<string> valuesLI = new List<string>();

                    foreach (DictionaryEntry de in valuesHT)
                    {
                        if (!de.Key.Equals("ActiveValue"))
                        {
                            valuesLI.Add((string)de.Value);
                        }
                    }

                    var discreteDialog = new Dialog.EditValues((string)valuesHT["ActiveValue"],valuesLI.ToArray(), _rClickedNode.Node.LabelText);

                    if (discreteDialog.ShowDialog() == true)
                    {
                        EmsMsaglLinker.SetLeafValues(_rClickedNode.Node.LabelText, discreteDialog.ResultArray);
                    }

                } else if(_rClickedNode.DrawingObject.GetType() == typeof(ContinuousNode))
                {
                    var contDialog = new Dialog.EditIntervall((decimal)valuesHT["StartValue"],
                                                              (decimal)valuesHT["EndValue"],
                                                              (decimal)valuesHT["Increment"],
                                                              (decimal)valuesHT["ActiveValue"],
                                                              _rClickedNode.Node.LabelText);
                    if(contDialog.ShowDialog() == true)
                    {
                        EmsMsaglLinker.SetLeafValues(_rClickedNode.Node.LabelText,
                                                     contDialog.StartValue,
                                                     contDialog.EndValue,
                                                     contDialog.Increment);
                    }
                }
            }
        }
        /// <summary>
        /// Entfernt den angeklickten Knoten aus dem Faktorbaum und dessen grafischer Darstellung.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveNode_Click(object sender, RoutedEventArgs e)
        {
            if(_rClickedNode != null)
            {
                EmsMsaglLinker.RemoveFactor(_rClickedNode.Node.LabelText);
                SetGraph();
            }
        }
        #endregion



        /// <summary>
        /// [NICHT MEHR VERWENDET]
        /// Führt den "Drop"-Vorgang eines Drag&Drop Prozesses aus.
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
    }
}

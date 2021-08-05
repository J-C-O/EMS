using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        /// <summary>
        /// Objekt zum verwalten eines MSAGL Graph-Objekts.
        /// </summary>
        GraphViewer masterViewer = new GraphViewer();

        /// <summary>
        /// Referenz auf einen angeclickten Knoten.
        /// </summary>
        private IViewerNode _rClickedNode;

        public EditorView()
        {
            InitializeComponent();
            // Vorbereiten der Benutzeroberfläche
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
            // Unterscheidung zwischen Maustasten
            // Links -> Knoten bewegen
            // Rechts -> Knoten editieren
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
                // Speichern des Node-Objektes unter der Maustaste
                var rootNode = masterViewer.ObjectUnderMouseCursor as IViewerNode;
                _rClickedNode = rootNode;

                // Erzeugen des  Kontextmenüs
                EMSEditorContextMenu editorCMenu = new EMSEditorContextMenu();
                editorCMenu.PlacementTarget = sender as DockPanel;

                // Hier werden Delegates verwendet, da die Eventhandler mehr Variablen als _sender und _e benötigen.
                editorCMenu.AddParallel.Click += delegate (object _sender, RoutedEventArgs _e) { AddComplex_Click(_sender, _e, true); };
                editorCMenu.AddAlternative.Click += delegate (object _sender, RoutedEventArgs _e) { AddComplex_Click(_sender, _e, false); };

                editorCMenu.AddDiscrete.Click += AddDiscrete_Click;
                editorCMenu.AddContinuous.Click += AddContinuous_Click;

                editorCMenu.EditValues.Click += EditLeafValue_Click;
                editorCMenu.RemoveNode.Click += RemoveNode_Click;

                // Unterscheidung des Initialsieren des Kontextmenüs in Abhängigkeit des Objektes unter dem Mauszeiger
                // Knoten unter dem Mauszeiger?
                // Wenn ja welche Art von Knoten (Complex/Leaf)
                if (rootNode != null)
                {
                    
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

                } else if(rootNode == null && masterViewer.Graph.NodeCount == 0)
                {
                    editorCMenu.SetInitMenu();
                    editorCMenu.IsOpen = true;
                }
            }
        }



        #region ADD NODES
        /// <summary>
        /// Fügt je nach übergebenen Parameter einen parallelen oder einen alternativen Faktor zum Baum hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="parallel">Bestimmt ob der Faktor parallel oder alternativ ist.</param>
        private void AddComplex_Click(object sender, RoutedEventArgs e, bool parallel)
        {
            string winTitle;
            // Nodetype bestimmen
            if(parallel)
            {
                winTitle = "New Parallel Node";
            } else
            {
                winTitle = "New Alternative Node";
            }

            // Nodename bekommen
            var dialog = new NewComplexFactor(winTitle);
            if(dialog.ShowDialog() == true)
            {
                if (masterViewer.Graph.NodeMap.ContainsKey(dialog.ResponseText))
                {
                    statusTextBox.Text = "No cycles allowed";
                } else
                {
                    if(_rClickedNode == null && parallel)
                    {
                        EmsMsaglLinker.AddFactor_Parallel(dialog.ResponseText);
                    } else if(_rClickedNode == null && !parallel)
                    {
                        EmsMsaglLinker.AddFactor_Alternative(dialog.ResponseText);
                    } else if(_rClickedNode != null && parallel)
                    {
                        EmsMsaglLinker.AddFactor_Parallel(_rClickedNode.Node.LabelText, dialog.ResponseText);
                    } else if(_rClickedNode != null && !parallel)
                    {
                        EmsMsaglLinker.AddFactor_Alternative(_rClickedNode.Node.LabelText, dialog.ResponseText);
                    }
                    SetGraph();
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
                //Hashtable mit dem Wertebereich und dem aktiven Wert
                Hashtable valuesHT = EmsMsaglLinker.GetLeafValues(_rClickedNode.Node.LabelText);
                //Unterscheidung zwischen diskretem und kontinuierlichem Faktor
                if (_rClickedNode.DrawingObject.GetType() == typeof(DiscreteNode))
                {
                    List<string> valuesLI = new List<string>();

                    foreach (DictionaryEntry de in valuesHT)
                    {
                        //Alle Einträge in der Hashtable, die nicht den Schlüssel ActiveValue haben, werden in die Liste valuesLI geschrieben
                        if (!de.Key.Equals("ActiveValue"))
                        {
                            valuesLI.Add((string)de.Value);
                        }
                    }

                    //Erzeugen eines Dialogfensters zum editieren der Werte und einsehen des aktiven Werts und Faktornamen
                    var discreteDialog = new EditValues((string)valuesHT["ActiveValue"], valuesLI.ToArray(),
                                                        _rClickedNode.Node.LabelText);

                    if (discreteDialog.ShowDialog() == true)
                    {
                        //setzen der neuen Werte
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
            int _preCount = masterViewer.Graph.NodeCount;
            int _postCount = 0;

            if(_rClickedNode != null)
            {
                //Entfernen des Knotens aus dem Baum
                EmsMsaglLinker.RemoveFactor(_rClickedNode.Node.LabelText);
                //Aktualisieren des Graphen
                SetGraph();
                //setzen der Variable _postCount auf die aktuelle Knotenanzahl
                _postCount = masterViewer.Graph.NodeCount;
            }
            // prüfen ob sich Knotenanzahl verändert hat, wenn nicht konnte der Knoten nicht entfernt werden
            if(_preCount == _postCount)
            {
                statusTextBox.Text = "You can't delete the root";
                statusTextBox.Background = Brushes.Yellow;
            }
        }
        #endregion

    }
}

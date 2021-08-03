using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EMS.EMSFactorClasses;
using EMS.EMSMSAGL.FactorNodes;
using Microsoft.Msagl.Drawing;

namespace EMS.Backend
{
    /// <summary>
    /// Diese statische Klasse koppelt Funktionen der MSAGL-Bibliothek mit denen der Klassen im Namespace EMS.EMSFactorClasses.
    /// </summary>
    public static class EmsMsaglLinker
    {
        #region Properties
        /// <summary>
        /// Objekt vom Typ FactorComplex, hier wird der Faktorbaum gespeichert.
        /// </summary>
        public static FactorComplex Tree = new FactorComplex();

        /// <summary>
        /// Objekt vom Typ Graph, hier wird die grafische Darstellung des Faktorbaums gespeichert.
        /// </summary>
        public static Graph Graph = new Graph();

        /// <summary>
        /// In dieser Eigenschaft werden Statusmeldungen von dieser Klasse gespeichert.
        /// </summary>
        public static string StatusMessage = "";

        //public static string ConfOutput = "";
        #endregion

        #region Reset-Methods
        /// <summary>
        /// Instanziiert Tree mit einem neuen FactorComplex-Objekt.
        /// </summary>
        public static void ResetTree()
        {
            Tree = null;
            Tree = new FactorComplex();
        }

        /// <summary>
        /// Instanziiert Graph mit einem neuen Graph-Objekt.
        /// </summary>
        public static void ResetGraph()
        {
            Graph = null;
            Graph = new Graph();
        }
        #endregion

        #region Tree to Graph
        /// <summary>
        /// Setzt Graph zurück und erzeugt einen neuen Graphen anhand von Tree.
        /// </summary>
        public static void SetTreeGraph()
        {
            List<string> nodeNames = new List<string>();
            Tree.GetNames(nodeNames);

            ResetGraph();

            foreach(string name in nodeNames)
            {
                if(Tree.GetNodeByName(name) != null)
                {
                    AddGraphNode(Tree.GetNodeByName(name));
                }              
            }

        }

        /// <summary>
        /// Fügt einen Knoten entsprechend des übergebenen Faktors factor Graph hinzu.
        /// </summary>
        /// <param name="factor"></param>
        private static void AddGraphNode(Factor factor)
        {

            if (factor.GetType() == typeof(FactorParallel))
            {
                AddFactor(new ParallelNode(factor.Name), factor);
            }
            else
            if (factor.GetType() == typeof(FactorAlternative))
            {
                AddFactor(new AlternativeNode(factor.Name), factor);
            }
            else
            if (factor.GetType().IsGenericType)
            {
                if (factor.GetType().GetGenericTypeDefinition() == typeof(ArrayValue<>))
                {
                    AddFactor(new DiscreteNode(factor.Name), factor);
                }
            }
            else
            if (factor.GetType() == typeof(Intervall))
            {
                AddFactor(new ContinuousNode(factor.Name), factor);
            }
            else
            {
                StatusMessage += "\n Unbekannter Faktor, hinzufügen nicht Möglich.";
                return;
            }

        }
        #endregion

        #region Manage Tree (Initialize;Next;Print;Set/GetLeafValues)
        public static void InitializeTree()
        {
            Tree.SetInitVal();
            ConfOutput = Tree.PrintNodes();
        }
        public static void NextFactor()
        {
            if (Tree.HasNext())
            {
                Tree.GetNext();
                ConfOutput += "\n" + Tree.PrintNodes();
            } else
            {
                ConfOutput += "\n No new configurations";
            }
        }
        public static string PrintTree()
        {
            return Tree.PrintNodes();
        }

        public static void SetLeafValues(string leafName, string[] values)
        {
            //nach setzen der Werte Baum neu initialisieren!

            if (Tree.GetNodeByName(leafName) is ArrayValue<string>)
            {
                (Tree.GetNodeByName(leafName) as ArrayValue<string>).Values = values;
                InitializeTree();
                StatusMessage = "Tree updated";
            }
        }
        public static void SetLeafValues(string leafName, decimal sv, decimal ev, decimal iv)
        {
            if(Tree.GetNodeByName(leafName) is Intervall)
            {
                (Tree.GetNodeByName(leafName) as Intervall).StartVal = sv;
                (Tree.GetNodeByName(leafName) as Intervall).EndVal = ev;
                (Tree.GetNodeByName(leafName) as Intervall).Increment = iv;

                InitializeTree();
                StatusMessage = "Tree updated";
            }
        }

        public static Hashtable GetLeafValues(string leafName)
        {
            Factor factor = Tree.GetNodeByName(leafName);
            if (factor is ArrayValue<string>)
            {
                ArrayValue<string> arrayValue = (ArrayValue<string>)factor;

                Dictionary<string, string> valueDict = new Dictionary<string, string>();
                valueDict.Add("ActiveValue",arrayValue.OutVal);
                //valueDict.Add("Values", arrayValue.Values);
                for (int i = 0; i < arrayValue.Values.Length; i++)
                {
                    valueDict.Add("Value_" + i.ToString(), arrayValue.Values[i]);
                }

                return new Hashtable(valueDict);
            }
            else if (factor is Intervall)
            {
                Intervall intervall = (Intervall)factor;

                Dictionary<string, decimal> intervallDict = new Dictionary<string, decimal>();
                intervallDict.Add("ActiveValue", decimal.Parse(intervall.OutVal));

                intervallDict.Add("StartValue", intervall.StartVal);
                intervallDict.Add("EndValue", intervall.EndVal);
                intervallDict.Add("Increment", intervall.Increment);

                return new Hashtable(intervallDict);
            }
            else return null;

        }
        #endregion
        
        #region Save/Load Tree
        /// <summary>
        /// Exportiert eine Experimentkonfiguration in eine XML-Datei.
        /// </summary>
        /// <param name="configPath">Dateipfad der XML-Datei</param>
        public static void SaveTreeConfig(string configPath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Factor));
            TextWriter writer = new StreamWriter(configPath);

            xmlSerializer.Serialize(writer, Tree);
            writer.Close();
            StatusMessage += "\nKonfiguration gespeichert";
        }

        /// <summary>
        /// Lädt einen Faktorbaum aus einer XML-Datei
        /// </summary>
        /// <param name="configPath">Dateipfad der XML-Datei</param>
        public static void LoadTreeConfig(string configPath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Factor));
            TextReader reader = new StreamReader(configPath);

            Tree = (FactorComplex)xmlSerializer.Deserialize(reader);
            reader.Close();
            StatusMessage += "\nKonfiguration eingelesen";

            //InitializeTree();
        }
        #endregion

        #region RemoveFactor
        /// <summary>
        /// Entfernt einen Faktor mit dem Namen factorName aus den Objekten Tree und Graph.
        /// </summary>
        /// <param name="factorName">zu entfernender Faktorname</param>
        public static void RemoveFactor(string factorName)
        {
            if(Tree.GetNodeByName(factorName) != null && Tree.GetNodeByName(factorName).ParentNode != null)
            {
                Tree.GetNodeByName(Tree.GetNodeByName(factorName).ParentNode).RemoveNode(Tree.GetNodeByName(factorName));
                Graph.RemoveNode(Graph.FindNode(factorName));
            } else
            {
                StatusMessage = "You can't remove the root node, use [Reset Tree] to reset the graph.";
            }
            
            
        }
        #endregion

        #region AddFactor
        /// <summary>
        /// Fügt einen Faktor an Tree an und fügt einen entsprechenden Knoten in Graph ein.
        /// </summary>
        /// <param name="factor">Anzufügender Faktor</param>
        /// <param name="factorNode">grafischer Faktorknoten für Graph</param>
        /// <param name="parentName">Name des Elternknotens</param>
        private static void AddFactor(Factor factor, Node factorNode, string parentName)
        {
            Graph.AddNode(factorNode);
            Graph.AddEdge(parentName, factorNode.Label.Text);

            Tree.AddNodeByParentName(parentName, factor);

        }
        /// <summary>
        /// Fügt einen Wurzelknoten zu Tree und Graph hinzu.
        /// </summary>
        /// <param name="parentName">Knotename</param>
        /// <param name="factorNode">grafischer Faktorknoten für Graph</param>
        private static void AddFactor(string parentName, Node factorNode)
        {
            Graph.AddNode(factorNode);
            Tree.ParentNode = parentName;
        }
        /// <summary>
        /// Fügt dem Graphen einen Knoten hinzu und zieht eine Kante falls factor einen Wert in factor.ParentNode gesetzt hat.
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="factorNode"></param>
        private static void AddFactor(Node factorNode, Factor factor)
        {
            Graph.AddNode(factorNode);
            if(factor.ParentNode != null)
            {
                Graph.AddEdge(factor.ParentNode, factorNode.Label.Text);
            }
        }

        /// <summary>
        /// Fügt einen parallelen Faktor an Tree und Graph an.
        /// </summary>
        /// <param name="rootName">Name des Elternknotens</param>
        /// <param name="childName">Name des Kindknotens</param>
        public static void AddFactor_Parallel(string rootName, string childName)
        {
            FactorParallel parallelChild = new FactorParallel(childName);

            AddFactor(parallelChild, new ParallelNode(childName), rootName);
        }
        /// <summary>
        /// Fügt einen parallelen Faktor an Tree und Graph als Wurzelknoten an.
        /// </summary>
        /// <param name="rootName">Name des Wurzelknotens</param>
        public static void AddFactor_Parallel(string rootName)
        {
            Tree = new FactorParallel(rootName);
            AddFactor(null, new ParallelNode(rootName));
        }

        /// <summary>
        /// Fügt einen alternativen Faktor an Tree und Graph an.
        /// </summary>
        /// <param name="rootName">Name des Elternknotens</param>
        /// <param name="childName">Name des Kindknotens</param>
        public static void AddFactor_Alternative(string rootName, string childName)
        {
            FactorAlternative alternativeChild = new FactorAlternative(childName);

            AddFactor(alternativeChild, new AlternativeNode(childName), rootName);
        }
        /// <summary>
        /// Fügt einen parallelen Faktor an Tree und Graph als Wurzelknoten an.
        /// </summary>
        /// <param name="rootName">Name des Wurzelknotens</param>
        public static void AddFactor_Alternative(string rootName)
        {
            Tree = new FactorAlternative(rootName);
            AddFactor(null, new AlternativeNode(rootName));
        }

        /// <summary>
        /// Fügt einen diskreten Faktor an Tree und Graph an.
        /// </summary>
        /// <param name="rootName">Name des Elternknotens</param>
        /// <param name="childName">Name des Kindknotens</param>
        /// <param name="discreteChild">Diskreter Faktor der Tree hinzugefügt wird</param>
        public static void AddFactor_Discrete(string rootName, string childName, FactorDiscrete discreteChild)
        {
            AddFactor(discreteChild, new DiscreteNode(childName), rootName);
        }

        /// <summary>
        /// Fügt einen kontinuierlichen Faktor an Tree und Graph an.
        /// </summary>
        /// <param name="rootName">Name des Elternknotens</param>
        /// <param name="childName">Name des Kindknotens</param>
        /// <param name="continuousChild">Kontinuierlicher Faktor der Tree hinzugefügt wird</param>
        public static void AddFactor_Continuous(string rootName, string childName, FactorContinuous continuousChild)
        {
            AddFactor(continuousChild, new ContinuousNode(childName), rootName);
        }
        #endregion
    }
}

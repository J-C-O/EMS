using System;
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
    public static class EmsMsaglLinker
    {
        #region Properties
        public static FactorComplex Tree = new FactorComplex();
        public static Graph Graph = new Graph();

        public static Graph TestGraph = new Graph();

        public static string StatusMessage = "";
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
            Tree.SetNames(nodeNames);

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
                Console.WriteLine("MATCH:   " + factor.GetType() + " | " + typeof(FactorParallel));

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
                return;
            }

        }
        #endregion

        #region Manage Tree (Initialize;Next;Print)
        public static void InitializeTree()
        {
            Tree.SetInitVal();
            //StatusMessage = Tree.PrintNodes();
        }
        public static void NextFactor()
        {
            if (Tree.HasNext())
            {
                Tree.GetNext();
                StatusMessage += "\n" + Tree.PrintNodes();
            } else
            {
                StatusMessage += "\n No new configurations";
            }
        }
        public static string PrintTree()
        {
            return Tree.PrintNodes();
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
            if(Tree.GetNodeByName(factorName) != null)
            {
                Tree.GetNodeByName(Tree.GetNodeByName(factorName).ParentNode).RemoveNode(Tree.GetNodeByName(factorName));
            }
            
            Graph.RemoveNode(Graph.FindNode(factorName));
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

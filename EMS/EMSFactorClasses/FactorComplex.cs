using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EMS.EMSFactorClasses

{
    /// <summary>
    /// Basisklasse für komplexe Faktoren.
    /// Implementiert das parallele Verhalten von Faktoren.
    /// </summary>
    //Benötigt für Serialisierung
    [XmlInclude(typeof(FactorParallel))]
    [XmlInclude(typeof(FactorAlternative))]
    public class FactorComplex : Factor
    {

        /// <summary>
        /// Liste vom Typ Factor in der die Objekte der Subfaktoren abgelegt werden.
        /// </summary>
        [XmlArray("Sub-Factors")] //Alias für Serialisierung
        public List<Factor> nodes = new List<Factor>();

        /// <summary>
        /// Index für die Liste nodes, startet beim letzten Eintrag.
        /// </summary>
        public int FactorIDX { get; set; }

        

        /// <summary>
        /// Fügt der Liste nodes ein Objekt vom Typ Factor hinzu.
        /// </summary>
        /// <param name="factor">Objekt vom Typ Factor, welches hinzugefügt werden soll.</param>
        public override void AddNode(Factor factor)
        {
            factor.ParentNode = this.Name;
            this.nodes.Add(factor);

            //hinzugefügten Knoten suchen und ParentNode auf this.Name setzen.
            //this.nodes.Find(x => x.Name == factor.Name).ParentNode = this.Name;
            // FactorIDX muss hier gesetzt werden, da hier entschieden wird wie viele Elemente this.nodes hat.
            FactorIDX = this.nodes.Count - 1;
        }

        /// <summary>
        /// Fügt einen Knoten an einer bestimmten Stelle hinzu.
        /// </summary>
        /// <param name="parent">Elternknoten, hier wird der Knoten angehangen</param>
        /// <param name="child">Kindknoten, der an den Elternknoten angehangen wird.</param>
        public override void AddNodeByParentName(string parent, Factor child)
        {
            if(string.Equals(this.Name, parent))
            {
                this.AddNode(child);
            } else
            {
                foreach (Factor factor in nodes)
                {
                    if (string.Equals(factor.Name, parent))
                    {
                        factor.AddNode(child);
                    }
                    else
                    {
                        factor.AddNodeByParentName(parent, child);
                    }
                }
            }
            
        }

        

        /// <summary>
        /// Entfernt ein Objekt vom Typ Faktor aus der Liste nodes.
        /// </summary>
        /// <param name="factor">Objekt vom Typ Factor, welches entfernt werden soll.</param>
        public override void RemoveNode(Factor factor)
        {
            this.nodes.Remove(factor);
            FactorIDX = this.nodes.Count - 1;
        }

        /// <summary>
        /// Gibt die Eigenschaft Name und alle Elemente in nodes als Zeichenkette zurück.
        /// </summary>
        /// <returns>Eine Zeichenkette mit der Eigenschaft Name und allen Elementen in nodes.</returns>
        public override string PrintNodes()
        {
            int i = 0;
            string result = Name + "(";

            foreach (Factor factor in this.nodes)
            {
                result += factor.PrintNodes();
                if (i != this.nodes.Count - 1)
                {
                    result += "+";
                }
                i++;
            }
            return result + ")";
        }

        public override void SetNames(List<string> nodeNames)
        {
            base.SetNames(nodeNames);
            foreach(Factor factor in nodes)
            {
                factor.SetNames(nodeNames);
            }
        }

        public override Factor GetNodeByName(string nodeName)
        {

            if (this.CheckNodeName(nodeName))
            {
                return this;
            }

            for (int i = 0; i < this.nodes.Count; i++)
            {
                if (this.nodes[i].GetNodeByName(nodeName) != null)
                {
                    return this.nodes[i].GetNodeByName(nodeName);
                }
            }

            return null;
        }

        /// <summary>
        /// Initialisiert alle Elemente in nodes.
        /// </summary>
        public override void SetInitVal()
        {
            foreach (Factor factor in nodes)
            {
                factor.SetInitVal();
                FactorIDX = this.nodes.Count - 1;
                factor.Activate();
            }

        }

        /// <summary>
        /// Prüft ob ein Faktor in nodes[i] weitergeschalten werden kann und zählt i so lange herunter bis
        /// ein weiterschalten möglich ist. Initialisiert gegebenfalls alle Elemente ab nodes[i + 1].
        /// Ruft für das Element in nodes[i] GetNext() auf.
        /// </summary>
        public override void GetNext()
        {
            int i = this.nodes.Count - 1;

            while ((i > 0) && !(this.nodes[i].HasNext()))
            {
                i--;
            }
            // auf initialwert setzen wenn i nicht letzter Eintrag
            if (i < this.nodes.Count - 1)
            {
                for (int j = i + 1; j < this.nodes.Count; j++)
                {
                    this.nodes[j].SetInitVal();
                }
            }
            //Console.WriteLine(this.PrintNodes());
            this.nodes[i].GetNext();
        }

        /// <summary>
        /// Prüft ob ein Faktor weitergeschalten werden kann, indem geprüft wird ob alle Elemente
        /// in der Liste nodes nicht mehr weitergeschalten werden können.
        /// </summary>
        /// <returns>Einen booleschen Wert: true-kann hochzählen; false-kann nicht mehr hochzählen</returns>
        public override bool HasNext()
        {
            // Wenn HasNext() aller Kinder false -> return false
            bool[] validator = new bool[this.nodes.Count];

            int i = 0;
            foreach (Factor factor in this.nodes)
            {
                validator[i] = factor.HasNext();
                i++;
            }

            if (Array.TrueForAll<bool>(validator, IsFalse))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Prüft ob ein übergebener boolescher Wert false entspricht.
        /// </summary>
        /// <param name="value">zu prüfender Wert</param>
        /// <returns>Einen booleschen Wert: true-Wert ist "false"; false-Wert ist "true"</returns>
        private static bool IsFalse(bool value)
        {
            return value == false;
        }

        /// <summary>
        /// Gibt an ob es sich um einen parallelen oder alternativen Faktor handelt.
        /// </summary>
        /// <returns>true = parallel; false = alternativ</returns>
        public virtual bool IsParallel()
        {
            return true;
        }

        /// <summary>
        /// Setzt den Wert der Eigenschaft this.IsActive und nodes.IsActive  auf false. 
        /// </summary>
        public override void Deactivate()
        {
            this.IsActive = false;
            foreach (Factor factor in nodes)
            {
                factor.IsActive = false;
            }
        }

        /// <summary>
        /// Setzt den Wert der Eigenschaft this.IsActive und nodes.IsActive  auf true.
        /// </summary>
        public override void Activate()
        {
            this.IsActive = true;
            foreach (Factor factor in nodes)
            {
                factor.IsActive = true;
            }
        }

        
    }
}
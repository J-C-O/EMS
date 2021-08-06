using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EMS.EMSFactorClasses
{
    /// <summary>
    /// Abstrakte Oberklasse, von der alle Faktorklassen geerbt haben.
    /// </summary>
    //Benötigt für Serialisierung
    [XmlInclude(typeof(FactorComplex))]
    [XmlInclude(typeof(FactorLeaf))]
    public abstract class Factor
    {
        /// <summary>
        /// Eigenschaft die den Namen eines Faktors beschreibt.
        /// </summary>
        [XmlAttribute(attributeName: "FactorName")] //Alias für Serialiserung
        public string Name { get; set; }
        /// <summary>
        /// Eigenschaft die beschreibt ob ein Faktor aktiv ist.
        /// </summary>
        [XmlAttribute(attributeName: "Active")]
        public bool IsActive = true;

        /// <summary>
        /// Beschreibt ob ein Faktor Subfaktoren hält.
        /// </summary>
        [XmlAttribute(attributeName: "Complex")]
        public  bool Composite { get; set; }

        /// <summary>
        /// Eigenschaft mit dem Namen des Elternknotens.
        /// </summary>
        [XmlAttribute(attributeName: "FactorGroup")]
        public string ParentNode { get; set; }

        protected List<string> names = new List<string>();

        /// <summary>
        /// Fügt ein Objekt vom Typ Factor einer Liste hinzu. 
        /// </summary>
        /// <param name="factor">Objekt vom Typ Factor welches einer Liste angehangen werden soll.</param>
        //public virtual void AddNode(Factor factor)
        //{
        //    return;
        //}


        //public virtual void AddNodeByParentName(string parent, Factor child)
        //{
        //    return;
        //}

        /// <summary>
        /// Gibt true zurück, wenn der Name des aufrufenden Knotens mit dem gesuchten übereinstimmt.
        /// </summary>
        /// <param name="nodeName">Name des gesuchten Knotens</param>
        /// <returns></returns>
        public virtual bool CheckNodeName(string nodeName)
        {
            if (string.Equals(this.Name, nodeName))
            {
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Gibt eine Selbstreferenz zurück, wenn der übergebene Wert mit this.Name übereinstimmt.
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public virtual Factor GetNodeByName(string nodeName)
        {
            if (CheckNodeName(nodeName))
            {
                return this;
            } else
            {
                return null;
            }
        }

        /// <summary>
        /// Entfernt ein Objekt vom Typ Faktor aus einer Liste.
        /// </summary>
        /// <param name="factor">Objekt vom Typ Factor welches aus einer Liste entfernt werden soll.</param>
        //public virtual void RemoveNode(Factor factor)
        //{
        //    return;
        //}


        /// <summary>
        /// Gibt Auskunft darüber ob die Klasse ein Kompositum ist.
        /// </summary>
        /// <returns>"true" da Kompositum</returns>
        public virtual bool IsComposite()
        {
            return true;
        }

        /// <summary>
        /// Setzt die IsActive-Eigenschaft auf false.
        /// </summary>
        public virtual void Deactivate()
        {
            this.IsActive = false;
        }

        /// <summary>
        /// Setzt die IsActive-Eigenschaft auf true.
        /// </summary>
        public virtual void Activate()
        {
            this.IsActive = true;
        }

        /// <summary>
        /// Gibt alle Elemente in der Liste nodes als Zeichenkette zurück.
        /// </summary>
        /// <returns></returns>
        public abstract string PrintNodes();

        /// <summary>
        /// Initialisiert ein Factor-Object.
        /// </summary>
        public abstract void SetInitVal();

        /// <summary>
        /// Fügt einer übergebenen Liste this.Name hinzu.
        /// </summary>
        /// <returns></returns>
        public virtual void GetNames(List<string> nodeNames)
        {
            nodeNames.Add(Name);
        }

        /// <summary>
        /// Schaltet ein Factor-Ojekt auf seine nächste Wertausprägung.
        /// </summary>
        public abstract void GetNext();
        /// <summary>
        /// Prüft ob ein Factor-Objekt weitere Werte annehmen kann.
        /// </summary>
        /// <returns>false</returns>
        public virtual bool HasNext()
        {
            return false;
        }

    }
}
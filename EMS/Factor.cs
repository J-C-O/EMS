using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EMSFactorClasses
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
        [XmlAttribute]
        public bool IsActive = true;

        /// <summary>
        /// Fügt ein Objekt vom Typ Factor einer Liste hinzu. 
        /// </summary>
        /// <param name="factor">Objekt vom Typ Factor welches einer Liste angehangen werden soll.</param>
        public virtual void AddNode(Factor factor)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Entfernt ein Objekt vom Typ Faktor aus einer Liste.
        /// </summary>
        /// <param name="factor">Objekt vom Typ Factor welches aus einer Liste entfernt werden soll.</param>
        public virtual void RemoveNode(Factor factor)
        {
            throw new System.NotImplementedException();
        }


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
        /// Schaltet ein Factor-Ojekt auf seine nächste Wertausprägung.
        /// </summary>
        public abstract void GetNext();
        /// <summary>
        /// Prüft ob ein Factor-Objekt weitere Werte annehmen kann.
        /// </summary>
        /// <returns></returns>
        public virtual bool HasNext()
        {
            return true;
        }

        /// <summary>
        /// NOT USED
        /// </summary>
        /// <returns></returns>
        public virtual string PrintConfig()
        {
            throw new System.NotImplementedException();
        }
    }
}
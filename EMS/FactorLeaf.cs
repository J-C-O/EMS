using System;
using System.Xml.Serialization;

namespace EMSFactorClasses
{
    /// <summary>
    /// Basisklasse für atomare Faktoren.
    /// </summary>
    //Benötigt für Serialisierung
    [XmlInclude(typeof(FactorDiscrete))]
    [XmlInclude(typeof(FactorContinuous))]
    public class FactorLeaf : Factor
    {
        /// <summary>
        /// Eigenschaft die den Wert eines Faktors beschreibt.
        /// </summary>
        [XmlAttribute(attributeName:"FactorValue")] //Alias für Serialisierung
        public string OutVal { get; set; }
        /// <summary>
        /// Schaltet zum nächsten Faktorwert
        /// </summary>

        /// <summary>
        /// Setzt die Eigenschaft OutVal auf einen neuen Wert.
        /// </summary>
        public override void GetNext()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prüft ob es einen weiteren Faktorwert gibt.
        /// </summary>
        /// <returns></returns>
        public override bool HasNext()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gibt Auskunft darüber ob die Klasse ein Kompositum ist.
        /// </summary>
        /// <returns>"false" da Blatt</returns>
        public override bool IsComposite()
        {
            return false;
        }

        /// <summary>
        /// Gibt "Leaf" als Zeichenkette zurück.
        /// </summary>
        /// <returns>Leaf</returns>
        public override string PrintNodes()
        {
            return "Leaf";
        }


        /// <summary>
        /// Setzt die Eigenschaft OutVal auf den Initialwert.
        /// </summary>
        public override void SetInitVal()
        {
            throw new NotImplementedException();
        }
    }
}